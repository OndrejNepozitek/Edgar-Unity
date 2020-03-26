using System.Collections.Generic;
using System.Linq;
using ProceduralLevelGenerator.Unity.Generators.Common.Rooms;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ProceduralLevelGenerator.Unity.Utils
{
    public class DungeonGeneratorUtils
    {
        /// <summary>
        ///     Copies tiles from individual room templates to the tilemaps that hold generated dungeons.
        /// </summary>
        public void ApplyTemplates(IEnumerable<RoomInstance> rooms, List<Tilemap> destinationTilemaps)
        {
            foreach (var roomInfo in rooms)
            {
                var roomTemplateTilemaps = roomInfo.RoomTemplateInstance.GetComponentsInChildren<Tilemap>().ToList();
                ApplyTemplate(roomTemplateTilemaps, roomInfo.Position, destinationTilemaps);
            }
        }

        /// <summary>
        ///     Copies tiles from a given room template to the tilemaps that hold generated dungeons.
        /// </summary>
        public void ApplyTemplate(List<Tilemap> roomTemplateTilemaps, Vector3Int position, List<Tilemap> destinationTilemaps)
        {
            DeleteNonNullTiles(roomTemplateTilemaps, position, destinationTilemaps);

            for (var i = 0; i < roomTemplateTilemaps.Count; i++)
            {
                var sourceTilemap = roomTemplateTilemaps[i];
                var destinationTilemap = destinationTilemaps[i];

                foreach (var tilemapPosition in sourceTilemap.cellBounds.allPositionsWithin)
                {
                    var tile = sourceTilemap.GetTile(tilemapPosition);

                    if (tile != null)
                    {
                        destinationTilemap.SetTile(tilemapPosition + position, tile);
                    }
                }
            }
        }

        /// <summary>
        ///     Finds all non null tiles in a given room and then takes these positions and deletes
        ///     all such tiles on all tilemaps of the dungeon. The reason for this is that we want to
        ///     replace all existing tiles with new tiles from the room.
        /// </summary>
        /// <param name="roomTemplateTilemaps"></param>
        /// <param name="position"></param>
        /// <param name="destinationTilemaps"></param>
        protected void DeleteNonNullTiles(List<Tilemap> roomTemplateTilemaps, Vector3Int position, List<Tilemap> destinationTilemaps)
        {
            var tilesToRemove = new HashSet<Vector3Int>();

            // Find non-null tiles across all tilemaps of the room
            foreach (var sourceTilemap in roomTemplateTilemaps)
            {
                foreach (var tilemapPosition in sourceTilemap.cellBounds.allPositionsWithin)
                {
                    var tile = sourceTilemap.GetTile(tilemapPosition);

                    if (tile != null)
                    {
                        tilesToRemove.Add(tilemapPosition);
                    }
                }
            }

            // Delete all found tiles across all tilemaps of the dungeon
            for (var i = 0; i < roomTemplateTilemaps.Count; i++)
            {
                var destinationTilemap = destinationTilemaps[i];

                foreach (var tilemapPosition in tilesToRemove)
                {
                    destinationTilemap.SetTile(tilemapPosition + position, null);
                }
            }
        }
    }
}