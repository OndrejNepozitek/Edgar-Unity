---
title: Post-processing
---

After a level is generated, we may often want to run some additional logic like spawning enemies, etc. This can be achieved by providing your own post-processing logic that will be called after the level is generated and provided with information about the level. 

To better understand how the generator works, we will first describe which post-processing is done by the generator itself and then provide ways to extend this behaviour and provide your own logic. You can skip right to [Custom post-processing](./post-process.md#custom-post-processing) if that is what you are looking for.

## Built-in post-processing steps

#### 0. Instantiate room template with correct positions

This is not actually a post-processing as it happens in the generator stage of the whole generator and cannot be disabled. At this point, the generator knows the final position and room template for each room in the level. The generator goes through the rooms and instantiates the corresponding room template game object and moves it to its correct position. If we disabled all the other post-processing steps, we would get a bunch of correctly positioned rooms, but there would often be weird overlap between the rooms.

#### 1. Initialize shared tilemaps

In this step, the generator initializes the structure of shared tilemaps to which we will copy individual rooms in the next step. These tilemaps will contain all the tiles in the level. If you provided your own *Tilemap Layers Handler*, this is the time it gets called.

#### 2. Copy rooms to shared tilemaps

In this step, the generator copies individual rooms to shared tilemaps. If we use corridors, it is important to first copy other rooms and only then corridors. By doing so, corridors will make holes into the walls of other rooms, so we can go from one room to another.

#### 3. Center grid

In this step, the whole level is moved in a way that its centre ends up at (0,0). This step is only used to make it easier to go through multiple generated levels without having to move the camera around.

#### 4. Disable room template renderers

At this point, we display both tiles from shared tilemaps and tiles from individual room template game objects that we instantiated in the step 0. Therefore, we have to disable all tilemap renderers from individual room templates. 

You may think why do not we just disable the whole room template. The reason for that is that there may be some additional game objects like lights, enemies, etc. and we do not want to lose that.

#### 5. Disable room template colliders 

The last step is very similar to the previous step. At this point, there are colliders from individual room templates that would prevent us from going from one room to another. We keep only the colliders that are set to trigger because these may be useful for example for [Current room detection](../guides/current-room-detection.md).

## Custom post-processing

There are currently 2 ways of implementing custom processing-logic: you either implement a custom component or use a *ScriptableObject*. I would recommend starting with a custom component as it is a bit easier, any only use *ScriptableObjects* if you need some of their benefits.

> **Note:** Previously (before *v2.0.0-beta.0*), it was only possible to create custom post-processing logic using *ScriptableObjects*. But that process is relatively tedious: you have to add the `CreateAssetMenu` attribute (which I never remember) and then create an instance of that *ScriptableObject*. Therefore, I also made it possible to use a `MonoBehaviour` component and just attach it to the generator game object.

### Using a `MonoBehaviour` component

The first approach is to create a class that inherits from the `DungeonGeneratorPostProcessingComponentGrid2D` class (which in turn inherits from `MonoBehaviour`). This class expects you to override the `void Run(DungeonGeneratorLevelGrid2D level)` method where you should place your post-processing logic.

<ExternalCode name="2d_customPostProcessingComponent" />

When you have your implementation ready, go to the scene where you have the generator and attach this component to the game object with the generator. If you now run the generator, your post-processing code should be called.

### Using a `ScriptableObject`

The second approach is to create a class that inherits from `DungeonGeneratorPostProcessingGrid2D` (which in turn inherits from `ScriptableObject`). And because the base class is a *ScriptableObject*, you need to add the `CreateAssetMenu` attribute, so you are able to create an instance of that *ScriptableObject*. This class expects you to override the `void Run(DungeonGeneratorLevelGrid2D level)` method where you should place your post-processing logic.

<ExternalCode name="2d_customPostProcessing" />

When you have your implementation ready, you now have to create an instance of that ScriptableObject by right-clicking in the project view and then <Path path="2d:Examples/Docs/My custom post-processing" />. And the last step is to drag and drop this ScriptableObject in the *Custom post process tasks* section of the dungeon generator.

<Image src="2d/examples/example1/custom_post_process.png" caption="Add the ScriptableObject to the Custom post process tasks array" />

<FeatureUsage id="custom-post-processing" />

### (PRO) Priority callbacks