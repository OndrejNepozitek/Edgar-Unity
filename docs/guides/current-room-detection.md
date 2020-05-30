---
title: Current room detection
---

import { Image, Gallery, GalleryImage } from "@theme/Gallery";

In this guide, we will learn how to detect when a player enters or leaves a room. We can use this information to keep track of the currently active room or we can for example spawn enemies when the player enters a room.

<Image src="img/v2/guides/current_room_detection/result.png" caption="Information about the current room are displayed in the top-left corner." />

> **Note:** All files from this example can be found at *ProceduralLevelGenerator/Examples/CurrentRoomDetection*.

## Setup

Our plan is the following:
- Add a **trigger collider** to the **floor tilemap** layer of individual room templates
- Add a **RoomManager** component that will react to the **player entering or leaving the room**
- Add a *handler* that will react to the **OnTriggerEnter2D** and **OnTriggerExit2D** events and delegate that to the room manager
- *(Optional)* Add a **GameManager** component that will display the **currently active room**

We will use the room templates from [Example 1](../examples/example-1) and the following level graph:

<Image src="img/v2/guides/current_room_detection/result.png" caption="The level graph that is used in this guide. Each room has a unique name so that we can easily recognize if our implementation works or not." />

## Floor collider

We will use a trigger collider attached to the floor layer of individual room templates to detect when a player enters or leaves a room. There are at least two ways of adding this collider:

1. We can [override the default structure of tilemaps](../guides/room-template-customization) in order to add the floor collider to each room template right after it is created.
2. We can use a custom post processing script to add the floor collider to each room after a level is generated.

An advantage of the first approach is that Unity does not have to recompute the colliders every time a level is generated. An advantage of the second approach is that it is more flexible and easier to experiment with because we can just write a simple post processing script a do not have to modify any room templates. We decided to use the second approach because we use room template from [Example 1](../examples/example-1) and they do not have any floro colliders.

Below you can see the post processing code that is needed to add this floor collider to each of the rooms in a generated level.

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

## Trigger handler

When we have our floor colliders enabled, we must be able to do something when the player triggers the collider. We will create a very simple component that will be added the floor tilemap layer. This component will have two methods - **OnTriggerEnter2D** and **OnTriggerExit2D** - and inside these methods we will call the room manager that we will create in the next step.

    public class CurrentRoomDetectionTriggerHandler : MonoBehaviour
    {
        private CurrentRoomDetectionRoomManager roomManager;

        public void Start()
        {
            roomManager = transform.parent.parent.gameObject.GetComponent<CurrentRoomDetectionRoomManager>();
        }

        public void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.gameObject.tag == "Player")
            {
                roomManager?.OnRoomEnter(otherCollider.gameObject);
            }
        }

        public void OnTriggerExit2D(Collider2D otherCollider)
        {
            if (otherCollider.gameObject.tag == "Player")
            {
                roomManager?.OnRoomLeave(otherCollider.gameObject);
            }
        }
    }

We have to add this component to each room in the generated level. We will modify our post-processing logic to look like this:

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

                // Add current room detection handler
                floor.AddComponent<CurrentRoomDetectionTriggerHandler>();
            }
        }

        protected void AddFloorCollider(GameObject floor) { ... }
    }

## Room manager

We could handle all the logic inside the handler that we created in the previous step. However, it might be better to have a central place where all the logic regarding individual rooms takes place. Therefore, we will create a simple room manager component with two methods - **OnRoomEnter** and **OnRoomLeave** which will be called by the handler from the previous step.

    public class CurrentRoomDetectionRoomManager : MonoBehaviour
    {
        /// <summary>
        /// Room instance of the corresponding room.
        /// </summary>
        public RoomInstance RoomInstance;

        /// <summary>
        /// Gets called when a player enters the room.
        /// </summary>
        /// <param name="player"></param>
        public void OnRoomEnter(GameObject player)
        {
            Debug.Log($"Room enter. Room name: {RoomInstance.Room.Name}, Room template: {RoomInstance.RoomTemplatePrefab.name}");
        }

        /// <summary>
        /// Gets called when a player leaves the room.
        /// </summary>
        /// <param name="player"></param>
        public void OnRoomLeave(GameObject player)
        {
            Debug.Log($"Room leave {RoomInstance.Room.Name}");
        }
    }

Again, we will use our post-processing logic to add this room manager to each room in the level. Moreover, we will also set the `RoomInstance` field so that the room manager has access to all the information about the room.

    public class CurrentRoomDetectionPostProcess : DungeonGeneratorPostProcessBase
    {
        public override void Run(GeneratedLevel level, LevelDescription levelDescription)
        {
            foreach (var roomInstance in level.GetRoomInstances())
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

        protected void AddFloorCollider(GameObject floor) { ... }
    }

## Game manager

The last thing that we can do is to add a game manager that will keep track of in which room the player is currently located. A straightforward solution would be to simply keep track of which room was entered most recently. However, there is a catch. The player collider might be quite large so it is possible that it collides with two neighbouring rooms at once. That means that if the player goes just slightly to the next room and then back to the previous room, `OnTriggerEnter2D` will not be called because the player did not leave the room.

The solution is quite simple. We will set the new room to be active only when the player completely leaves the current room. An example implementation can be seen in the game manager class that is included in the folder with the example.