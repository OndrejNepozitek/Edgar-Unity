using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using Assets.ProceduralLevelGenerator.Scripts.Pro;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.PipelineTasks
{
    public delegate void PlatformerPostProcessCallback(GeneratedLevel level, LevelDescription levelDescription);

    public abstract class PlatformerGeneratorPostProcessBase : ScriptableObject, IPostProcessTask<PlatformerPostProcessCallback>
    {
        public virtual void RegisterCallbacks(PriorityCallbacks<PlatformerPostProcessCallback> callbacks)
        {

        }

        public virtual void Run(GeneratedLevel level, LevelDescription levelDescription)
        {

        }
    }
}