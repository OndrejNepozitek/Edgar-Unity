using System;

namespace Edgar.Unity
{
    public interface IDungeonGeneratorPostProcessing
    {
        Random Random { get; }

        void Run(DungeonGeneratorLevelGrid2D level);

        void SetRandomGenerator(Random random);
    }
}