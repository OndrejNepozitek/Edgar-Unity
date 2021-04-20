namespace Edgar.Unity
{
    /// <summary>
    /// Priorities of post-processing steps.
    /// </summary>
    /// <remarks>
    /// Priorities of post-processing steps.
    /// </remarks>
    public class PostProcessPrioritiesGrid2D
    {
        public static int InitializeSharedTilemaps = 100;

        public static int CopyTilesToSharedTilemaps = 200;

        public static int CenterGrid = 300;

        public static int DisableRoomTemplateRenderers = 400;

        public static int DisableRoomTemplateColliders = 500;
    }
}
