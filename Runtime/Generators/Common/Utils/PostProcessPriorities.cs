namespace ProceduralLevelGenerator.Unity.Generators.Common.Utils
{
    /// <summary>
    /// Priorities of post-processing steps.
    /// </summary>
    public static class PostProcessPriorities
    {
        public static int InitializeSharedTilemaps = 100;

        public static int CopyTilesToSharedTilemaps = 200;

        public static int CenterGrid = 300;

        public static int DisableRoomTemplateRenderers = 400;

        public static int DisableRoomTemplateColliders = 500;
    }
}