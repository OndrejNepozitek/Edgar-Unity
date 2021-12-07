---
title: Dungeon generator
---

import { Image, Gallery, GalleryImage } from "@theme/Gallery";

## Minimal setup

- Add the **Dungeon Generator Component** to any Game Object in the Scene.
- Assign your level graph to the **Level Graph** field.
- Hit the **Generate dungeon** button or enable **Generate on start** and enter play mode

<Image src="img/v2/generators/dungeon_generator_inspector.png" caption="Dungeon generator runner" width="500px" />

## Configuration

#### Input config

- **Level Graph** - Level graph that should be used. Must not be null.
- **Use corridors** - Whether corridors should be used between neighbouring rooms. If enabled, corridor room templates must be provided in the level graph.

#### Generator config

- **Root Game Object** - Game Object to which the generated level will be attached. New Game Object will be created if null.
- **Timeout** - How long (in milliseconds) should we wait for the algorithm to generate a level. We may sometimes create an input that is too hard for the algorithm so it is good to stop after some amount of time with an error.
- **Repeat Mode Override** - Whether to override the repeat mode of individual room templates.
    - **No override** - Nothing is overridden, keep repeat modes from room templates.
    - **Allow repeat** - All room templates may repeat in generated levels.
    - **No immediate** - Neighboring room must have different room templates.
    - **No repeat** - All rooms must have different room templates.

> **Note:** If you provide too few room templates, they may repeat in generated levels even if you choose the **No immediate** or **No repeat** options. To make sure that the repeat mode is satisfied, please provide enough room templates to choose from.

- **Minimum Room Distance** - The minimum distance between non-neighbouring rooms.
    - If equal to **0** - walls from one room can occupy the same tiles as walls from a different room.
    - If equal to **1** (default) - walls from different rooms can be next to each other but not on top of each other.
    - If equal to **2** - there must be at least one empty tile between walls of different rooms. (This is good for when using rule tiles and weird things are happening.)

> **Note:** Higher values of *Minimum Room Distance* may negatively affect the performance of the generator. Moreover, with very short corridor, it might even be impossible to generate a level with a high value of this parameter.

#### Post processing config

Please see the [Post-processing](../generators/post-process) page to find detailed information about this configuration.

- **Initialize Shared Tilemaps** - Whether to initialize tilemaps that will hold the generated level.
- **Tilemap Layers Handler** - Which tilemap layers handler should be used to initialize shared tilemaps. Uses the `DungeonTilemapLayersHandler` if not set.
- **Tilemap Material** - Material that will be used in Tilemap Renderers of shared tilemaps. This is useful, for example, for lights. If left null, the default material will be used.
- **Copy Tiles To Shared Tilemaps** - Whether to copy tiles from individual room template to the shared tilemaps.
- **Center Grid** - Whether to move the level so that its centre is approximately at (0,0). Useful for debugging in Scene view in the editor.
- **Disable Room Template Renderers** - Whether to disable tilemap renderers of individual rooms, useful only when *Copy Tiles To Shared Tilemaps* is enabled.
- **Disable Room Template Colliders** - Whether to disable tilemap colliders of individual rooms, useful only when *Copy Tiles To Shared Tilemaps* is enabled.

> **TODO:** Link to page with tilemap layers.

#### Other config

- **Use Random Seed** - Whether to use a random seed for each new generated level. 
- **Random Generator Seed** - Random generator seed that will be used when **Use Random Seed** is disabled. Useful for debugging.
- **Generate On Start** - Whether to generate a new level when play mode is entered.

## Call the generator from a script

It is very simple to call the generator from a script:

1. Get the `DungeonGenerator` component from the game object that holds the generator
2. Call the `Generate()` method

Example:

    var generator = GameObject.Find("Dungeon Generator").GetComponent<DungeonGenerator>();
    generator.Generate();

> **Note:** The `Generate()` method blocks the main Unity thread, so the game may freeze while the dungeon is generated. The PRO version comes with an implementation that uses coroutines to make sure that the games does not freeze.

### (PRO) With coroutines

If we do not want to block the main Unity thread when the level is generating, we can use a coroutine. There are two ways of doing that.

The simple approach uses only built-in Unity coroutines and works like this:

    var generator = GameObject.Find("Dungeon Generator").GetComponent<DungeonGenerator>();
    StartCoroutine(generator.GenerateCoroutine());

There is one problem with the simple approach - coroutines cannot really handle exceptions. So if there is some problem with the generator or with custom post-processing logic, the coroutine just dies and we are not able to do any cleanup. Therefore, I implemented a smarter coroutine that lets us handle any errors. Example usage:

    using System.Collections;
    using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator;
    using ProceduralLevelGenerator.Unity.Pro;
    using UnityEngine;

    public class CoroutineWithDataExampleAdvanced : MonoBehaviour
    {
        public void Start()
        {
            var generator = GameObject.Find("Dungeon Generator").GetComponent<DungeonGenerator>();
            StartCoroutine(GeneratorCoroutine(generator));
        }

        private IEnumerator GeneratorCoroutine(DungeonGenerator generator)
        {
            // Start the smart coroutine
            // StartSmartCoroutine is a custom extension method of MonoBehaviour, be sure to use the ProceduralLevelGenerator.Unity.Pro namespace
            var generatorCoroutine = this.StartSmartCoroutine(generator.GenerateCoroutine());

            // Wait until the smart coroutine is completed
            // Make sure to yield return the Coroutine property and not the generator coroutine itself!!
            yield return generatorCoroutine.Coroutine;

            // Check if the coroutine was successful
            if (generatorCoroutine.IsSuccessful)
            {
                Debug.Log("Level generated!");
            }
            // If there were any errors, we can access the Exception object
            // Or we can also rethrow the exception by calling generatorCoroutine.ThrowIfNotSuccessful();
            else
            {
                Debug.LogError("There was an error when generating the level!");
                Debug.LogError(generatorCoroutine.Exception.Message);
            }
        }
    }
