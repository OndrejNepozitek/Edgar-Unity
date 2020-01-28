using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using UnityEngine;
using Random = System.Random;

namespace Assets.Resources.Docs.Pipeline.Example2
{
    public interface IDungeonPayload
    {
        int Size { get; set; }
    }

    public interface IRandomGeneratorPayload
    {
        Random Random { get; set; }
    }

    public class Payload : IRandomGeneratorPayload, IDungeonPayload
    {
        public int Size { get; set; }
        public Random Random { get; set; }
    }

    [CreateAssetMenu(menuName = "Example tasks/Random size task", fileName = "RandomSizeTask")]
    public class RandomSizeConfig : PipelineConfig
    {
        public int MaxSize;
        public int MinSize;
    }

    public class RandomSizeTask<TPayload> : ConfigurablePipelineTask<TPayload, RandomSizeConfig>
        where TPayload : class, IRandomGeneratorPayload, IDungeonPayload
    {
        public override void Process()
        {
            Payload.Size = Payload.Random.Next(Config.MinSize, Config.MaxSize);
        }
    }
}