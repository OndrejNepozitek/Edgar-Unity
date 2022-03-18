using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Edgar.Geometry;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Edgar.Unity.Examples
{
    public class DoorsAnimation : MonoBehaviour
    {
        #if OndrejNepozitekEdgar && UNITY_EDITOR
        public DoorLineGrid2D DoorLine;
        public bool Show = false;

        public void Run()
        {
            // StartCoroutine(RunCoroutine());
            EditorCoroutines.Execute(RunCoroutine());
        }

        private IEnumerator RunCoroutine()
        {
            Show = true;
            var polygon = RoomTemplateLoaderGrid2D.GetPolygonFromRoomTemplate(gameObject);
            var doorsComponent = gameObject.GetComponent<DoorsGrid2D>();
            var doorMode = doorsComponent.GetDoorMode();
            var doors = doorMode.GetDoors(polygon);

            foreach (var doorLineGrid2D in doors)
            {
                foreach (var start in doorLineGrid2D.Line.GetPoints())
                {
                    var end = start + doorLineGrid2D.Length * doorLineGrid2D.Line.GetDirectionVector();

                    DoorLine = new DoorLineGrid2D()
                    {
                        From = start.ToUnityIntVector3(),
                        To = end.ToUnityIntVector3(),
                        Length = doorLineGrid2D.Length + 1,
                    };

                    Debug.Log(1);

                    var timer = new Stopwatch();
                    timer.Start();

                    SceneView.RepaintAll();

                    while (timer.ElapsedMilliseconds < 500)
                    {
                        yield return null;
                    }

                    DoorLine = null;
                }
            }

            Show = false;
            SceneView.RepaintAll();
            yield break;
        }

        [CustomEditor(typeof(DoorsAnimation))]
        public class Inspector : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                if (GUILayout.Button("Run animation"))
                {
                    ((DoorsAnimation) target).Run();
                }
            }

            public void OnSceneGUI()
            {
                var doorsAnimation = (DoorsAnimation) target;
                if (doorsAnimation.DoorLine != null && doorsAnimation.Show)
                {
                    var doorLine = doorsAnimation.DoorLine;

                    var grid = doorsAnimation.gameObject.GetComponentInChildren<Grid>();
                    var color = Color.red;

                    DrawDoorLine(doorLine, grid, color);
                }
            }

            private static void DrawDoorLine(DoorLineGrid2D doorLine, Grid grid, Color color, string label = null)
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

                    DrawRectangleOutline(grid, fromSolid.ToUnityIntVector3(), toSolid.ToUnityIntVector3(),
                        color, new Vector2(0.2f, 0.2f));
                }
            }

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
            private static void DrawRectangleOutline(Grid grid, Vector3Int fromTile, Vector3Int toTile, Color color, Vector2 sizeModifier = default, bool addDiagonal = false, string label = null, bool isDotted = false)
            {
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

                    Handles.Label(points[1] + new Vector3(0.08f, 0), label, style);
                }

                drawLine(points[0], points[1]);
                drawLine(points[1], points[2]);
                drawLine(points[2], points[3]);
                drawLine(points[3], points[0]);

                Handles.DrawSolidRectangleWithOutline(points.ToArray(), color, color);

                if (addDiagonal)
                {
                    drawLine(points[0], points[2]);
                }

                Handles.color = originalColor;
            }
        }

        public static class EditorCoroutines
        {
            public class Coroutine
            {
                public IEnumerator enumerator;
                public System.Action<bool> OnUpdate;
                public List<IEnumerator> history = new List<IEnumerator>();
            }

            static readonly List<Coroutine> coroutines = new List<Coroutine>();

            public static void Execute(IEnumerator enumerator, System.Action<bool> OnUpdate = null)
            {
                if (coroutines.Count == 0)
                {
                    EditorApplication.update += Update;
                }

                var coroutine = new Coroutine {enumerator = enumerator, OnUpdate = OnUpdate};
                coroutines.Add(coroutine);
            }

            static void Update()
            {
                for (int i = 0; i < coroutines.Count; i++)
                {
                    var coroutine = coroutines[i];
                    bool done = !coroutine.enumerator.MoveNext();
                    if (done)
                    {
                        if (coroutine.history.Count == 0)
                        {
                            coroutines.RemoveAt(i);
                            i--;
                        }
                        else
                        {
                            done = false;
                            coroutine.enumerator = coroutine.history[coroutine.history.Count - 1];
                            coroutine.history.RemoveAt(coroutine.history.Count - 1);
                        }
                    }
                    else
                    {
                        if (coroutine.enumerator.Current is IEnumerator)
                        {
                            coroutine.history.Add(coroutine.enumerator);
                            coroutine.enumerator = (IEnumerator) coroutine.enumerator.Current;
                        }
                    }

                    if (coroutine.OnUpdate != null) coroutine.OnUpdate(done);
                }

                if (coroutines.Count == 0) EditorApplication.update -= Update;
            }

            internal static void StopAll()
            {
                coroutines.Clear();
                EditorApplication.update -= Update;
            }
        }
        #endif
    }
}