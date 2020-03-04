using System.Linq;
using Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Rooms;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates;
using Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.PipelineTasks;
using Assets.ProceduralLevelGenerator.Scripts.Pro;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Tasks
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Dead Cells/Ramparts post process", fileName = "Dead Cells Ramparts Post Process")]
    public class DeadCellsRampartsPostProcessTask : PlatformerGeneratorPostProcessBase
    {
        public bool AddWalls = true;

        public int WallDepth = 50;

        public TileBase WallTile;
        
        private Tilemap wallsTilemap;

        public override void RegisterCallbacks(PriorityCallbacks<PlatformerPostProcessCallback> callbacks)
        {
            if (AddWalls)
            {
                callbacks.RegisterCallbackAfter(PostProcessPriorities.InitializeSharedTilemaps, AddWallsUnderRoom);
            }
        }

        private void AddWallsUnderRoom(GeneratedLevel level, LevelDescription levelDescription)
        {
            wallsTilemap = RoomTemplateUtils.GetTilemaps(level.RootGameObject).Single(x => x.name == "Walls"); // TODO: add some helper for getting tilemaps

            foreach (var roomInstance in level.GetRoomInstances())
            {
                var room = (DeadCellsRoom) roomInstance.Room;

                if (room.Outside)
                {
                    AddWallsUnderRoom(roomInstance);
                }
            }
        }

        private void AddWallsUnderRoom(RoomInstance roomInstance)
        {
            var roomTemplatePrefab = roomInstance.RoomTemplatePrefab;
            var tilemaps = roomTemplatePrefab.GetComponentsInChildren<Tilemap>().Where(x => x.name != "Other 3").ToList();
            var usedTiles = RoomTemplatesLoader.GetUsedTiles(tilemaps).Select(x => x.ToUnityIntVector3()).ToList(); // TODO: make better
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