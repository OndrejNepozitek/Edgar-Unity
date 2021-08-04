using Edgar.Geometry;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    public static class DoorsInspectorUtils
    {
        public static void DrawDoorLine(DoorLineGrid2D doorLine, Grid grid, Color color, string label = null)
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
                var finalLabel = $"{doorsCount} door{(doorsCount != 1 ? "s" : "")}\nSize {doorLine.Length}";

                if (label != null)
                {
                    finalLabel += $"\n{label}";
                }

                GridUtils.DrawRectangleOutline(grid, fromSolid.ToUnityIntVector3(), toSolid.ToUnityIntVector3(),
                    color, new Vector2(0.1f, 0.1f), label: finalLabel);
                GridUtils.DrawRectangleOutline(grid, fromSolid.ToUnityIntVector3(), toDotted.ToUnityIntVector3(),
                    color, new Vector2(0.1f, 0.1f), isDotted: true);
            }
            else
            {
                GridUtils.DrawRectangleOutline(grid, fromSolid.ToUnityIntVector3(), toDotted.ToUnityIntVector3(),
                    color, new Vector2(0.1f, 0.1f), isDotted: true, label: "Too\nsmall");
            }
        }
    }
}