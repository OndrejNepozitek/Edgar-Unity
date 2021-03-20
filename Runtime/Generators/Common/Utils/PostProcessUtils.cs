using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace Edgar.Unity
{
    /// <summary>
    /// Utility post-processing functions
    /// </summary>
    public static class PostProcessUtils
    {
        /// <summary>
        /// Gets the center point of given tilemaps-
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
        /// Position the grid so that the level is centered at (0,0).
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

        /// <summary>
        /// Initializes shared tilemaps of a given level.
        /// </summary>
        /// <param name="level">Generated level.</param>
        /// <param name="mode">Tilemap layers mode.</param>
        /// <param name="defaultTilemapLayersHandler">Default tilemap layers handler. Used for the Default mode.</param>
        /// <param name="customTilemapLayersHandler">Custom tilemap layers handler. Used for the Custom mode.</param>
        /// <param name="example">Example game object for tilemaps structure. Used for the FromExample mode.</param>
        public static void InitializeSharedTilemaps(GeneratedLevel level, TilemapLayersStructureMode mode, ITilemapLayersHandler defaultTilemapLayersHandler, ITilemapLayersHandler customTilemapLayersHandler, GameObject example)
        {
            GameObject tilemapsRoot;

            if (mode == TilemapLayersStructureMode.FromExample)
            {
                if (example == null)
                {
                    throw new ConfigurationException($"When {nameof(PostProcessConfig.TilemapLayersStructure)} is set to {nameof(TilemapLayersStructureMode.FromExample)}, {nameof(PostProcessConfig.TilemapLayersExample)} must not be null. Please set the field in the Dungeon Generator component.");
                }

                var tilemapsSource = example;
                var tilemapsSourceRoot = RoomTemplateUtils.GetTilemapsRoot(tilemapsSource);

                if (tilemapsSourceRoot == tilemapsSource)
                {
                    throw new ConfigurationException($"Given {nameof(PostProcessConfig.TilemapLayersExample)} is not valid as it does not contain a game object called {GeneratorConstants.TilemapsRootName} that holds individual tilemap layers.");
                }

                tilemapsRoot = Object.Instantiate(tilemapsSourceRoot, level.RootGameObject.transform);
                tilemapsRoot.name = GeneratorConstants.TilemapsRootName;

                foreach (var tilemap in tilemapsRoot.GetComponentsInChildren<Tilemap>())
                {
                    tilemap.ClearAllTiles();
                }
            }
            else
            {
                // Initialize GameObject that will hold tilemaps
                tilemapsRoot = new GameObject(GeneratorConstants.TilemapsRootName);
                tilemapsRoot.transform.parent = level.RootGameObject.transform;

                if (mode == TilemapLayersStructureMode.Default)
                {
                    defaultTilemapLayersHandler.InitializeTilemaps(tilemapsRoot);
                } 
                else if (mode == TilemapLayersStructureMode.Custom)
                {
                    if (customTilemapLayersHandler == null)
                    {
                        throw new ConfigurationException($"When {nameof(PostProcessConfig.TilemapLayersStructure)} is set to {nameof(TilemapLayersStructureMode.Custom)}, {nameof(PostProcessConfig.TilemapLayersHandler)} must not be null. Please set the field in the Dungeon Generator component.");
                    }

                    customTilemapLayersHandler.InitializeTilemaps(tilemapsRoot);
                }
            }
        }

        /// <summary>
        /// Sets a given material to all shared tilemap layers.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="tilemapMaterial"></param>
        public static void SetTilemapsMaterial(GeneratedLevel level, Material tilemapMaterial)
        {
            if (tilemapMaterial == null)
            {
                return;
            }

            var tilemapsRoot = RoomTemplateUtils.GetTilemapsRoot(level.RootGameObject);

            foreach (var tilemapRenderer in tilemapsRoot.GetComponentsInChildren<TilemapRenderer>())
            {
                tilemapRenderer.material = tilemapMaterial;
            }
        }

        /// <summary>
        /// Copies to from individual room templates to shared tilemaps.
        /// </summary>
        /// <remarks>
        /// The order is important. First, copy all basic rooms and only then copy corridor rooms.
        /// </remarks>
        /// <param name="level"></param>
        public static void CopyTilesToSharedTilemaps(GeneratedLevel level)
        {
            var destinationTilemaps = level.GetSharedTilemaps();

            foreach (var roomInstance in level.GetRoomInstances().OrderBy(x => x.IsCorridor))
            {
                CopyTiles(roomInstance, destinationTilemaps, true, false);
            }
        }

        /// <summary>
        /// Copies tiles from a given room template to given destination tilemaps.
        /// </summary>
        /// <remarks>
        /// One important aspect of this method is how to handle already existing tiles in destination tilemaps.
        ///
        /// When deleteNonNullTiles is true, it computes all non-null positions across all layers in the room template.
        /// After that, it deletes all tiles on these positions in destination tilemaps.
        ///
        /// When deleteTilesInsideOutline is true, it computes all tiles inside the outline of the room template and
        /// deletes them from the destination tilemaps.
        /// So even if there is a hole inside the room template, the position is still removed.
        ///
        /// deleteNonNullTiles and deleteTilesInsideOutline can be combined together.
        /// </remarks>
        /// <param name="roomInstance">Room instance to be copied to the destination tilemaps.</param>
        /// <param name="destinationTilemaps">List of destination tilemaps.</param>
        /// <param name="deleteNonNullTiles">Whether to delete non-null tiles from destination tilemaps.</param>
        /// <param name="deleteTilesInsideOutline">Whether to delete all tiles insides the outline from destination tilemaps.</param>
        public static void CopyTiles(RoomInstance roomInstance, List<Tilemap> destinationTilemaps, bool deleteNonNullTiles, bool deleteTilesInsideOutline)
        {
            var sourceTilemaps = RoomTemplateUtils.GetTilemaps(roomInstance.RoomTemplateInstance);
            sourceTilemaps = RoomTemplateUtils.GetTilemapsForCopying(sourceTilemaps);

            var tilesToRemove = new List<Vector3Int>();

            if (deleteNonNullTiles)
            {
                var tiles = GetNonNullTiles(sourceTilemaps);
                tilesToRemove.AddRange(tiles.Select(x => x + roomInstance.Position));
            }

            if (deleteTilesInsideOutline)
            {
                var tiles = GetTilesInsideOutline(roomInstance, false);
                tilesToRemove.AddRange(tiles);
            }

            RemoveTiles(destinationTilemaps, tilesToRemove);

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
                        destinationTilemap.SetTile(tilemapPosition + roomInstance.Position, tile);
                        destinationTilemap.SetTransformMatrix(tilemapPosition + roomInstance.Position, sourceTilemap.GetTransformMatrix(tilemapPosition));
                    }
                }
            }
        }

        private static List<Vector3Int> GetTilesInsideOutline(RoomInstance roomInstance, bool useLocalPositions)
        {
            return roomInstance
                .OutlinePolygon
                .GetAllPoints()
                .Select(x => new Vector3Int(x.x, x.y, 0))
                .Select(x => useLocalPositions ? x - roomInstance.Position : x)
                .ToList();
        }

        private static List<Vector3Int> GetNonNullTiles(List<Tilemap> tilemap)
        {
            var tiles = new HashSet<Vector3Int>();

            // Find non-null tiles across all source tilemaps
            foreach (var sourceTilemap in tilemap)
            {
                foreach (var tilemapPosition in sourceTilemap.cellBounds.allPositionsWithin)
                {
                    var tile = sourceTilemap.GetTile(tilemapPosition);

                    if (tile != null)
                    {
                        tiles.Add(tilemapPosition);
                    }
                }
            }

            return tiles.ToList();
        }

        private static void RemoveTiles(List<Tilemap> tilemaps, List<Vector3Int> tiles)
        {
            foreach (var tile in tiles)
            {
                foreach (var tilemap in tilemaps)
                {
                    tilemap.SetTile(tile, null);
                }
            }
        }

        /// <summary>
        /// Disables tilemap renderers in room template instances.
        /// </summary>
        /// <remarks>
        /// This method is useful when using shared tilemaps.
        /// </remarks>
        /// <param name="level"></param>
        public static void DisableRoomTemplatesRenderers(GeneratedLevel level)
        {
            foreach (var roomInstance in level.GetRoomInstances())
            {
                var roomTemplateInstance = roomInstance.RoomTemplateInstance;
                var tilemaps = GetTilemaps(roomTemplateInstance, x => x.IgnoreWhenDisablingRenderers);

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
                var tilemaps = GetTilemaps(roomTemplateInstance, x => x.IgnoreWhenDisablingColliders);

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

        private static List<Tilemap> GetTilemaps(GameObject gameObject, Predicate<IgnoreTilemap> excludePredicate)
        {
            return RoomTemplateUtils
                .GetTilemaps(gameObject)
                .Where(tilemap =>
                {
                    var ignoreTilemap = tilemap.GetComponent<IgnoreTilemap>();
                    return ignoreTilemap == null || !excludePredicate(ignoreTilemap);
                })
                .ToList();
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