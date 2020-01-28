using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.PrecomputedLevels
{
    public abstract class AbstractPrecomputedLevelsHandler : ScriptableObject
    {
        public virtual void OnComputationStarted()
        {
        }

        public virtual void OnComputationEnded()
        {
        }

        public abstract void LoadLevel(object payload);

        public abstract void SaveLevel(object payload);
    }
}