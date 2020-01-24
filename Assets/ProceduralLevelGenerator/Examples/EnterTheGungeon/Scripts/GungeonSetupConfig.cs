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
		where TPayload : class, IGeneratorPayload, IGraphBasedGeneratorPayload, INamedTilemapsPayload, IRandomGeneratorPayload, IRoomToIntMappingPayload<Room>
	{
		public override void Process()
        {
            foreach (var roomInfo in Payload.Layout.GetAllRoomInfo())
            {
                // TODO: this is kinda strange
                var node = roomInfo.GeneratorData.Node;
                var room = (GungeonRoom) Payload.RoomToIntMapping.GetByValue(node);

                var roomGameObject = roomInfo.Room;
                roomGameObject.SetActive(true);
                var roomManager = roomGameObject.GetComponent<RoomManager>();
                
                foreach (var tilemapRenderer in roomGameObject.GetComponentsInChildren<TilemapRenderer>())
                {
                    tilemapRenderer.enabled = false;
                }

                roomGameObject.transform.Find("Walls").gameObject.GetComponent<TilemapCollider2D>().enabled = false;

                if (room.Type == RoomType.Entrance)
                {
                    var spawnPosition = roomGameObject.transform.Find("SpawnPosition");
                    var player = GameObject.Find("Player");
                    player.transform.position = spawnPosition.position;
                }

                if (roomManager != null)
                {
                    roomManager.Room = room;
                    roomManager.Enemies = Config.Enemies;

                    if (room.Type != RoomType.Corridor)
                    {
                        foreach (var doorInfo in roomInfo.Doors)
                        {
                            // TODO: too complex to get neighboring rooms
                            var corridorRoom = Payload.Layout.GetAllRoomInfo().First(x => x.GeneratorData.Node == doorInfo.ConnectedRoom);
                            var corridorGameObject = corridorRoom.Room;
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