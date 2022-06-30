---
title: Dungeon generator
---

## Minimal setup

1. Create an empty game object in the scene (name it however you like, I usually use *Dungeon Generator*)
2. Add the **Dungeon Generator (Gid3D)** component to that game object
3. Assign your *GeneratorSettings* to the **Generator Settings** field
4. Assign your level graph to the **Level Graph** field
5. Hit the **Generate dungeon** button or enable **Generate on start** and enter play mode

<Image src="3d/generator_setup/component.png" caption="Dungeon generator component" width="500px" />

## Configuration

#### Input config (`FixedLevelGraphConfigGrid3D`) {#input-config}

- **Level Graph** - Level graph that should be used. Must not be null.
- **Use Corridors** - Whether corridors should be used between neighbouring rooms. If enabled, corridor room templates must be provided in the level graph.
- **Allow Rotation Override** - Use this field to override whether room templates can be rotated in generated levels.
- **Fix Elevations Inside Cycles** - How to handle level graph cycles and room templates with different door elevations. Find out more [here](../guides/different-elevations.md#option-1-avoid-elevation-changes-inside-cycles).

#### Generator config (`DungeonGeneratorConfigGrid3D`) {#generator-config}

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

- **Generator Settings** - Instance of the *GeneratorSettings* scriptable object.

#### Post-processing config (`PostProcessingConfigGrid3D`) {#post-processing-config}

- **Center Level** - Whether to move the level so that its centre is approximately at (0,0). Useful for debugging in the Scene view inside the editor.
- **Process Connectors And Blockers** - Whether door connectors and blockers should be added to the level
- **Add Connectors** - Which door connectors should be added. For every single door in the level, there are room templates on both sides of the door, which means that there possible 2 different door connectors that can be added. This dropdown controls which of those connectors are added to the level:
    - **Never** - No door connectors are added
    - **RoomsOnly** - Only connectors belonging to non-corridor rooms are used
    - **CorridorsOnly** - Only connectors belonging to corridor rooms are used
    - **RoomsAndCorridors** - All connectors are added
    - **PreferCorridors** - Corridor connectors are added unless there is no corridor between neighbouring rooms, in which case room connectors are used.

#### Other config (available directly on the generator class) {#other-config}

- **Use Random Seed** - Whether to use a random seed for each new generated level. 
- **Random Generator Seed** - Random generator seed that will be used when **Use Random Seed** is disabled. Useful for debugging.
- **Generate On Start** - Whether to generate a new level when play mode is entered.

### Change the configuration from a script

You can also easily change the configuration of the generator directly from a script as all the fields are exposed on the *DungeonGeneratorGrid3D* components.

## Call the generator from a script

> **Note:** This part of the guide lives in the 2D section of the documentation: [here](../../generators/dungeon-generator.md#call-the-generator-from-a-script)
>
> The concepts are the same in the 2D version, you just have to replace the `Grid2D` class name suffix with `Grid3D`. For example, `DungeonGeneratorGrid2D` becomes `DungeonGeneratorGrid3D`.

<Difference2D3D />