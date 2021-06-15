using Edgar.Geometry;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    public static class DoorsInspectorUtils
    {
        public static void DrawDoorLine(DoorLine doorLine, Grid grid)
        {
            var line = new OrthogonalLineGrid2D(doorLine.From.ToCustomIntVector2(), doorLine.To.ToCustomIntVector2());
            var fromSolid = line.From;
            var toSolid = line.From;

            if (line.Length > 0)
            {
                toSolid += (doorLine.Length - 1) * line.GetDirectionVector();
            }

            var toDotted = line.To;

            var doorsCount = line.Length - doorLine.Length + 2;

            if (doorsCount > 0)
            {
                GridUtils.DrawRectangleOutline(grid, fromSolid.ToUnityIntVector3(), toSolid.ToUnityIntVector3(),
                    Color.red, new Vector2(0.1f, 0.1f), label: $"{doorsCount} door{(doorsCount != 1 ? "s" : "")}\nSize {doorLine.Length}");
                GridUtils.DrawRectangleOutline(grid, fromSolid.ToUnityIntVector3(), toDotted.ToUnityIntVector3(),
                    Color.red, new Vector2(0.1f, 0.1f), isDotted: true);
            }
            else
            {
                GridUtils.DrawRectangleOutline(grid, fromSolid.ToUnityIntVector3(), toDotted.ToUnityIntVector3(),
                    Color.red, new Vector2(0.1f, 0.1f), isDotted: true, label: "Too\nsmall");
            }
        }
    }
}