using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Utils;
using Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.PipelineTasks;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Tasks
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Dead Cells/Ramparts post process", fileName = "RampartsPostProcess")]
    public class DeadCellsRampartsPostProcessTask : PlatformerGeneratorPostProcessBase
    {
        public bool AddWalls = true;

        public int WallDepth = 50;

        public TileBase WallTile;

        public bool DisableOther3 = true;

        private RoomShapesLoader roomShapesLoader;

        private Tilemap wallsTilemap;

        protected override void Run(GeneratedLevel level, LevelDescription levelDescription)
        {
            roomShapesLoader = new RoomShapesLoader();
            wallsTilemap = PostProcessUtils.GetTilemaps(level.RootGameObject).Single(x => x.name == "Walls"); // TODO: add some helper for getting tilemaps

            if (AddWalls)
            {
                foreach (var roomInstance in level.GetAllRoomInstances())
                {
                    AddWallsUnderRoom(roomInstance);
                }
            }

            if (DisableOther3)
            {
                var other3Tilemap = PostProcessUtils.GetTilemaps(level.RootGameObject).Single(x => x.name == "Other 3");
                other3Tilemap.gameObject.SetActive(false);
            }
        }

        private void AddWallsUnderRoom(RoomInstance roomInstance)
        {
            var roomTemplatePrefab = roomInstance.RoomTemplatePrefab;
            var tilemaps = roomTemplatePrefab.GetComponentsInChildren<Tilemap>().Where(x => x.name != "Other 3").ToList();
            var usedTiles = roomShapesLoader.GetUsedTiles(tilemaps).Select(x => x.ToUnityIntVector3()).ToList(); // TODO: make better
            var minY = usedTiles.Min(x => x.y);

            foreach (var pos in usedTiles.Where(x => x.y == minY))
            {
                for (int i = 1; i <= WallDepth; i++)
                {
                    var wallPosition = roomInstance.Position + pos + Vector3Int.down * i;
                    wallsTilemap.SetTile(wallPosition, WallTile);
                }
            }
        }
    }
}