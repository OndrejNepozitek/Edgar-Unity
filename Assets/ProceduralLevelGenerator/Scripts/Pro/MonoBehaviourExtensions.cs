using System.Collections;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Pro
{
    public static class MonoBehaviourExtensions
    {
        public static CoroutineWithData<TValue> StartCoroutineWithData<TValue>(this MonoBehaviour monoBehaviour, IEnumerator coroutine, bool throwImmediately = false){
            var coroutineObject = new CoroutineWithData<TValue>();
            coroutineObject.Coroutine = monoBehaviour.StartCoroutine(coroutineObject.InternalRoutine(coroutine, throwImmediately));
            return coroutineObject;
        }
    }
}