using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Rooms;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    public class GungeonRoomManager : MonoBehaviour
    {
        public bool Cleared;

        public bool Revealed;

        public List<GameObject> Doors = new List<GameObject>();

        public GameObject[] Enemies;

        public bool EnemiesSpawned;

        public Collider2D FloorCollider;

        private RoomInstance roomInstance;

        private GungeonRoom room;

        public void Start()
        {
            roomInstance = GetComponent<RoomInfo>()?.RoomInstance;
            room = roomInstance?.Room as GungeonRoom;
        }

        public void OnRoomEnter(Collider2D otherCollider)
        {
            GungeonGameManager.Instance.SetCurrentRoomType(room.Type);

            if (!Revealed && roomInstance != null)
            {
                Revealed = true;
                GungeonGameManager.Instance.RevealRoom(roomInstance);
            }

            if (Cleared == false && EnemiesSpawned == false && ShouldSpawnEnemies())
            {
                EnemiesSpawned = true;

                CloseDoors();
                SpawnEnemies();

                StartCoroutine(WaitBeforeOpeningDoors());
            }
        }

        private void SpawnEnemies()
        {
            var enemies = new List<GameObject>();

            for (var i = 0; i < 100; i++)
            {
                var position = RandomPointInBounds(FloorCollider.bounds);
                var enemyPrefab = Enemies[Random.Range(0, Enemies.Length)];

                if (!IsPointWithinCollider(FloorCollider, position))
                {
                    continue;
                }

                // TODO: handle trigger better
                if (Physics2D.OverlapCircleAll(position, 1).All(x => x.isTrigger))
                {
                    var enemy = Instantiate(enemyPrefab);
                    enemy.transform.position = position;
                    enemies.Add(enemy);
                }

                if (enemies.Count >= 8)
                {
                    break;
                }
            }
        }

        private void CloseDoors()
        {
            foreach (var door in Doors)
            {
                door.SetActive(true);
            }
        }

        private void OpenDoors()
        {
            foreach (var door in Doors)
            {
                door.SetActive(false);
            }
        }

        public static bool IsPointWithinCollider(Collider2D collider, Vector2 point)
        {
            return (collider.ClosestPoint(point) - point).sqrMagnitude < Mathf.Epsilon * Mathf.Epsilon;
        }

        public static Vector3 RandomPointInBounds(Bounds bounds)
        {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }

        private IEnumerator WaitBeforeOpeningDoors()
        {
            yield return new WaitForSeconds(5);

            Cleared = true;
            OpenDoors();
        }

        public void OnRoomLeave(Collider2D otherCollider)
        {
            // Debug.Log($"{otherCollider.gameObject.name} leaves {Room?.Type}");
        }

        public bool ShouldSpawnEnemies()
        {
            return room.Type == GungeonRoomType.Normal || room.Type == GungeonRoomType.Hub || room.Type == GungeonRoomType.Boss;
        }
    }
}