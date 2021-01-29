using System;
using System.Collections;
using System.Collections.Generic;
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
        public void Run(IEnumerable<IPipelineTask<TPayload>> pipelineTasks, TPayload payload)
        {
            var enumerator = GetEnumerator(pipelineTasks, payload);
            while (enumerator.MoveNext())
            {
                /* empty */
            }
        }

        public IEnumerator GetEnumerator(IEnumerable<IPipelineTask<TPayload>> pipelineTasks, TPayload payload)
        {
            foreach (var pipelineItem in pipelineTasks)
            {
                pipelineItem.Payload = payload;

                var enumerator = InvokeWithDiagnostics(pipelineItem.Process);

                while (InvokeWithDiagnostics(enumerator.MoveNext))
                {
                    yield return null;
                }
            }
        }

        private TReturn InvokeWithDiagnostics<TReturn>(Func<TReturn> function)
        {
            try
            {
                return function();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}