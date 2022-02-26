using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Edgar.Unity.Examples.CurrentRoomDetection
{
    [CreateAssetMenu(menuName = "Edgar/Examples/Current room detection/Post-processing", fileName = "CurrentRoomDetectionPostProcessing")]

    #region codeBlock:2d_currentRoomDetection_postProcessing

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

                // Add the room manager component
                var roomManager = roomTemplateInstance.AddComponent<CurrentRoomDetectionRoomManager>();
                roomManager.RoomInstance = roomInstance;

                // Add current room detection handler
                floor.AddComponent<CurrentRoomDetectionTriggerHandler>();
            }
        }

        private void AddFloorCollider(GameObject floor)
        {
            var tilemapCollider2D = floor.AddComponent<TilemapCollider2D>();
            tilemapCollider2D.usedByComposite = true;

            var compositeCollider2d = floor.AddComponent<CompositeCollider2D>();
            compositeCollider2d.geometryType = CompositeCollider2D.GeometryType.Polygons;
            compositeCollider2d.isTrigger = true;
            compositeCollider2d.generationType = CompositeCollider2D.GenerationType.Manual;

            floor.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    #endregion
}