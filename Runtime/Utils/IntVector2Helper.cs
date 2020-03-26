using GeneralAlgorithms.DataStructures.Common;

namespace ProceduralLevelGenerator.Unity.Utils
{
    public static class IntVector2Helper
    {
        public static IntVector2 Top => new IntVector2(0, 1);

        public static IntVector2 Right => new IntVector2(1, 0);

        public static IntVector2 Bottom => new IntVector2(0, -1);

        public static IntVector2 Left => new IntVector2(-1, 0);

        public static IntVector2 TopLeft => new IntVector2(-1, 1);

        public static IntVector2 TopRight => new IntVector2(1, 1);

        public static IntVector2 BottomLeft => new IntVector2(-1, -1);

        public static IntVector2 BottomRight => new IntVector2(1, -1);
    }
}