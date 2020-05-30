using System.Linq;
using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ProceduralLevelGenerator.Unity.Examples.CurrentRoomDetection.Scripts
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Current room detection/Post process", fileName = "CurrentRoomDetectionPostProcess")]
    public class CurrentRoomDetectionPostProcess : DungeonGeneratorPostProcessBase
    {
        public override void Run(GeneratedLevel level, LevelDescription levelDescription)
        {
            foreach (var roomInstance in level.GetRoomInstances())
            {
                var roomTemplateInstance = roomInstance.RoomTemplateInstance;

                // Find floor tilemap layer
                var tilemaps = RoomTemplateUtils.GetTilemaps(roomTemplateInstance);
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

        protected void AddFloorCollider(GameObject floor)
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
}