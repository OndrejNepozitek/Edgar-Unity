---
id: pipelinePayload
title: Pipeline payload
---

Pipeline payload is an object that flows through the pipeline. Each pipeline task can modify the payload and all the following tasks in the payload will have access to that modified object.

## Basic idea

The basic idea is that we use interfaces to define which data we need to have on the payload. Below you can see the basic payload interface that provides access to the tilemaps of the generated dungeon together with the GameObject that holds them.

```csharp
/// <summary>
/// Basic generator pipeline payload.
/// </summary>
public interface IGeneratorPayload
{
    /// <summary>
    /// Gameobject that holds dungeon tilemaps and possibly other game objects.
    /// </summary>
    GameObject GameObject { get; set; }

    /// <summary>
    /// Tilemaps of the generated dungeon.
    /// </summary>
    List<Tilemap> Tilemaps { get; set; }
}
```

 We can use this interface to directly populate the tilemaps with our dungeon tiles in one task, and then for example add monsters in another task. However, some dungeon generators may provide additional information about the dungeon. For example, the generator used in this plugin generates dungeons consisting of rooms, so we may be interested in where are individual rooms placed. For this purpose, there is the `IGraphBasedGeneratorPayload` interface.

 Following interfaces are provided in the plugin (you can see them defined [here](https://github.com/OndrejNepozitek/ProceduralLevelGenerator-Unity/tree/master/Assets/ProceduralLevelGenerator/Scripts/GeneratorPipeline/Payloads/Interfaces)):
- `IGeneratorPayload` - provides tilemaps of the dungeon and the GameObject that holds them
- `IGraphBasedGeneratorPayload` - provides access to the input and the output of the dungeon generator algorithm
- `INamedTilemapsPayload` - provides named tilemaps if you do not want to access them by their indices
- `IRandomGeneratorPayload` - provides access to an instace of the `Random` class

### Why interfaces?

Why do we need interfaces? Why not just work with a class that has all the data that we need? Well, **in theory**, interfaces should give us more flexibility. We can decide to use a slightly different generator and replace only a small part of the payload and use our old pipeline tasks that were not affected by the change.

Another reason is that with interfaces, I can provide some basic pipeline tasks without forcing you to use an existing payload class. You simply make sure that your payload implements all the needed interfaces and the tasks should *just work*.

## Extending payloads

If you want to extend the payload, you have basically two options. The first option is to inherit from the `PipelinePayload` class and add any functionality that you need. This approach is good if you want to just add new functionality and want to keep original functionality intact. The second option is to create your own payload class from scratch without inheriting. 

## Payload initialization

Before we give the payload to the first task in the pipeline, we need to create its instance. For this purpose, you need to provide an implementation of the abstract `AbstractPayloadInitializer` class and assign that instance to the *PayloadInitializer* field of the dungeon generator pipeline.

 This class has only a single method - `public abstract object InitializePayload();` - which should return a new instace of the payload. There is a default initializer provided (`PayloadInitializer` class) that prepares tilemaps and creates an instace of the random number generator.

