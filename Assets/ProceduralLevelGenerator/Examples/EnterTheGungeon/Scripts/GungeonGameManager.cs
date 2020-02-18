using System.Collections;
using System.Diagnostics;
using Assets.ProceduralLevelGenerator.Examples.Common;
using Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Rooms;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator;
using Assets.ProceduralLevelGenerator.Scripts.Pro;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    /// <summary>
    /// Example of a simple game manager that uses the DungeonGeneratorRunner to generate levels.
    /// </summary>
    public class GungeonGameManager : GameManagerBase<GungeonGameManager>
    {
        public FogOfWar FogOfWar;

        private GungeonRoomType currentRoomType;
        private long generatorElapsedMilliseconds;

        public void Update()
        {
            if (Input.GetKey(KeyCode.G))
            {
                LoadNextLevel();
            }
        }

        public override void LoadNextLevel()
        {
            // Show loading screen
            ShowLoadingScreen("Enter the Gungeon", "loading..");

            // Reset Fog of War
            FogOfWar?.Reset();

            // Find the generator runner
            var generator = GameObject.Find("Dungeon Generator").GetComponent<DungeonGeneratorRunner>();

            // Start the generator coroutine
            StartCoroutine(GeneratorCoroutine(generator));
        }

        /// <summary>
        /// Coroutine that generates the level.
        /// We need to yield return before the generator starts because we want to show the loading screen
        /// and it cannot happen in the same frame.
        /// It is also sometimes useful to yield return before we hide the loading screen to make sure that
        /// all the scripts that were possibly created during the process are properly initialized.
        /// </summary>
        private IEnumerator GeneratorCoroutine(DungeonGeneratorRunner generator)
        {
            yield return null;

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            generator.Generate();

            stopwatch.Stop();

            yield return null;

            generatorElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            RefreshLevelInfo();
            HideLoadingScreen();
        }

        private void RefreshLevelInfo()
        {
            SetLevelInfo($"Generated in {generatorElapsedMilliseconds / 1000d:F}s\nCurrent room: {currentRoomType}");
        }

        public void SetCurrentRoomType(GungeonRoomType type)
        {
            currentRoomType = type;
            RefreshLevelInfo();
        }

        public void RevealRoom(RoomInstance roomInstance)
        {
            if (FogOfWar != null)
            {
                FogOfWar.VisionGrid.AddPolygon(GetPolygon(roomInstance), (Vector2Int) roomInstance.Position, 1);

                foreach (var doorInstance in roomInstance.Doors)
                {
                    var neighbor = doorInstance.ConnectedRoomInstance;

                    if (neighbor.IsCorridor)
                    {
                        FogOfWar.VisionGrid.AddPolygon(GetPolygon(neighbor), (Vector2Int) neighbor.Position, 1);
                    }
                }
            }
        }

        // TODO: remove later
        private Polygon2D GetPolygon(RoomInstance roomInstance)
        {
            var tilemaps = RoomTemplateUtils.GetTilemaps(roomInstance.RoomTemplateInstance);
            var outlineTilemaps = RoomTemplateUtils.GetTilemapsForOutline(tilemaps);
            var usedTiles = RoomTemplatesLoaderTest.GetUsedTiles(outlineTilemaps);
            var newPolygon = RoomTemplatesLoader.GetPolygonFromTiles(usedTiles);
            return new Polygon2D(newPolygon);
        }
    }
}