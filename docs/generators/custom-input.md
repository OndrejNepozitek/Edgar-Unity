---
title: (PRO) Custom input
---

In the free version of the asset, the input for the generator is fixed. That means that we create a level graph in the GUI and give it directly to the generator. However, there are situations where we might want to alter the level graph. For example, we may want to add a secret room that is connected to a random room in the level.

In this tutorial, we will learn how to implement custom inputs in order to have more control over the input for the generator.

## ```LevelGraph``` and ```LevelDescription```

The first thing that we need to understand is the difference between ```LevelGraph``` and ```LevelDescription``` classes. If you are reading this tutorial, you probably know what is a level graph. It is a collection of rooms and connections and it describes the high-level structure of generated levels. With each level graph is associated an instance of the ```LevelGraph``` scriptable object. 

However, level graph are not directly given to the generator as an input. First, we need to convert the ```LevelGraph``` to an instance of the ```LevelDescription``` class. The reason for that is that level graphs are made primarily for the GUI editor and we need to convert them to a real graph data structure.

Both ```LevelGraph``` and ```LevelDescription``` revolve around rooms and connections. The following code should demonstrate the basic API of both classes and how to convert one to the other one:

    public class CustomInputExample : DungeonGeneratorInputBase
    {
        public LevelGraph LevelGraph;
        public bool UseCorridors;

        protected override LevelDescription GetLevelDescription()
        {
            var levelDescription = new LevelDescription();

            // Go through rooms in the level graph and add them to the level description
            foreach (var room in LevelGraph.Rooms)
            {
                levelDescription.AddRoom(room, GetRoomTemplates(room));
            }

            // Go through connections in the level graph
            foreach (var connection in LevelGraph.Connections)
            {
                // If corridors are enabled, add corridor connection
                if (UseCorridors)
                {
                    // Create a room for the corridor
                    var corridorRoom = ScriptableObject.CreateInstance<Room>();
                    corridorRoom.Name = "Corridor";


                    levelDescription.AddCorridorConnection(connection, corridorRoom, GetCorridorRoomTemplates());
                }
                // Else connect the rooms directly
                else
                {
                    levelDescription.AddConnection(connection);
                }
            }

            return levelDescription;
        }

        /// <summary>
        /// Gets room templates for a given room.
        /// </summary>
        private List<GameObject> GetRoomTemplates(RoomBase room)
        {
            // Get room templates from a given room
            var roomTemplates = room.GetRoomTemplates();

            // If the list is empty, we use the defaults room templates from the level graph
            if (roomTemplates == null || roomTemplates.Count == 0)
            {
                var defaultRoomTemplates = LevelGraph.DefaultIndividualRoomTemplates;
                var defaultRoomTemplatesFromSets = LevelGraph.DefaultRoomTemplateSets.SelectMany(x => x.RoomTemplates);

                // Combine individual room templates with room templates from room template sets
                return defaultRoomTemplates.Union(defaultRoomTemplatesFromSets).ToList();
            }

            return roomTemplates;
        }

        /// <summary>
        /// Gets corridor room templates.
        /// </summary>
        private List<GameObject> GetCorridorRoomTemplates()
        {
            var defaultRoomTemplates = LevelGraph.CorridorIndividualRoomTemplates;
            var defaultRoomTemplatesFromSets = LevelGraph.CorridorRoomTemplateSets.SelectMany(x => x.RoomTemplates);

            return defaultRoomTemplates.Union(defaultRoomTemplatesFromSets).ToList();
        }
    }

## Custom input implementation