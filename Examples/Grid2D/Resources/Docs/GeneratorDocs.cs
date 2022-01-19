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
    }
}