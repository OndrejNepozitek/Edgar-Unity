using Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.PipelineTasks;
using Assets.ProceduralLevelGenerator.Scripts.Pro;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts.Tasks
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Enter The Gungeon/Post process", fileName = "Gungeon Post Process")]
    public class GungeonPostProcessTask : DungeonGeneratorPostProcessBase
    {
        public GameObject[] Enemies;

        protected override void Run(GeneratedLevel level, LevelDescription levelDescription)
        {
            // Instance may be null in Editor mode
            GungeonGameManager.Instance?.ResetFogOfWar();

            foreach (var roomInstance in Payload.GeneratedLevel.GetRoomInstances())
            {
                var room = (GungeonRoom) roomInstance.Room;
                var roomTemplateInstance = roomInstance.RoomTemplateInstance;

                var roomManager = roomTemplateInstance.GetComponent<GungeonRoomManager>();

                if (roomManager == null)
                {
                    roomManager = roomTemplateInstance.AddComponent<GungeonRoomManager>();
                }

                foreach (var roomEnterHandler in roomTemplateInstance.GetComponentsInChildren<RoomEnterHandler>())
                {
                    roomEnterHandler.Setup();
                }

                if (roomManager != null)
                {
                    roomManager.Enemies = Enemies;

                    if (room.Type != GungeonRoomType.Corridor)
                    {
                        foreach (var door in roomInstance.Doors)
                        {
                            var corridorRoom = door.ConnectedRoomInstance;
                            var corridorGameObject = corridorRoom.RoomTemplateInstance;
                            var doorsGameObject = corridorGameObject.transform.Find("Door")?.gameObject;

                            if (doorsGameObject != null)
                            {
                                doorsGameObject.SetActive(false);
                                roomManager.Doors.Add(doorsGameObject);
                            }
                        }
                    }
                }

                // Get spawn position if Entrance
                if (room.Type == GungeonRoomType.Entrance)
                {
                    var spawnPosition = roomTemplateInstance.transform.Find("SpawnPosition");
                    var player = GameObject.FindWithTag("Player");
                    player.transform.position = spawnPosition.position;

                    // Instance may be null in Editor mode 
                    GungeonGameManager.Instance?.RevealRoom(roomInstance);
                }
            }
        }
    }
}