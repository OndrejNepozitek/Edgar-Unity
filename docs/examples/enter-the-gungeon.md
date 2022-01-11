---
title: (PRO) Enter the Gungeon
---

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';
import { Video } from "@theme/Video";
import useBaseUrl from "@docusaurus/useBaseUrl";

In this tutorial, we will look into how to generate levels similar to what we can see in [Enter the Gungeon](https://store.steampowered.com/app/311690/Enter_the_Gungeon/). We will use [this tileset](https://pixel-poem.itch.io/dungeon-assetpuck) by [@pixel_poem](https://twitter.com/pixel_poem) - be sure to check out their work if you like the tileset.

> **Disclaimer:** We are in no way affiliated with the authors of the **Enter the Gungeon** game and this plugin is not used in the game. This is only a case study about how to use this plugin to create something similar to what is done in that game.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="2d/examples/gungeon/result1.png" caption="Example result" />
    <GalleryImage src="2d/examples/gungeon/result2.png" caption="Example result" />
</Gallery>

<Video src="videos/gungeon_example_video.mp4" style={{ marginBottom: 15, marginTop: -15 }} />

> **Note:** All files from this example can be found at *Edgar/Examples/EnterTheGungeon*.

## Room templates

> **Note:** If you want to add some more room templates, be sure to use the *Create* menu (*Examples/Enter the Gungeon/Room template*) or duplicate one of the existing room templates.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="2d/examples/gungeon/room_templates/entrance.png" caption="Entrance" />
    <GalleryImage src="2d/examples/gungeon/room_templates/hub1.png" caption="Hub" />
    <GalleryImage src="2d/examples/gungeon/room_templates/normal5.png" caption="Normal" />
    <GalleryImage src="2d/examples/gungeon/room_templates/reward.png" caption="Reward" />
    <GalleryImage src="2d/examples/gungeon/room_templates/shop.png" caption="Shop" />
    <GalleryImage src="2d/examples/gungeon/room_templates/secret.png" caption="Secret" />
</Gallery>

## Level graphs

In Enter the Gungeon, they use multiple level graphs for each stage of the game.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="2d/examples/gungeon/level_graph_2.png" caption="Stage 1 level graph" /> 
    <GalleryImage src="2d/examples/gungeon/level_graph_1.png" caption="Stage 2 level graph" /> 
</Gallery>

### Custom rooms and connections

In the level graph above, we used custom room and connection types. We use this feature to add additional data to rooms and connection and also to change how they are displayed in the editor.

#### Rooms

Each room in Enter the Gungeon has its type - there are rooms with enemies, treasure rooms, shops, etc. We use a custom room implementation to add the `GungGungeonRoomType Type` field to each room. Moreover, we use different colours to distinguish different types of rooms in the level graph editor.

#### Connections

Some corridors in Enter the Gungeon are locked and can be unlocked only from the other side of the door. This is usually used to force the player to go through a loop that ends with a treasure or shop room and the door then serves as a shortcut to get back to the main path. We use a custom connection implementation to add the `bool IsLocked` field. If the door is locked, we use red colour to draw the line between the two rooms.

<Tabs
defaultValue="room"
values={[
{label: 'GungeonRoom.cs', value: 'room'},
{label: 'GungeonConnection.cs', value: 'connection'},
]}>
<TabItem value="room">

```
public class GungeonRoom : RoomBase
    {
        public GungeonRoomType Type;

        public override List<GameObject> GetRoomTemplates()
        {
            // We do not need any room templates here because they are resolved based on the type of the room.
            return null;
        }

        public override string GetDisplayName()
        {
            // Use the type of the room as its display name.
            return Type.ToString();
        }

        public override RoomEditorStyle GetEditorStyle(bool isFocused)
        {
            var style = base.GetEditorStyle(isFocused);

            var backgroundColor = style.BackgroundColor;

            // Use different colors for different types of rooms
            switch (Type)
            {
                case GungeonRoomType.Entrance:
                    backgroundColor = new Color(38/256f, 115/256f, 38/256f);
                    break;

                /* ... */
            }

            style.BackgroundColor = backgroundColor;

            // Darken the color when focused
            if (isFocused)
            {
                style.BackgroundColor = Color.Lerp(style.BackgroundColor, Color.black, 0.7f);
            }

            return style;
        }
    }
```

  </TabItem>
  <TabItem value="connection">

```
    public class GungeonConnection : Connection
    {
        // Whether the corresponding corridor should be locked
        public bool IsLocked;

        public override ConnectionEditorStyle GetEditorStyle(bool isFocused)
        {
            var style = base.GetEditorStyle(isFocused);

            // Use red color when locked
            if (IsLocked)
            {
                style.LineColor = Color.red;
            }

            return style;
        }
    }
```

  </TabItem>
</Tabs>

### Input setup task

We will use a [custom input setup task](generators/custom-input.md) because it gives us more control when working with the input. We will use it to choose a random level graph and add a random secret room. The base of the task is the same as in the [Dead Cells](examples/dead-cells.md#input-setup) example.

### Pick random level graph

Because we have multiple level graphs for each stage of the game, we want to choose the level graph randomly from the available options. The implementation is straightforward:

    public class GungeonInputSetupTask : DungeonGeneratorInputBase
    {
        [Range(1, 2)]
        public int Stage = 1;

        public LevelGraph[] Stage1LevelGraphs;

        public LevelGraph[] Stage2LevelGraphs;

        protected override LevelDescription GetLevelDescription()
        {
            // Pick random level graph
            var levelGraphs = Stage == 1 ? Stage1LevelGraphs : Stage2LevelGraphs;
            var levelGraph = levelGraphs.GetRandom(Payload.Random);
            GungeonGameManager.Instance.CurrentLevelGraph = levelGraph;

            /* ... */
        }
    }

Then we just assign level graphs to the two arrays. The last step is to control the current stage of the game. We can do that in the game manager before we generate a level:

    private IEnumerator GeneratorCoroutine(DungeonGenerator generator)
    {
        /* ... */

        // Configure the generator with the current stage number
        var inputTask = (GungeonInputSetupTask) generator.CustomInputTask;
        inputTask.Stage = Stage;

        /* ... */
    }

### Random secret rooms

Even though all the levels are primarily guided by hand-made level graphs, there is a bit of randomness included. When we set up the input for the algorithm, we roll a die to determine if we want to add a secret room to the level. We can add a `float SecretRoomChance` field to the input setup and configure this value directly in the generator inspector. In Enter the Gungeon, they also choose whether to connect the room to a dead-end room or to any rooms - this is controlled with `SecretRoomDeadEndChance`.

To add the secret room to the level, we first get all the rooms from the level description and randomly choose one of them to attach the secret room to. Then we have to do 3 things. First, we create an instance of the secret room - this corresponds to a room node in the level graph. Second, we create an instance of the connection between the two rooms - this corresponds to an edge in the level graph. And third, because we use corridors, we also need to create an instance of the corridor room that is between the two rooms.

> **Note:** Our secret rooms are not really secret as we do not hide them in any way. I may revisit this in the future to make them really secret.

<details><summary>Show code block</summary>
<div>

```
    public class GungeonInputSetupTask : DungeonGeneratorInputBase
    {
        public LevelGraph LevelGraph;

        public GungeonRoomTemplatesConfig RoomTemplates;

        // The probability that a secret room is added to the level
        [Range(0f, 1f)]
        public float SecretRoomChance = 0.9f;

        // The probability that a secret room is attached to a dead-end room
        [Range(0f, 1f)]
        public float SecretRoomDeadEndChance = 0.5f;

        protected override LevelDescription GetLevelDescription()
        {
            /* ... */

            // Add secret rooms
            AddSecretRoom(levelDescription);

            /* ... */
        }

        private void AddSecretRoom(LevelDescription levelDescription)
        {
            // Return early if no secret room should be added to the level
            if (Payload.Random.NextDouble() > SecretRoomChance) return;

            // Get the graphs of rooms
            var graph = levelDescription.GetGraph();

            // Decide whether to attach the secret room to a dead end room or not
            var attachToDeadEnd = Payload.Random.NextDouble() < SecretRoomDeadEndChance;

            // Find all the possible rooms to attach to and choose a random one
            var possibleRoomsToAttachTo = graph.Vertices.Cast<GungeonRoom>().Where(x =>
                (!attachToDeadEnd || graph.GetNeighbours(x).Count() == 1) && x.Type != GungeonRoomType.Entrance
            ).ToList();
            var roomToAttachTo = possibleRoomsToAttachTo[Payload.Random.Next(possibleRoomsToAttachTo.Count)];

            // Create secret room
            var secretRoom = ScriptableObject.CreateInstance<GungeonRoom>();
            secretRoom.Type = GungeonRoomType.Secret;
            levelDescription.AddRoom(secretRoom, RoomTemplates.GetRoomTemplates(secretRoom).ToList());

            // Prepare the connection between secretRoom and roomToAttachTo
            var connection = ScriptableObject.CreateInstance<GungeonConnection>();
            connection.From = roomToAttachTo;
            connection.To = secretRoom;

            // Connect the two rooms with a corridor
            var corridorRoom = ScriptableObject.CreateInstance<GungeonRoom>();
            corridorRoom.Type = GungeonRoomType.Corridor;
            levelDescription.AddCorridorConnection(connection, RoomTemplates.CorridorRoomTemplates.ToList(), corridorRoom);
        }
    }
```

</div>
</details>

## Room manager

In Enter the Gungeon, when a player visits a (combat-oriented) room for the first time, two things happen. First, all the doors to neighbouring rooms get closed and locked. And second, enemies are spawned. Only after all the enemies are defeated, the doors unlock.

<Video src="videos/gungeon_enter_room.mp4" />

<br />

> **Note:** The enemies in this example are very dumb - they just stand there and cannot be killed as there is no combat system implemented. Therefore, the doors open after some time even though enemies are still alive.

### Current room detection

The base of this setup is detecting when a player enters a room. We will use the same setup as we described in the [Current room detection](guides/current-room-detection.md) tutorial. That means that we have a floor collider that is set to trigger, and it informs `RoomManager` when the player enters a room.

### Enemies

We will use a very simple approach to a randomized spawn of enemies. We will use the floor collider that we set up in the previous step to get a random position inside the room.

The algorithm works as follows:

1. Get a random position inside floor collider bounds
2. Check if the position is actually inside the collider (there may be holes)
3. Check that there are no other colliders near the position
4. Pick a random enemy and instantiate it at the position

<details><summary>Show code block</summary>
<div>

```

    public class GungeonRoomManager : MonoBehaviour
    {
        /// <summary>
        /// Enemies that can spawn inside the room.
        /// </summary>
        public GameObject[] Enemies;

        /// <summary>
        /// Collider of the floor tilemap layer.
        /// </summary>
        public Collider2D FloorCollider;

        /* ... */

        private void SpawnEnemies()
        {
            EnemiesSpawned = true;

            var enemies = new List<GameObject>();
            var totalEnemiesCount = GungeonGameManager.Instance.Random.Next(4, 8);

            while(enemies.Count < totalEnemiesCount)
            {
                // Find random position inside floor collider bounds
                var position = RandomPointInBounds(FloorCollider.bounds, 1f);

                // Check if the point is actually inside the collider as there may be holes in the floor, etc.
                if (!IsPointWithinCollider(FloorCollider, position))
                {
                    continue;
                }

                // We want to make sure that there is no other collider in the radius of 1
                if (Physics2D.OverlapCircleAll(position, 0.5f).Any(x => !x.isTrigger))
                {
                    continue;
                }

                // Pick random enemy prefab
                var enemyPrefab = Enemies[Random.Range(0, Enemies.Length)];

                // Create an instance of the enemy and set position and parent
                var enemy = Instantiate(enemyPrefab);
                enemy.transform.position = position;
                enemy.transform.parent = roomInstance.RoomTemplateInstance.transform;
                enemies.Add(enemy);
            }
        }
    }
```

</div>
</details>

<br />

> **Note:** As the process of choosing enemy spawn points is random, we hope that the success rate is quite high, and we do not have to spend too much time on it. However, if we wanted to spawn too many enemies or there were too many holes in the collider, we could have problems with performance. In that case, it would be better to use a different approach.

### Doors

Our goal is to close neighbouring corridors with doors when the player enters the room and then open the doors when all the enemies are dead. The only slightly complex part is how to obtain the game objects that represent the doors. To make our lives easier, we added the doors directly to each corridor room template. That means that after the level is generated we just have to retrieve the doors from corridor room templates.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="2d/examples/gungeon/room_templates/ver5.png" caption="Vertical corridor" />
    <GalleryImage src="2d/examples/gungeon/room_templates/hor5.png" caption="Horizontal corridor" />
</Gallery>

We can do it like this:

1. Prepare a custom post-processing task
2. Go through all non-corridor rooms
4. Find all the corridors that are connected to the room
5. Get the door game object from each neighbouring corridor
6. Store all the doors in the room manager

When we have the game objects, we can simply activate them when the player enters the room and then deactivate them when enemies are dead. (Or just open the doors after 3 seconds because we do not have any combat implemented.)

<details><summary>Show code block</summary>
<div>

```

    public class GungeonPostProcessTask : DungeonGeneratorPostProcessBase
    {
        public GameObject[] Enemies;

        public override void Run(GeneratedLevel level, LevelDescription levelDescription)
        {
            /* ... */

            foreach (var roomInstance in level.GetRoomInstances())
            {
                var room = (GungeonRoom) roomInstance.Room;
                var roomTemplateInstance = roomInstance.RoomTemplateInstance;

                // Find floor tilemap layer
                var tilemaps = RoomTemplateUtils.GetTilemaps(roomTemplateInstance);
                var floor = tilemaps.Single(x => x.name == "Floor").gameObject;

                // Add current room detection handler
                floor.AddComponent<GungeonCurrentRoomHandler>();

                // Add room manager
                var roomManager = roomTemplateInstance.AddComponent<GungeonRoomManager>();
                
                if (room.Type != GungeonRoomType.Corridor)
                {
                    // Set enemies and floor collider to the room manager
                    roomManager.Enemies = Enemies;
                    roomManager.FloorCollider = floor.GetComponent<CompositeCollider2D>();

                    // Find all the doors of neighboring corridors and save them in the room manager
                    // The term "door" has two different meanings here:
                    //   1. it represents the connection point between two rooms in the level
                    //   2. it represents the door game object that we have inside each corridor
                    foreach (var door in roomInstance.Doors)
                    {
                        // Get the room instance of the room that is connected via this door
                        var corridorRoom = door.ConnectedRoomInstance;

                        // Get the room template instance of the corridor room
                        var corridorGameObject = corridorRoom.RoomTemplateInstance;

                        // Find the door game object by its name
                        var doorsGameObject = corridorGameObject.transform.Find("Door")?.gameObject;

                        // Each corridor room instance has a connection that represents the edge in the level graph
                        // We use the connection object to check if the corridor should be locked or not
                        var connection = (GungeonConnection) corridorRoom.Connection;

                        if (doorsGameObject != null)
                        {
                            // If the connection is locked, we set the Locked state and keep the game object active
                            // Otherwise we set the EnemyLocked state and deactivate the door. That means that the door is active and locked
                            // only when there are enemies in the room.
                            if (connection.IsLocked)
                            {
                                doorsGameObject.GetComponent<GungeonDoor>().State = GungeonDoor.DoorState.Locked;
                            }
                            else
                            {
                                doorsGameObject.GetComponent<GungeonDoor>().State = GungeonDoor.DoorState.EnemyLocked;
                                doorsGameObject.SetActive(false);
                            }
                            
                            roomManager.Doors.Add(doorsGameObject);
                        }
                    }
                }
            }
        }
    }
```

</div>
</details>

### Locked doors

The last thing that we have to handle are doors that should be locked even if there are no enemies. These doors are used to separate reward/shop rooms from other rooms and force the player to find a different path to the reward room. When the player discovers the reward room, all the neighbouring locked doors are unlocked.

## Fog of War

In this example, the [Fog of War](guides/fog-of-war.md) feature is enabled. For more information on how to set up the feature, please see the [documentation](guides/fog-of-war.md). In order to integrate the Fog of War into this example scene, I modified the current room detection script (`GungeonCurrentRoomHandler` class) to trigger the fog when a player enters a corridor room, and I also modified the `GungeonPostProcessTask` class to set up the fog after a level is generated.

> **Note:** The integration of the Fog of War effect into this example could be improved. I think that it looks better when the next room is revealed only after the player walks through the middle of a corridor and not right when he enters the corridor. Also, the integration with doors is not ideal - you can reveal rooms behind locked rooms if you go close to the door. I want to improve this in the future.

> **Note:** To disable the Fog of War effect, go to the main camera and disable the Fog of War component.

## Results

<Gallery cols={2} fixedHeight>
    <GalleryImage src="2d/examples/gungeon/result3.png" caption="Example result" />
    <GalleryImage src="2d/examples/gungeon/result4.png" caption="Example result" />
</Gallery>