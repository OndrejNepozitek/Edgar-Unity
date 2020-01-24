using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    public class RoomManager : MonoBehaviour
    {
        public TheGungeonRoom Room;

        public bool Cleared = false;

        public List<GameObject> Doors = new List<GameObject>();

        public GameObject[] Enemies;

        public Collider2D FloorCollider;

        public void OnRoomEnter(Collider2D otherCollider)
        {
            Debug.Log($"{otherCollider.gameObject.name} enters {Room?.Type}");

            if (Cleared == false)
            {
                foreach (var door in Doors)
                {
                    door.SetActive(true);
                }

                var enemies = new List<GameObject>();

                for (int i = 0; i < 100; i++)
                {
                    var position = RandomPointInBounds(FloorCollider.bounds);
                    var enemyPrefab = Enemies[Random.Range(0, Enemies.Length)];
                    var enemyCollider = enemyPrefab.GetComponent<Collider2D>();

                    if (!IsPointWithinCollider(FloorCollider, position))
                    {
                        continue;
                    }

                    //Collider2D[] colliders = new Collider2D[10];
                    //var contactFilter = new ContactFilter2D();
                    //var collidersCount = Physics2D.OverlapCollider(enemyCollider, contactFilter.NoFilter(), colliders);

                    // TODO: handle better
                    //if (colliders.All(x => x == null || x.isTrigger))
                    //{
                    //    var enemy = Instantiate(enemyPrefab);
                    //    enemy.transform.position = position;
                    //}

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

                StartCoroutine(WaitBeforeOpeningDoors());
            }
        }

        public static bool IsPointWithinCollider(Collider2D collider, Vector2 point)
        {
            return (collider.ClosestPoint(point) - point).sqrMagnitude < Mathf.Epsilon * Mathf.Epsilon;
        }

        public static Vector3 RandomPointInBounds(Bounds bounds) {
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

            foreach (var door in Doors)
            {
                door.SetActive(false);
            }
        }

        public void OnRoomLeave(Collider2D otherCollider)
        {
            Debug.Log($"{otherCollider.gameObject.name} leaves {Room?.Type}");
        }
    }
}