using Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.PipelineTasks;
using Assets.ProceduralLevelGenerator.Scripts.Pro;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts.Tasks
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Enter The Gungeon/Post process", fileName = "GungeonPostProcess")]
    public class GungeonPostProcessTask : DungeonGeneratorPostProcessBase
    {
        public GameObject[] Enemies;

        protected override void Run(GeneratedLevel level, LevelDescription levelDescription)
        {
            // TODO: improve later
            level.RootGameObject.AddComponent<LevelInfo>();
            level.RootGameObject.GetComponent<LevelInfo>().Level = level;

            foreach (var roomInstance in Payload.GeneratedLevel.GetAllRoomInstances())
            {
                var room = (GungeonRoom) roomInstance.Room;
                var roomTemplateInstance = roomInstance.RoomTemplateInstance;

                var roomManager = roomTemplateInstance.GetComponent<RoomManager>();

                if (roomManager == null)
                {
                    roomManager = roomTemplateInstance.AddComponent<RoomManager>();
                }

                if (roomManager != null)
                {
                    // TODO: improve later
                    roomManager.RoomInstance = roomInstance;

                    roomManager.Room = room;
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
                }
            }
        }
    }
}