---
title: Dungeon generator
---

## Minimal setup

- Add the **Dungeon Generator Component** to any Game Object in the Scene.
- Assign your level graph to the **Level Graph** field.
- Hit the **Generate dungeon** button or enable **Generate on start** and enter play mode

<Image src="2d/generators/dungeon_generator_inspector.png" caption="Dungeon generator runner" width="500" />

## Configuration

#### Input config (`FixedLevelGraphConfigGrid2D`) {#input-config}

- **Level Graph** - Level graph that should be used. Must not be null.
- **Use corridors** - Whether corridors should be used between neighbouring rooms. If enabled, corridor room templates must be provided in the level graph.

#### Generator config (`DungeonGeneratorConfigGrid2D`) {#generator-config}

- **Root Game Object** - Game Object to which the generated level will be attached. New Game Object will be created if null.
- **Timeout** - How long (in milliseconds) should we wait for the algorithm to generate a level. We may sometimes create an input that is too hard for the algorithm, so it is good to stop after some amount of time with an error.
- **Repeat Mode Override** - Whether to override the repeat mode of individual room templates.
    - **No override** - Nothing is overridden, keep repeat modes from room templates.
    - **Allow repeat** - All room templates may repeat in generated levels.
    - **No immediate** - Neighbouring room must have different room templates.
    - **No repeat** - All rooms must have different room templates.

> **Note:** If you provide too few room templates, they may repeat in generated levels even if you choose the **No immediate** or **No repeat** options. To make sure that the repeat mode is satisfied, please provide enough room templates to choose from.

- **Minimum Room Distance** - The minimum distance between non-neighbouring rooms.
    - If equal to **0** - walls from one room can occupy the same tiles as walls from a different room.
    - If equal to **1** (default) - walls from different rooms can be next to each other but not on top of each other.
    - If equal to **2** - there must be at least one empty tile between walls of different rooms. (This is good for when using rule tiles and weird things are happening.)

> **Note:** Higher values of *Minimum Room Distance* may negatively affect the performance of the generator. Moreover, with very short corridor, it might even be impossible to generate a level with a high value of this parameter.

- **Room Template Prefab Mode** - Controls how are room templates instantiated when in the Editor.
    - **Instantiate** - Instantiate room templates using `Object.Instantiate()` - the default behaviour.
    - **InstantiatePrefab** - Instantiate room templates using `PrefabUtility.InstantiatePrefab()`. This option keeps all prefab references intact.
    - **InstantiatePrefabAndUnpackRoot** - Instantiate room templates using `PrefabUtility.InstantiatePrefab()`, but unpack the root object using `PrefabUtility.UnpackPrefabInstance()`. This option keep all the prefab references except for the root object. Useful if you need to alter some room templates in the Editor after a level is generated.

#### Post-processing config (`PostProcessingConfigGrid2D`) {#post-processing-config}

Please see the [Post-processing](../generators/post-process.md) page to find detailed information about this configuration.

- **Initialize Shared Tilemaps** - Whether to initialize tilemaps that will hold the generated level.
- **Tilemap Layers Handler** - Which tilemap layers handler should be used to initialize shared tilemaps. Uses the `DungeonTilemapLayersHandler` if not set.
- **Tilemap Material** - Material that will be used in Tilemap Renderers of shared tilemaps. This is useful, for example, for lights. If left null, the default material will be used.
- **Copy Tiles To Shared Tilemaps** - Whether to copy tiles from individual room templates to the shared tilemaps.
- **Center Grid** - Whether to move the level so that its centre is approximately at (0,0). Useful for debugging in Scene view in the editor.
- **Disable Room Template Renderers** - Whether to disable tilemap renderers of individual rooms, useful only when *Copy Tiles To Shared Tilemaps* is enabled.
- **Disable Room Template Colliders** - Whether to disable tilemap colliders of individual rooms, useful only when *Copy Tiles To Shared Tilemaps* is enabled.

#### Other config (available directly on the generator class) {#other-config}

- **Use Random Seed** - Whether to use a random seed for each new generated level. 
- **Random Generator Seed** - Random generator seed that will be used when **Use Random Seed** is disabled. Useful for debugging.
- **Generate On Start** - Whether to generate a new level when play mode is entered.

### Change the configuration from a script

You can also easily change the configuration of the generator directly from a script:

<ExternalCode name="2d_generator_changeConfiguration" />

## Call the generator from a script

It is very simple to call the generator from a script:

1. Get the `DungeonGenerator` component from the game object that holds the generator
2. Call the `Generate()` method

Example:

<ExternalCode name="2d_generator_run" />

> **Note:** The `Generate()` method blocks the main Unity thread, so the game may freeze while the dungeon is generated. The PRO version comes with an implementation that uses coroutines to make sure that the games does not freeze.

### (PRO) With coroutines

If we do not want to block the main Unity thread when the level is generating, we can use a coroutine. There are two ways of doing that.

The simple approach uses only built-in Unity coroutines and works like this:

<ExternalCode name="2d_generator_runCoroutines" />

There is one problem with the simple approach - coroutines cannot handle exceptions. So if there is some problem with the generator or with custom post-processing logic, the coroutine just dies, and we are not able to do any clean-up. Therefore, I implemented a smarter coroutine that lets us handle any errors. Example usage:

<ExternalCode name="2d_generator_runCoroutinesAdvanced" />

## Controlling the seed of the generator

In the majority of procedural level generators, there is something called the **seed of the generator**. In Edgar, it is a number that controls the randomness of the generator. If you use the same seed twice, the generator will produce the same level.

By default, Edgar automatically generates a new random seed for you each time you run the generator. That means that you should see a different level each time you press the *Generate* button or call the generator from a script. If you want to change this behaviour, there are two properties that you can play with. First, there is the `UseRandomSeed` property that controls whether the generator should pick a random seed for you. The second property is the `RandomSeed` property which controls which seed is used when `UseRandomSeed` is set to false.

> **Note:** As an excercise, you can try playing with the seed yourself. Head to the dungeon generator component and uncheck the `UseRandomSeed` property. Next, in the `RandomSeed` field, write any number you want, e.g. 42. Now, try generating a few levels. You should see that each generated level is the same. You can also check that console which should show that the seed that you picked is used to generate the level. If you change the `RandomSeed` value, a different level should be generated.

You might be asking yourself in which scenario is controlling the seed useful. Here are some examples:

- If you are debugging some problem that you have, it might be useful to fix the seed so that you can easily replicate the problem and later check if it is really fixed. If the problem manifests only in some of the generated levels, it might be otherwise hard to address it if you need to generate multiple levels to find one with the problem.
- If you want to implement a save and load system, you might need to be able to generate the exact same level twice. More info is in the [How to implement save and load system?](../other/faq.md#how-to-implement-save-and-load-system) section of the FAQ page.
- If you want to send the same level to multiple players in a multiplayer game, the simplest solution might be sending them the seed instead and generating the level on their computers. That way, each player will get the same level. More info is in the [Send the same level to multiple players (in a multiplayer game)](../other/faq.md#send-the-same-level-to-multiple-players-in-a-multiplayer-game) section of the FAQ page.

If you have read this documentation page carefully, you should now know how to change the seed configuration from a script and also how to call the generator from a script. With that knowledge, you should be able to fully control the seed and the behaviour of the generator.