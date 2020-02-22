namespace Assets.ProceduralLevelGenerator.Scripts.Pro
{
    public static class PostProcessPriorities
    {
        public static int InitializeSharedTilemaps = 0;

        public static int CopyTilesToSharedTilemaps = 100;

        public static int CenterGrid = 200;

        public static int DisableRoomTemplateRenderers = 300;

        public static int DisableRoomTemplateColliders = 300;
    }
}