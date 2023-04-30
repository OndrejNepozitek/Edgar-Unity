using System;
using UnityEngine;

namespace Edgar.Unity
{
    [Serializable]
    public class DungeonGeneratorConfigGrid2D
    {
        public GameObject RootGameObject;

        /// <summary>
        /// Number of milliseconds before the current attempt to generate
        /// a layout is aborted.
        /// </summary>
        public int Timeout = 10000;

        /// <summary>
        /// Whether to override repeat mode configuration of individual room templates.
        /// </summary>
        public RepeatModeOverride RepeatModeOverride;

        /// <summary>
        /// What is the minimum number of tiles there must be between non-neighbouring rooms.
        /// </summary>
        [Range(0, 5)]
        public int MinimumRoomDistance = 1;
        
        /// <summary>
        /// Controls how room templates are instantiated. <seealso cref="RoomTemplatePrefabModeGrid2D" />
        /// </summary>
        public RoomTemplatePrefabModeGrid2D RoomTemplatePrefabMode = RoomTemplatePrefabModeGrid2D.Instantiate;
    }
}