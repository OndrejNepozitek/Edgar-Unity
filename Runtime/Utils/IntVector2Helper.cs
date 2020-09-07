using Edgar.Geometry;

namespace ProceduralLevelGenerator.Unity.Utils
{
    public static class IntVector2Helper
    {
        public static Vector2Int Top => new Vector2Int(0, 1);

        public static Vector2Int Right => new Vector2Int(1, 0);

        public static Vector2Int Bottom => new Vector2Int(0, -1);

        public static Vector2Int Left => new Vector2Int(-1, 0);

        public static Vector2Int TopLeft => new Vector2Int(-1, 1);

        public static Vector2Int TopRight => new Vector2Int(1, 1);

        public static Vector2Int BottomLeft => new Vector2Int(-1, -1);

        public static Vector2Int BottomRight => new Vector2Int(1, -1);
    }
}