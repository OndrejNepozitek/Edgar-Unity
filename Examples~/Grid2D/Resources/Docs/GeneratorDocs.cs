using UnityEngine;

namespace Edgar.Unity.Examples.Grid2D.Resources.Docs
{
    internal class GeneratorDocs
    {
        public void Run()
        {
            #region codeBlock:2d_generator_run

            var generator = GameObject.Find("Dungeon Generator").GetComponent<DungeonGeneratorGrid2D>();
            generator.Generate();

            #endregion
        }

        public void ChangeConfiguration()
        {
            #region codeBlock:2d_generator_changeConfiguration

            // Get the generator component
            var generator = GameObject.Find("Dungeon Generator").GetComponent<DungeonGeneratorGrid2D>();

            // Access the input config
            generator.FixedLevelGraphConfig.UseCorridors = false;

            // Access the generator config
            generator.GeneratorConfig.Timeout = 5000;

            // Access the post-processing config
            generator.PostProcessConfig.CenterGrid = false;

            // Access other properties
            generator.UseRandomSeed = false;
            generator.RandomGeneratorSeed = 1000;
            generator.GenerateOnStart = false;

            #endregion
        }
    }
}