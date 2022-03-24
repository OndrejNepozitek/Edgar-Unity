using System.Linq;
using UnityEngine.Tilemaps;

namespace Edgar.Unity.Examples.UnusedDoors
{
    /// <summary>
    /// Simple post-processing script to demonstrate the usage of the roomInstance.DoorLines property.
    /// The goal is to retrieve all the possible door positions and mark used tiles with one special tile
    /// and unused tiles with a different special tile in the generated level.
    /// </summary>
    public class UnusedDoorsPostProcessing : DungeonGeneratorPostProcessingComponentGrid2D
    {
        /// <summary>
        /// This tile will be used to mark used door tiles.
        /// </summary>
        public TileBase UsedDoorTile;

        /// <summary>
        /// This tile will be used to mark unused door tiles.
        /// </summary>
        public TileBase UnusedDoorTile;

        public override void Run(DungeonGeneratorLevelGrid2D level)
        {
            // Prepare a tile to draw our special tiles on
            // For this example, the "Other 1" tilemap layer is used
            var tilemaps = level.GetSharedTilemaps();
            var tilemap = tilemaps.First(x => x.name == "Other 1");

            // Go through all the rooms in the level
            foreach (var roomInstance in level.RoomInstances)
            {
                // For each room, go through its door lines
                // These door lines mark available door positions which might not be used in the level
                foreach (var doorLine in roomInstance.DoorLines)
                {
                    // Get all the tiles on the door line
                    foreach (var tileInfo in doorLine.GetTiles())
                    {
                        // Check if the tile was used for a door or not
                        // Based on the condition, choose the correct tile
                        var tile = tileInfo.IsUsed ? UsedDoorTile : UnusedDoorTile;

                        // Draw the tile on the tilemap
                        // Do not forget that the tile position is relative to the room template,
                        // so you need to add the position of the room itself
                        tilemap.SetTile(tileInfo.Position + roomInstance.Position, tile);
                    }
                }
            }
        }
    }
}