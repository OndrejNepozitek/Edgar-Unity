using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Enter The Gungeon/Gungeon setup task", fileName = "GungeonSetupConfig")]
	public class GungeonSetupConfig : PipelineConfig
    {
        public GameObject[] Enemies;
    }

	public class GungeonSetupTask<TPayload> : ConfigurablePipelineTask<TPayload, GungeonSetupConfig> 
		where TPayload : class, IGeneratorPayload, IGraphBasedGeneratorPayload, IRandomGeneratorPayload
	{
		public override void Process()
        {
            foreach (var roomInstance in Payload.GeneratedLevel.GetAllRoomInstances())
            {
                var room = roomInstance.Room as GungeonRoom;
                var roomTemplateInstance = roomInstance.RoomTemplateInstance;

                // TODO: how to properly handle this block?
                roomTemplateInstance.SetActive(true);
                foreach (var tilemapRenderer in roomTemplateInstance.GetComponentsInChildren<TilemapRenderer>())
                {
                    tilemapRenderer.enabled = false;
                }
                roomTemplateInstance.transform.Find("Walls").gameObject.GetComponent<TilemapCollider2D>().enabled = false;

                // Get spawn position if Entrance
                if (room.Type == RoomType.Entrance)
                {
                    var spawnPosition = roomTemplateInstance.transform.Find("SpawnPosition");
                    var player = GameObject.Find("Player");
                    player.transform.position = spawnPosition.position;
                }

                var roomManager = roomTemplateInstance.GetComponent<RoomManager>();
                if (roomManager != null)
                {
                    roomManager.Room = room;
                    roomManager.Enemies = Config.Enemies;

                    if (room.Type != RoomType.Corridor)
                    {
                        foreach (var door in roomInstance.Doors)
                        {
                            var corridorRoom = door.ConnectedRoom;
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
            }
        }
    }
}