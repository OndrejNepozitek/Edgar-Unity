using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edgar.GraphBasedGenerator.Common;
using Edgar.GraphBasedGenerator.Common.Exceptions;
using Edgar.GraphBasedGenerator.Grid2D;
using Edgar.GraphBasedGenerator.Grid2D.Internal;
using Edgar.Unity.Diagnostics;
using UnityEngine;

namespace Edgar.Unity
{
    public class PipelineRunner<TPayload> where TPayload : class
    {
        private bool isGenerating = false;
        
        /// <summary>
        ///     Runs given pipeline items with a given payload.
        /// </summary>
        /// <param name="pipelineTasks"></param>
        /// <param name="payload"></param>
        public void Run(IEnumerable<IPipelineTask<TPayload>> pipelineTasks, TPayload payload, bool runDiagnostics = false)
        {
            var enumerator = GetEnumerator(pipelineTasks, payload, runDiagnostics);
            while (enumerator.MoveNext())
            {
                /* empty */
            }
        }

        public IEnumerator GetEnumerator(IEnumerable<IPipelineTask<TPayload>> pipelineTasks, TPayload payload, bool runDiagnostics = false)
        {
            if (isGenerating)
            {
                Debug.LogError($"The generator was called while already generating a level. This usually indicates an error in the setup. It is often caused by calling the generator on Start/Awake from a game manager while having the 'Generate On Start' option turned on in the generator. If you are calling the generator manually, disable 'Generate On Start' in the generator component.");
            }
            
            isGenerating = true;
            
            var enumerator = GetEnumeratorNoErrorHandling(pipelineTasks, payload);
            while (true)
            {
                try
                {
                    var hasNext = enumerator.MoveNext();
                    if (!hasNext)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    isGenerating = false;
                    
                    switch (e)
                    {
                        case TimeoutException timeoutException:
                            HandleTimeoutException(timeoutException, payload);
                            break;
                        case NoSuitableShapeForRoomException noSuitableShapeForRoom:
                            throw HandleNoSuitableShapeException(noSuitableShapeForRoom, payload);
                    }

                    throw;
                }

                yield return null;
            }

            isGenerating = false;

            if (runDiagnostics)
            {
                var results = Diagnostics.Diagnostics.Run(payload);
                Diagnostics.Diagnostics.DisplayPerformanceResults(results, true);
            }
        }

        private IEnumerator GetEnumeratorNoErrorHandling(IEnumerable<IPipelineTask<TPayload>> pipelineTasks, TPayload payload)
        {
            foreach (var pipelineItem in pipelineTasks)
            {
                yield return null;
                
                pipelineItem.Payload = payload;
                var enumerator = pipelineItem.Process();
                
                yield return null;
                
                while (enumerator.MoveNext())
                {
                    yield return null;
                }
            }
        }

        private void HandleTimeoutException(TimeoutException exception, TPayload payload)
        {
            var results = Diagnostics.Diagnostics.Run(payload);
            exception.DiagnosticResults = results;
            Diagnostics.Diagnostics.DisplayPerformanceResults(results);
        }

        private Exception HandleNoSuitableShapeException(NoSuitableShapeForRoomException exception, TPayload payload)
        {
            var room = exception.Room as RoomNode<RoomBase>;
            var roomTemplates = exception
                .NeighboringShapes.Cast<RoomTemplateInstanceGrid2D>()
                .Select(x => x.RoomTemplate)
                .ToList();

            var results = Diagnostics.Diagnostics.Run(payload);
            Diagnostics.Diagnostics.DisplayNoSuitableShapeResults(results, room.Room, roomTemplates);

            return new GeneratorException($"The generator was not able to produce a level due to an error. Please see the console above for additional diagnostic information.");
        }
    }
}