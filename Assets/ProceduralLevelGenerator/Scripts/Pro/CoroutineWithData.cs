using System;
using System.Collections;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Pro
{
    public class CoroutineWithData<TValue>
    {
        private TValue returnValue;

        public Coroutine Coroutine;

        public bool IsFinished { get; private set; }

        public Exception Exception { get; private set; }

        public bool HasError => Exception != null;

        public TValue Value
        {
            get
            {
                if (Exception != null)
                {
                    throw Exception;
                }

                return returnValue;
            }
        }

        public void ThrowIfNotSuccessful()
        {
            if (Exception != null)
            {
                throw Exception;
            }
        }

        public IEnumerator InternalRoutine(IEnumerator coroutine, bool throwImmediately = false)
        {
            while (true)
            {
                try
                {
                    if (!coroutine.MoveNext())
                    {
                        IsFinished = true;
                        yield break;
                    }
                }
                catch (Exception e)
                {
                    if (throwImmediately)
                    {
                        throw;
                    }

                    Exception = e;
                    yield break;
                }

                var yielded = coroutine.Current;
                if (yielded != null && yielded.GetType() == typeof(TValue))
                {
                    returnValue = (TValue) yielded;
                    IsFinished = true;
                    yield break;
                }

                yield return coroutine.Current;
            }
        }
    }
}