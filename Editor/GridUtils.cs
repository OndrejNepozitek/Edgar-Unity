using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    public static class GridUtils
    {
        private static DateTime lastIsometricErrorShown = DateTime.MinValue;

        /// <summary>
        /// Draws an outline around the rectangle formed by the two given points.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fromTile">First point of the rectangle.</param>
        /// <param name="toTile">Second point of the rectangle.</param>
        /// <param name="color">Color of the outline</param>
        /// <param name="sizeModifier">How much smaller should the outline be than the actual grid tiles</param>
        /// <param name="addDiagonal">Whether a diagonal should be drawn</param>
        /// <param name="label">Optional label</param>
        /// <param name="isDotted"></param>
        public static void DrawRectangleOutline(Grid grid, Vector3Int fromTile, Vector3Int toTile, Color color, Vector2 sizeModifier = default, bool addDiagonal = false, string label = null, bool isDotted = false)
        {
            if (grid.cellLayout == GridLayout.CellLayout.Isometric || grid.cellLayout == GridLayout.CellLayout.IsometricZAsY)
            {
                if (DateTime.Now.Subtract(TimeSpan.FromSeconds(30)) > lastIsometricErrorShown)
                {
                    Debug.LogError("Isometric levels are only supported in the PRO version");
                    lastIsometricErrorShown = DateTime.Now;
                }

                return;
            }

            var drawLine = isDotted
                ? (Action<Vector3, Vector3>) ((p1, p2) => Handles.DrawDottedLine(p1, p2, 2f))
                : (p1, p2) => Handles.DrawLine(p1, p2);

            // Make sure that the from tile is on the bottom-left
            if (fromTile.x > toTile.x || fromTile.y > toTile.y)
            {
                var tmp = fromTile;
                fromTile = toTile;
                toTile = tmp;
            }

            // Calculate world coordinates of the cells
            var fromWorld = grid.CellToWorld(fromTile);
            var toWorld = grid.CellToWorld(toTile);

            var cellSizeX = grid.cellSize.x;
            var cellSizeY = grid.cellSize.y;

            var xDirection = new Vector3(cellSizeX, 0);
            var yDirection = new Vector3(0, cellSizeY);

            var xSizeModifier = new Vector3(sizeModifier.x, 0);
            var ySizeModifier = new Vector3(0, sizeModifier.y);

            var points = new List<Vector3>();

            if (fromWorld.x < toWorld.x)
            {
                points.Add(fromWorld);
                points.Add(fromWorld + yDirection);
                points.Add(toWorld + yDirection + xDirection);
                points.Add(toWorld + xDirection);
            }
            else
            {
                points.Add(fromWorld);
                points.Add(toWorld + yDirection);
                points.Add(toWorld + yDirection + xDirection);
                points.Add(fromWorld + xDirection);
            }

            points[0] += xSizeModifier + ySizeModifier;
            points[1] += xSizeModifier - ySizeModifier;
            points[2] += -xSizeModifier - ySizeModifier;
            points[3] += -xSizeModifier + ySizeModifier;

            var originalColor = Handles.color;
            Handles.color = color;

            if (!string.IsNullOrEmpty(label))
            {
                var size = HandleUtility.GetHandleSize(points[1] + new Vector3(0.02f, 0));

                var style = new GUIStyle();
                style.normal.textColor = color;
                style.fontSize = (int) (15 / size);

                if (style.fontSize >= 5)
                {
                    Handles.Label(points[1] + new Vector3(0.08f, 0), label, style);
                }
            }

            drawLine(points[0], points[1]);
            drawLine(points[1], points[2]);
            drawLine(points[2], points[3]);
            drawLine(points[3], points[0]);

            if (addDiagonal)
            {
                drawLine(points[0], points[2]);
            }

            Handles.color = originalColor;
        }
    }
}