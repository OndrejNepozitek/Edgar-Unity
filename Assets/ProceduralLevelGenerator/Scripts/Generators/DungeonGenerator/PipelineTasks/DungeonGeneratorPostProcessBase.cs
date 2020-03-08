using System;
﻿using System.Collections;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.PipelineTasks
{
    public abstract class DungeonGeneratorPostProcessBase : PipelineTask<DungeonGeneratorPayload>
    {
        protected Random Random;

        public override IEnumerator Process()
        {
            Random = Payload.Random;
            Run(Payload.GeneratedLevel, Payload.LevelDescription);
            yield return null;
        }

        public abstract void Run(GeneratedLevel level, LevelDescription levelDescription);
    }
}