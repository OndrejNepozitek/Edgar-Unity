using System.Collections.Generic;
using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.TilemapLayers;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Utils
{
    public static class PostProcessUtils
    {
        public static void CenterGrid(GeneratedLevel level)
        {
            var tilemaps = GetTilemaps(level.RootGameObject);
            tilemaps[0].CompressBounds();

            var offset = tilemaps[0].cellBounds.center;

            foreach (Transform transform in level.RootGameObject.transform)
            {
                transform.position -= offset;
            }
        }

        public static void CombineTilemaps(GeneratedLevel level, ITilemapLayersHandler tilemapLayersHandler)
        {
            // Initialize GameObject that will hold tilemaps
            var tilemapsRoot = new GameObject(GeneratorConstants.TilemapsRootName);
            tilemapsRoot.transform.parent = level.RootGameObject.transform;

            // Create individual tilemaps
            tilemapLayersHandler.InitializeTilemaps(tilemapsRoot);

            var destinationTilemaps = GetTilemaps(tilemapsRoot);

            foreach (var roomInstance in level.GetAllRoomInstances().OrderBy(x => x.IsCorridor))
            {
                var sourceTilemaps = GetTilemaps(roomInstance.RoomTemplateInstance);

                CopyTiles(sourceTilemaps, destinationTilemaps, roomInstance.Position);
            }
        }

        public static void CopyTiles(List<Tilemap> sourceTilemaps, List<Tilemap> destinationTilemaps, Vector3Int offset)
        {
            sourceTilemaps = GeneratorUtils.GetTilemapsForCopying(sourceTilemaps);

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
            foreach (var roomInstance in level.GetAllRoomInstances())
            {
                var roomTemplateInstance = roomInstance.RoomTemplateInstance;
                var tilemaps = GetTilemaps(roomTemplateInstance);

                foreach (var tilemap in tilemaps)
                {
                    var tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();
                    Destroy(tilemapRenderer);
                }
            }
        }

        public static void DisableRoomTemplatesColliders(GeneratedLevel level)
        {
            foreach (var roomInstance in level.GetAllRoomInstances())
            {
                var roomTemplateInstance = roomInstance.RoomTemplateInstance;
                var tilemaps = GetTilemaps(roomTemplateInstance);

                foreach (var tilemap in tilemaps)
                {
                    var compositeCollider = tilemap.GetComponent<CompositeCollider2D>();

                    if (compositeCollider != null && !compositeCollider.isTrigger)
                    {
                        compositeCollider.enabled = false;
                    }
                }
            }
        }

        public static GameObject GetTilemapsRoot(GameObject gameObject)
        {
            return gameObject.transform.Find(GeneratorConstants.TilemapsRootName)?.gameObject ?? gameObject;
        }

        public static List<Tilemap> GetTilemaps(GameObject gameObject)
        {
            var tilemapsHolder = GetTilemapsRoot(gameObject);
            var tilemaps = new List<Tilemap>();

            foreach (var childTransform in tilemapsHolder.transform.Cast<Transform>())
            {
                var tilemap = childTransform.gameObject.GetComponent<Tilemap>();

                if (tilemap != null)
                {
                    tilemaps.Add(tilemap);
                }
            }

            // TODO: return tilemaps or GameObjects?
            return tilemaps;
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