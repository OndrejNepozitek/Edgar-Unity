using System.Collections.Generic;
using System.Linq;

namespace Edgar.Unity
{
    /// <summary>
    /// Class that makes it possible to run callbacks with given priorities (order).
    /// </summary>
    /// <typeparam name="TCallback"></typeparam>
    public class PriorityCallbacks<TCallback>
    {
        private readonly Dictionary<int, List<TCallback>> callbacks = new Dictionary<int, List<TCallback>>();
        private readonly List<TCallback> callbacksBefore = new List<TCallback>();
        private readonly List<TCallback> callbacksAfter = new List<TCallback>();

        public void RegisterCallback(int priority, TCallback callback, bool replaceExisting = false)
        {
            if (!callbacks.ContainsKey(priority))
            {
                callbacks[priority] = new List<TCallback>();
            }

            var existingCallbacks = callbacks[priority];

            if (replaceExisting)
            {
                existingCallbacks.Clear();
            }

            existingCallbacks.Add(callback);
        }

        public void RegisterCallbackAfter(int priority, TCallback callback, bool replaceExisting = false)
        {
            RegisterCallback(priority + 1, callback, replaceExisting);
        }

        public void RegisterCallbackBefore(int priority, TCallback callback, bool replaceExisting = false)
        {
            RegisterCallback(priority - 1, callback, replaceExisting);
        }

        public void RegisterCallbackInsteadOf(int priority, TCallback callback)
        {
            RegisterCallback(priority, callback, true);
        }

        public void RegisterBeforeAll(TCallback callback)
        {
            callbacksBefore.Insert(0, callback);
        }

        public void RegisterAfterAll(TCallback callback)
        {
            callbacksAfter.Add(callback);
        }

        public List<TCallback> GetCallbacks()
        {
            var orderedCallbacks = new List<TCallback>();

            orderedCallbacks.AddRange(callbacksBefore);

            foreach (var priority in callbacks.Keys.OrderBy(x => x))
            {
                orderedCallbacks.AddRange(callbacks[priority]);
            }

            orderedCallbacks.AddRange(callbacksAfter);

            return orderedCallbacks;
        }
    }
}