using System.Linq;
using Edgar.Unity.Examples.CurrentRoomDetection;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Edgar.Unity.Examples.Grid2D.Resources.Docs
{
    internal class CurrentRoomDetectionDocs1
    {
        #region codeBlock:2d_currentRoomDetection_postProcessing_1

        public class CurrentRoomDetectionPostProcessing : DungeonGeneratorPostProcessingGrid2D
        {
            public override void Run(DungeonGeneratorLevelGrid2D level)
            {
                foreach (var roomInstance in level.RoomInstances)
                {
                    var roomTemplateInstance = roomInstance.RoomTemplateInstance;

                    // Find floor tilemap layer
                    var tilemaps = RoomTemplateUtilsGrid2D.GetTilemaps(roomTemplateInstance);
                    var floor = tilemaps.Single(x => x.name == "Floor").gameObject;

                    // Add floor collider
                    AddFloorCollider(floor);
                }
            }

            private void AddFloorCollider(GameObject floor)
            {
                var tilemapCollider2D = floor.AddComponent<TilemapCollider2D>();
                #if UNITY_2023_2_OR_NEWER
                    tilemapCollider2D.compositeOperation = Collider2D.CompositeOperation.Merge;
                #else
                    tilemapCollider2D.usedByComposite = true;
                #endif

                var compositeCollider2d = floor.AddComponent<CompositeCollider2D>();
                compositeCollider2d.geometryType = CompositeCollider2D.GeometryType.Polygons;
                compositeCollider2d.isTrigger = true;
                compositeCollider2d.generationType = CompositeCollider2D.GenerationType.Manual;

                floor.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }

        #endregion
    }

    internal class CurrentRoomDetectionDocs2
    {
        #region codeBlock:2d_currentRoomDetection_postProcessing_2

        public class CurrentRoomDetectionPostProcessing : DungeonGeneratorPostProcessingGrid2D
        {
            public override void Run(DungeonGeneratorLevelGrid2D level)
            {
                foreach (var roomInstance in level.RoomInstances)
                {
                    var roomTemplateInstance = roomInstance.RoomTemplateInstance;

                    // Find floor tilemap layer
                    var tilemaps = RoomTemplateUtilsGrid2D.GetTilemaps(roomTemplateInstance);
                    var floor = tilemaps.Single(x => x.name == "Floor").gameObject;

                    // Add floor collider
                    AddFloorCollider(floor);

                    // Add current room detection handler
                    floor.AddComponent<CurrentRoomDetectionTriggerHandler>();
                }
            }

            private void AddFloorCollider(GameObject floor)
            {
                /* ... */
            }
        }

        #endregion
    }

    internal class CurrentRoomDetectionDocs3
    {
        #region codeBlock:2d_currentRoomDetection_postProcessing_3

        public class CurrentRoomDetectionPostProcessing : DungeonGeneratorPostProcessingGrid2D
        {
            public override void Run(DungeonGeneratorLevelGrid2D level)
            {
                foreach (var roomInstance in level.RoomInstances)
                {
                    var roomTemplateInstance = roomInstance.RoomTemplateInstance;

                    // Find floor tilemap layer
                    // Add floor collider

                    // Add the room manager component
                    var roomManager = roomTemplateInstance.AddComponent<CurrentRoomDetectionRoomManager>();
                    roomManager.RoomInstance = roomInstance;

                    // Add current room detection handler
                }
            }

            private void AddFloorCollider(GameObject floor)
            {
                /* ... */
            }
        }

        #endregion
    }
}