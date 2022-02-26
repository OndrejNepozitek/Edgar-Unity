using System;
using System.Collections.Generic;
using System.Linq;
using Edgar.Geometry;
using Edgar.GraphBasedGenerator.Common;
using Edgar.GraphBasedGenerator.Grid2D;
using Edgar.Legacy.GeneralAlgorithms.Algorithms.Common;
using Edgar.Unity.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Edgar.Unity
{
    /// <summary>
    /// Class used to convert room templates to the representation used in the dungeon generator library.
    /// </summary>
    public static class RoomTemplateLoaderGrid2D
    {
        /// <summary>
        /// Computes a polygon from its tiles.
        /// </summary>
        /// <param name="allPoints"></param>
        /// <returns></returns>
        public static PolygonGrid2D GetPolygonFromTiles(HashSet<Vector3Int> allPoints)
        {
            if (allPoints.Count == 0)
            {
                throw new InvalidOutlineException("There must be at least a single tile in the room template.");
            }

            var orderedDirections = new Dictionary<EdgarVector2Int, List<EdgarVector2Int>>
            {
                {IntVector2Helper.Top, new List<EdgarVector2Int> {IntVector2Helper.Left, IntVector2Helper.Top, IntVector2Helper.Right}},
                {IntVector2Helper.Right, new List<EdgarVector2Int> {IntVector2Helper.Top, IntVector2Helper.Right, IntVector2Helper.Bottom}},
                {IntVector2Helper.Bottom, new List<EdgarVector2Int> {IntVector2Helper.Right, IntVector2Helper.Bottom, IntVector2Helper.Left}},
                {IntVector2Helper.Left, new List<EdgarVector2Int> {IntVector2Helper.Bottom, IntVector2Helper.Left, IntVector2Helper.Top}}
            };

            var allPointsInternal = allPoints.Select(x => x.ToCustomIntVector2()).ToHashSet();
            var smallestX = allPointsInternal.Min(x => x.X);
            var smallestXPoints = allPointsInternal.Where(x => x.X == smallestX).ToList();
            var smallestXYPoint = smallestXPoints[smallestXPoints.MinBy(x => x.Y)];

            var startingPoint = smallestXYPoint;
            var startingDirection = IntVector2Helper.Top;

            var polygonPoints = new List<EdgarVector2Int>();
            var currentPoint = startingPoint + startingDirection;
            var firstPoint = currentPoint;
            var previousDirection = startingDirection;
            var first = true;

            if (!allPointsInternal.Contains(currentPoint))
            {
                throw new InvalidOutlineException("Invalid room shape. Please consult the docs.");
            }

            while (true)
            {
                var foundNeighbor = false;
                var currentDirection = new EdgarVector2Int();

                foreach (var directionVector in orderedDirections[previousDirection])
                {
                    var newPoint = currentPoint + directionVector;

                    if (allPointsInternal.Contains(newPoint))
                    {
                        currentDirection = directionVector;
                        foundNeighbor = true;
                        break;
                    }
                }

                if (!foundNeighbor)
                    throw new InvalidOutlineException("Invalid room shape. Please consult the docs.");

                if (currentDirection != previousDirection)
                {
                    polygonPoints.Add(currentPoint);
                }

                currentPoint += currentDirection;
                previousDirection = currentDirection;

                if (first)
                {
                    first = false;
                }
                else if (currentPoint == firstPoint)
                {
                    break;
                }
            }

            if (!IsClockwiseOriented(polygonPoints))
            {
                polygonPoints.Reverse();
            }

            if (polygonPoints.Count < 4)
            {
                throw new InvalidOutlineException("Invalid room shape. Please consult the docs.");
            }

            try
            {
                return new PolygonGrid2D(polygonPoints);
            }
            catch (ArgumentException e)
            {
                throw new InvalidOutlineException($"Invalid room shape. Please consult the docs. Internal error: {e.Message}");
            }
        }

        /// <summary>
        /// Computes a polygon from points on given tilemaps.
        /// </summary>
        /// <param name="tilemaps"></param>
        /// <returns></returns>
        public static PolygonGrid2D GetPolygonFromTilemaps(ICollection<Tilemap> tilemaps)
        {
            var usedTiles = GetUsedTiles(RoomTemplateUtilsGrid2D.GetTilemapsForOutline(tilemaps));

            return GetPolygonFromTiles(usedTiles);
        }

        /// <summary>
        /// Computes a polygon from points on given tilemaps.
        /// </summary>
        /// <param name="roomTemplate"></param>
        /// <returns></returns>
        public static PolygonGrid2D GetPolygonFromRoomTemplate(GameObject roomTemplate)
        {
            var outlineHandler = roomTemplate.GetComponent<IRoomTemplateOutlineHandlerGrid2D>();
            if (outlineHandler != null)
            {
                var polygon2d = outlineHandler.GetRoomTemplateOutline();
                return polygon2d?.GetGridPolygon();
            }

            var tilemaps = RoomTemplateUtilsGrid2D.GetTilemaps(roomTemplate);
            var outline = RoomTemplateUtilsGrid2D.GetTilemapsForOutline(tilemaps);

            return GetPolygonFromTilemaps(outline);
        }

        /// <summary>
        /// Gets all tiles that are not null in given tilemaps.
        /// </summary>
        /// <param name="tilemaps"></param>
        /// <returns></returns>
        public static HashSet<Vector3Int> GetUsedTiles(IEnumerable<Tilemap> tilemaps)
        {
            var usedTiles = new HashSet<Vector3Int>();

            foreach (var tilemap in tilemaps)
            {
                foreach (var position in tilemap.cellBounds.allPositionsWithin)
                {
                    var tile = tilemap.GetTile(position);

                    if (tile == null)
                    {
                        continue;
                    }

                    usedTiles.Add(position);
                }
            }

            return usedTiles;
        }

        /// <summary>
        ///     Computes a room room template from a given room template game object.
        /// </summary>
        /// <param name="roomTemplatePrefab"></param>
        /// <param name="roomTemplate"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryGetRoomTemplate(GameObject roomTemplatePrefab, out RoomTemplateGrid2D roomTemplate, out ActionResult result)
        {
            roomTemplate = null;

            // Check that the room template has all the required components
            var requiredComponentsResult = RoomTemplateDiagnostics.CheckComponents(roomTemplatePrefab);
            if (requiredComponentsResult.HasErrors)
            {
                result = requiredComponentsResult;
                return false;
            }

            PolygonGrid2D polygon;

            // Check that the outline of the room template is valid
            try
            {
                polygon = GetPolygonFromRoomTemplate(roomTemplatePrefab);
            }
            catch (InvalidOutlineException e)
            {
                result = new ActionResult();
                result.AddError($"The outline of the room template is not valid: {e.Message}");
                return false;
            }

            var allowedTransformations = new List<TransformationGrid2D> {TransformationGrid2D.Identity};
            var roomTemplateComponent = roomTemplatePrefab.GetComponent<RoomTemplateSettingsGrid2D>();
            var repeatMode = roomTemplateComponent?.RepeatMode ?? RoomTemplateRepeatMode.AllowRepeat;
            var doors = roomTemplatePrefab.GetComponent<DoorsGrid2D>();
            var doorMode = doors.GetDoorMode();

            // Check that the doors are valid
            var doorsCheck = RoomTemplateDiagnostics.CheckDoors(polygon, doorMode, doors.SelectedMode);
            if (doorsCheck.HasErrors)
            {
                result = doorsCheck;
                return false;
            }

            roomTemplate = new RoomTemplateGrid2D(polygon, doorMode, roomTemplatePrefab.name, repeatMode, allowedTransformations);

            result = new ActionResult();
            return true;
        }

        public static bool IsClockwiseOriented(IList<EdgarVector2Int> points)
        {
            var previous = points[points.Count - 1];
            var sum = 0L;

            foreach (var point in points)
            {
                sum += (point.X - previous.X) * (long) (point.Y + previous.Y);
                previous = point;
            }

            return sum > 0;
        }
    }
}