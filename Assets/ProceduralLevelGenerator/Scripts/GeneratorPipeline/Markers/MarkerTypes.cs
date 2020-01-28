using System;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Markers
{
    [Obsolete("Marker maps should not be used.")]
    public class MarkerTypes
    {
        public static MarkerType Nothing;

        public static MarkerType Wall;

        public static MarkerType Floor;

        public static MarkerType Door;

        public static MarkerType UnderDoor;

        static MarkerTypes()
        {
            Nothing = UnityEngine.Resources.Load<MarkerType>("MarkerTypes/Nothing");
            Wall = UnityEngine.Resources.Load<MarkerType>("MarkerTypes/Wall");
            Floor = UnityEngine.Resources.Load<MarkerType>("MarkerTypes/Floor");
            Door = UnityEngine.Resources.Load<MarkerType>("MarkerTypes/Door");
            UnderDoor = UnityEngine.Resources.Load<MarkerType>("MarkerTypes/UnderDoor");
        }
    }
}