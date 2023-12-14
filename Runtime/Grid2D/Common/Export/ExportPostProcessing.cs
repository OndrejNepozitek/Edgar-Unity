using System.Linq;
using UnityEngine;

namespace Edgar.Unity.Export
{
    #if OndrejNepozitekEdgar
    // This post-processing class is used to debug exported levels.
    [AddComponentMenu("Edgar/Grid2D/Export post-processing (you should not see this)")]
    public class ExportPostProcessing : DungeonGeneratorPostProcessingComponentGrid2D
    {
        public override void Run(DungeonGeneratorLevelGrid2D level)
        {
            RemoveWallsFromDoors(level);
        }

        private void RemoveWallsFromDoors(DungeonGeneratorLevelGrid2D level)
        {
            // Get the tilemap that we want to delete tiles from
            var walls = level.GetSharedTilemaps().Single(x => x.name == "Other 1");

            // Go through individual rooms
            foreach (var roomInstance in level.RoomInstances)
            {
                // Go through individual doors
                foreach (var doorInstance in roomInstance.Doors)
                {
                    // Remove all the wall tiles from door positions
                    foreach (var point in doorInstance.DoorLine.GetPoints())
                    {
                        walls.SetTile(point + roomInstance.Position, null);
                    }
                }
            }
        }
    }
    #endif
}