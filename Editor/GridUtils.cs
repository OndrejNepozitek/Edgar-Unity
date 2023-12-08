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

            // Make sure that the from tile is on the bottom-left
            if (fromTile.x > toTile.x || fromTile.y > toTile.y)
            {
                (fromTile, toTile) = (toTile, fromTile);
            }

            // Calculate world coordinates of the cells
            var fromWorld = grid.CellToWorld(fromTile);
            var toWorld = grid.CellToWorld(toTile);

            var xDirection = grid.CellToLocal(new Vector3Int(1, 0, 0));
            var yDirection = grid.CellToLocal(new Vector3Int(0, 1, 0));

            var xSizeModifier = sizeModifier.x * xDirection;
            var ySizeModifier = sizeModifier.y * yDirection;

            var points = new List<Vector3>();

            if (fromTile.x < toTile.x)
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

            DrawLine(points[0], points[1], isDotted);
            DrawLine(points[1], points[2], isDotted);
            DrawLine(points[2], points[3], isDotted);
            DrawLine(points[3], points[0], isDotted);

            if (addDiagonal)
            {
                DrawLine(points[0], points[2], isDotted);
            }

            Handles.color = originalColor;
        }

        private static void DrawLine(Vector3 point1, Vector3 point2, bool isDotted)
        {
            if (isDotted)
            {
                Handles.DrawDottedLine(point1, point2, 2f);
            }
            else
            {
                Handles.DrawLine(point1, point2);
            }
        }
    }
}