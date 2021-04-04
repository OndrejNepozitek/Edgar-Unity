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
            foreach (var pipelineItem in pipelineTasks)
            {
                pipelineItem.Payload = payload;

                IEnumerator enumerator;

                try
                {
                    enumerator = pipelineItem.Process();
                }
                catch (TimeoutException e)
                {
                    HandleTimeoutException(e, payload);
                    throw;
                }
                catch (NoSuitableShapeForRoomException e)
                {
                    throw HandleNoSuitableShapeException(e, payload);
                }

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
                    catch (TimeoutException e)
                    {
                        HandleTimeoutException(e, payload);
                        throw;
                    }
                    catch (NoSuitableShapeForRoomException e)
                    {
                        throw HandleNoSuitableShapeException(e, payload);
                    }

                    yield return null;
                }
            }

            if (runDiagnostics)
            {
                var results = Diagnostics.Diagnostics.Run(payload);
                Diagnostics.Diagnostics.DisplayPerformanceResults(results, true);
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