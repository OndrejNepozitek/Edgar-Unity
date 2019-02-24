---
id: pipelineTasks
title: Pipeline tasks
---

Pipeline tasks contain all the logic of the procedural dungeon generator. They are reusable and configurable components which we compose together to make the whole generator work.

## Creating pipeline tasks
There are currently 2 ways to create pipeline tasks - the first way is simpler but less flexible and the second way is the exact opposite - slightly more complex but also more flexible.

## Inheriting from `PipelineTask`

The first way to create a pipeline task is to create a class that inherits from the `PipelineTask` class below. As you can see below, the `PipelineTask` class is abstract and you have to provide an implementation of the `Process` method.

```csharp
/// <summary>
/// Base class for pipeline tasks. Used in simpler scenarios.
/// </summary>
/// <typeparam name="TPayload">Type of the payload</typeparam>
public abstract class PipelineTask<TPayload> : PipelineItem, IPipelineTask<TPayload> 
    where TPayload : class
{
    /// <summary>
    /// Payload object.
    /// </summary>
    public TPayload Payload { get; set; }

    /// <summary>
    /// Method containing all the logic of the task.
    /// </summary>
    /// <remarks>
    /// When this method is called, the Payload property is already set.
    /// </remarks>
    public abstract void Process();
}
```

> **Note:** `PipelineTask<TPayload>` inherits from `PipelineItem` and that class inherits from `ScriptableObject`. So every child of `PipelineTask<TPayload>` is a `ScriptableObject`.

### Example

Imagine that we have payload defined as follows:

```csharp
public class Payload {
    public int Number { get; set; }
}
```

Let's create a pipeline task that will subtract a number from the `Payload.Number` property and we want that number to be configurable in Editor:

```csharp
[CreateAssetMenu(menuName = "Example tasks/Subtract task", fileName = "SubtractTask")]
public class SubtractTask : PipelineTask<Payload>
{
    public int ToSubtract;

    public override void Process()
    {
        Payload.Number -= ToSubtract;
    }
}
```

You can see that I added the `CreateAssetMenu` attribute because we want to be able to create an instance of the task in the Editor. The rest of the code should be quite self-explanatory.

To use this task, we first create an instance of the `SubtractTask` ScriptableObject and then drag that instance to the *Generator pipeline script*.

### Pros and cons

The biggest *disadvantage* is that we have to specify exactly which payload class we want to use because Unity does not allow us to have generic ScriptableObjects. However, this may not be a problem if you do not plan to ever switch the implementation of the payload.

> **Note:** It is not exactly true that we have to specify which payload *class* we want to use. We can also use an *interface*. But what if we want to access properties from multiple interfaces? We can probably work around that but it is not very nice.

## Inheriting from `PipelineConfig`

The second way to create a pipeline task is more flexible. Instead of creating a single class, we will need 2 classes - one will be a non-generic configuration class and the second one will be a generic task. We will inherit from the following two classes.

```csharp
/// <summary>
/// Base class for configs of pipeline tasks.
/// </summary>
/// <remarks>
/// Should be used together with <seealso cref="ConfigurablePipelineTask{TPayload,TConfig}"/>.
/// </remarks>
public abstract class PipelineConfig : PipelineItem
{

}
```

```csharp
/// <summary>
/// Base class for configurable pipeline tasks.
/// </summary>
/// <typeparam name="TPayload">Type of payload.</typeparam>
/// <typeparam name="TConfig">Type of config.</typeparam>
public abstract class ConfigurablePipelineTask<TPayload, TConfig> : IConfigurablePipelineTask<TPayload, TConfig> 
    where TConfig : PipelineConfig 
    where TPayload : class
{
    /// <summary>
    /// Payload object.
    /// </summary>
    public TPayload Payload { get; set; }

    /// <summary>
    /// Config object.
    /// </summary>
    public TConfig Config { get; set; }

    /// <summary>
    /// Method containing all the logic of the task.
    /// </summary>
    /// <remarks>
    /// When this method is called, both Payload and Config properties are already set.
    /// </remarks>
    public abstract void Process();
}
```

> **Note:** `PipelineConfig` inherits from `PipelineItem` and that class inherits from `ScriptableObject`. So every child of `PipelineConfig` is a `ScriptableObject`.

### Example

Imagine that we want to implement a task that chooses a random size for our dungeon and that we already have 2 payload interfaces - `IDungeonPayload` and `IRandomGeneratorPayload`. Payload implementation may look like this:

```csharp
public interface IDungeonPayload
{
    int Size { get; set; }
}

public interface IRandomGeneratorPayload
{
    Random Random { get; set; }
}

public class Payload : IRandomGeneratorPayload, IDungeonPayload
{
    public Random Random { get; set; }

    public int Size { get; set; }
}
```

First we create a config for our task. We want to choose size from a predefined range.

```csharp
[CreateAssetMenu(menuName = "Example tasks/Random size task", fileName = "RandomSizeTask")]
public class RandomSizeConfig : PipelineConfig
{
    public int MinSize;

    public int MaxSize;
}
```

And now we create the actual task.

```csharp
public class RandomSizeTask<TPayload> : ConfigurablePipelineTask<TPayload, RandomSizeConfig> 
    where TPayload : class, IRandomGeneratorPayload, IDungeonPayload
{
    public override void Process()
    {
        Payload.Size = Payload.Random.Next(Config.MinSize, Config.MaxSize);
    }
}
```

Note a few things regarding the task:
- it is a generic class
- it inherits from the `ConfigurablePipelineTask<TPayload, RandomSizeConfig>` class - with this approach the payload is always an open generic type while the config is always the config class that we created in the previous step
- it has access to `Payload` and `Config` properties
    - `Payload` is of a generic `TPayload` type that must implement both `IDungeonPayload` and `IRandomGeneratorPayload` interfaces
    - `Config` is of the `RandomSizeConfig` type

To use this task, we first create an instance of the `RandomSizeConfig` ScriptableObject and then drag that instance to the *Generator pipeline script*. We do not do anything with the `RandomSizeTask` - the pipeline runner uses some *reflection magic* to find a corresponding task to each config class.

### Pros and cons

The biggest advantage of this approach is that we still have a non-generic ScriptableObject that we can use to configure the task in Editor, but we also have a **generic task** that uses generic type constraints to enforce that the payload type implements given interfaces.