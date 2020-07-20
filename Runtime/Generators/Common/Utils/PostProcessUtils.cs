using System;
using System.Collections.Generic;
using System.Linq;
using ProceduralLevelGenerator.Unity.Generators.Common.Rooms;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.TilemapLayers;
using ProceduralLevelGenerator.Unity.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace ProceduralLevelGenerator.Unity.Generators.Common.Utils
{
    /// <summary>
    /// Utility post-processing functions
    /// </summary>
    public static class PostProcessUtils
    {
        /// <summary>
        /// Gets the center point of given tilemaps
        /// </summary>
        /// <param name="tilemaps"></param>
        /// <param name="compressBounds">Whether to compress bounds of individual tilemaps before computing the center.</param>
        /// <returns></returns>
        public static Vector3 GetTilemapsCenter(List<Tilemap> tilemaps, bool compressBounds = false)
        {
            var minX = int.MaxValue;
            var maxX = int.MinValue;
            var minY = int.MaxValue;
            var maxY = int.MinValue;
    
            foreach (var tilemap in tilemaps)
            {
                if (compressBounds)
                {
                    tilemap.CompressBounds();
                }

                var cellBounds = tilemap.cellBounds;

                if (cellBounds.size.x + cellBounds.size.y == 0)
                {
                    continue;
                }

                minX = Math.Min(minX, cellBounds.xMin);
                maxX = Math.Max(maxX, cellBounds.xMax);
                minY = Math.Min(minY, cellBounds.yMin);
                maxY = Math.Max(maxY, cellBounds.yMax);
            }

            var offset = new Vector3((maxX + minX) / 2f, (maxY + minY) / 2f);

            var grid = tilemaps[0].layoutGrid;

            if (grid != null)
            {
                offset = grid.GetCellCenterLocal((offset * 100).RoundToUnityIntVector3()) / 100;
            }

            return offset;
        }

        /// <summary>
        /// Center the grid so that the level is centered at (0,0).
        /// </summary>
        /// <param name="level"></param>
        /// <param name="compressBounds">Whether to compress bounds of individual tilemaps before computing the center.</param>
        public static void CenterGrid(GeneratedLevel level, bool compressBounds = false)
        {
            var center = GetTilemapsCenter(level.GetSharedTilemaps(), compressBounds);

            foreach (Transform transform in level.RootGameObject.transform)
            {
                transform.position -= center;
            }
        }

        public static void InitializeSharedTilemaps(GeneratedLevel level, ITilemapLayersHandler tilemapLayersHandler)
        {
            // Initialize GameObject that will hold tilemaps
            var tilemapsRoot = new GameObject(GeneratorConstants.TilemapsRootName);
            tilemapsRoot.transform.parent = level.RootGameObject.transform;

            // Create individual tilemaps
            tilemapLayersHandler.InitializeTilemaps(tilemapsRoot);
        }

        public static void CopyTilesToSharedTilemaps(GeneratedLevel level)
        {
            foreach (var roomInstance in level.GetRoomInstances().OrderBy(x => x.IsCorridor))
            {
                CopyTilesToSharedTilemaps(level, roomInstance);
            }
        }

        public static void CopyTilesToSharedTilemaps(GeneratedLevel level, RoomInstance roomInstance)
        {
            var destinationTilemaps = level.GetSharedTilemaps();
            var sourceTilemaps = RoomTemplateUtils.GetTilemaps(roomInstance.RoomTemplateInstance);

            CopyTiles(sourceTilemaps, destinationTilemaps, roomInstance.Position);
        }

        public static void CopyTiles(List<Tilemap> sourceTilemaps, List<Tilemap> destinationTilemaps, Vector3Int offset)
        {
            sourceTilemaps = RoomTemplateUtils.GetTilemapsForCopying(sourceTilemaps);

            DeleteNonNullTiles(sourceTilemaps, destinationTilemaps, offset);

            foreach (var sourceTilemap in sourceTilemaps)
            {
                var destinationTilemap = destinationTilemaps.FirstOrDefault(x => x.name == sourceTilemap.name);

                if (destinationTilemap == null)
                {
                    continue;
                }

                foreach (var tilemapPosition in sourceTilemap.cellBounds.allPositionsWithin)
                {
                    var tile = sourceTilemap.GetTile(tilemapPosition);

                    if (tile != null)
                    {
                        destinationTilemap.SetTile(tilemapPosition + offset, tile);
                        destinationTilemap.SetTransformMatrix(tilemapPosition + offset, sourceTilemap.GetTransformMatrix(tilemapPosition));
                    }
                }
            }
        }

        /// <summary>
        ///     Finds all non null tiles in a given room and then takes these positions and deletes
        ///     all such tiles on all tilemaps of the dungeon. The reason for this is that we want to
        ///     replace all existing tiles with new tiles from the room.
        /// </summary>
        /// <param name="sourceTilemaps"></param>
        /// <param name="offset"></param>
        /// <param name="destinationTilemaps"></param>
        private static void DeleteNonNullTiles(List<Tilemap> sourceTilemaps, List<Tilemap> destinationTilemaps, Vector3Int offset)
        {
            var tilesToRemove = new HashSet<Vector3Int>();

            // Find non-null tiles across all source tilemaps
            foreach (var sourceTilemap in sourceTilemaps)
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

            // Delete all found tiles across all destination tilemaps
            for (var i = 0; i < sourceTilemaps.Count; i++)
            {
                var destinationTilemap = destinationTilemaps[i];

                foreach (var tilemapPosition in tilesToRemove)
                {
                    destinationTilemap.SetTile(tilemapPosition + offset, null);
                }
            }
        }

        public static void DisableRoomTemplatesRenderers(GeneratedLevel level)
        {
            foreach (var roomInstance in level.GetRoomInstances())
            {
                var roomTemplateInstance = roomInstance.RoomTemplateInstance;
                var tilemaps = RoomTemplateUtils.GetTilemaps(roomTemplateInstance);

                foreach (var tilemap in tilemaps)
                {
                    var tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();
                    Destroy(tilemapRenderer);
                }
            }
        }

        /// <summary>
        /// Disables colliders of individual room template tilemaps in the generated level.
        /// The goal is to try to keep triggers functioning.
        /// </summary>
        /// <param name="level"></param>
        public static void DisableRoomTemplatesColliders(GeneratedLevel level)
        {
            // Iterate through all the rooms
            foreach (var roomInstance in level.GetRoomInstances())
            {
                var roomTemplateInstance = roomInstance.RoomTemplateInstance;
                var tilemaps = RoomTemplateUtils.GetTilemaps(roomTemplateInstance);

                foreach (var tilemap in tilemaps)
                {
                    // Iterate through all the colliders
                    foreach (var collider in tilemap.GetComponents<Collider2D>())
                    {
                        // If the collider is not used by composite collider and it is not a trigger, destroy it
                        if (!collider.usedByComposite && !collider.isTrigger)
                        {
                            Destroy(collider);
                        } 
                        else if (collider.usedByComposite)
                        {
                            // If the collider is used by composite but that composite does not exist or is not a trigger, destroy it
                            if (collider.composite == null || !collider.composite.isTrigger)
                            {
                                Destroy(collider);
                            }
                        }
                    }
                }
            }
        }

        // TODO: where to put this?
        public static void Destroy(Object gameObject)
        {
            if (Application.isPlaying)
            {
                Object.Destroy(gameObject);
            }
            else
            {
                Object.DestroyImmediate(gameObject);
            }
        }
    }
}