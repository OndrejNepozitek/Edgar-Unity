using Edgar.Geometry;

namespace Edgar.Unity
{
    public static class IntVector2Helper
    {
        public static EdgarVector2Int Top => new EdgarVector2Int(0, 1);

        public static EdgarVector2Int Right => new EdgarVector2Int(1, 0);

        public static EdgarVector2Int Bottom => new EdgarVector2Int(0, -1);

        public static EdgarVector2Int Left => new EdgarVector2Int(-1, 0);

        public static EdgarVector2Int TopLeft => new EdgarVector2Int(-1, 1);

        public static EdgarVector2Int TopRight => new EdgarVector2Int(1, 1);

        public static EdgarVector2Int BottomLeft => new EdgarVector2Int(-1, -1);

        public static EdgarVector2Int BottomRight => new EdgarVector2Int(1, -1);
    }
}