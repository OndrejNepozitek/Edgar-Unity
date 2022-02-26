---
title: (PRO) Custom input
---

In the free version of the asset, the input for the generator is fixed. That means that we create a level graph in the GUI and give it directly to the generator. However, there are situations where we might want to alter the level graph. For example, we may want to add a secret room that is connected to a random room in the level.

In this tutorial, we will learn how to implement custom inputs in order to have more control over the input for the generator.

## ```LevelGraph``` and ```LevelDescription```

The first thing that we need to understand is the difference between ```LevelGraph``` and ```LevelDescription``` classes. If you are reading this tutorial, you probably know what is a level graph. It is a collection of rooms and connections, and it describes the high-level structure of generated levels. With each level graph is associated an instance of the ```LevelGraph``` scriptable object. 

However, level graphs are not directly given to the generator as an input. First, we need to convert the ```LevelGraph``` to an instance of the ```LevelDescription``` class. The reason for that is that level graphs are made primarily for the GUI editor, and we need to convert them to a real graph data structure.

Both ```LevelGraph``` and ```LevelDescription``` revolve around rooms and connections. The following code should demonstrate the basic API of both classes and how to convert one to the other one:

<ExternalCode name="2d_customInput_full" />

## Custom input implementation

Custom inputs are quite similar to [Custom post-processing](../generators/post-process.md) logic. We have to create a class that inherits from `DungeonGeneratorInputBase`. And because the base class is a ScriptableObject, we need to add the `CreateAssetMenu` attribute, so we are able to create an instance of that ScriptableObject. The `DungeonGeneratorInputBase` class has one abstract method that we need to implement - `LevelDescription GetLevelDescription()`:

<ExternalCode name="2d_customInput_simple" />

After we implement the logic, we have to create an instance of that ScriptableObject by right-clicking in the project view and <Path path="2d:Examples/Docs/My custom input" />. And the last step is to switch the *Input Type* in the generator inspector to *Custom Input* and drag and drop the ScriptableObject instance to the *Custom Input Task* field.

## Typical use cases

### Add rooms to the level graph

One typical use case is adding additional rooms (for example a random secret room) to an existing level graph. The workflow is usually as follows:

1. Create the static part of the level graph in the GUI
2. Create a custom input task with a public level graph field that we will assign our level graph to
3. Convert the `LevelGraph` to `LevelDescription` (as discussed above)
4. Create additional rooms and connect them to existing rooms in the level description

To make it easier to work with the graph of rooms and connections, `LevelDescription` has a `IGraph<RoomBase> GetGraph()` method to get the current graph of rooms. The graphs contain all the expected methods like getting all rooms or checking if two rooms are neighbours.

For an example of how can this be implemented, see the [Enter the Gungeon](../examples/enter-the-gungeon.md) example where we connect a secret room to a random room in the graph.

> **Note:** The graph which is returned by the `GetGraph()` method currently does not get updated when you modify the level description. You need to call the method again to get a new graph.

### Assign room templates automatically

Another typical use case is implementing custom logic for assigning room templates to individual rooms. For example, if we use [custom rooms](../basics/level-graphs.md#pro-custom-rooms-and-connections), we may want to assign room templates based on the type of the room instead of manually assigning room templates to individual rooms. This can be seen both in [Enter the Gungeon](../examples/enter-the-gungeon.md) and [Dead Cells](../examples/dead-cells.md) examples.

### Procedural graphs

It is also possible to have a completely procedural structure of levels by creating the whole level description on the fly without any static parts.