# ProceduralLevelGenerator-Unity

This project is a Unity plugin for procedural generation of 2D dungeons and aims to give game designers a **complete control** over generated levels. It combines procedural generation and **handmade room templates** to generate levels with a **feeling of consistency**. Under the hood, the plugin uses my .NET [procedural level generator](https://github.com/OndrejNepozitek/ProceduralLevelGenerator).

Similar approaches are used in games like [**Enter the Gungeon**](https://www.boristhebrave.com/2019/07/28/dungeon-generation-in-enter-the-gungeon/) or [**Dead Cells**](https://www.indiedb.com/games/dead-cells/news/the-level-design-of-a-procedurally-generated-metroidvania).

## See the documentation and examples [here](https://ondrejnepozitek.github.io/ProceduralLevelGenerator-Unity/docs/introduction).

## Current state of the plugin

This is an alpha version of the improved version 2 of the plugin. The current state is not ideal yet, but I think that it is better to use v2 if you are a new user. If you already use v1.x, there are some breaking changes in v2 so you have to think about whether to migrate or not (I would recommend to do so). You can find the latest v1.x release [here](https://github.com/OndrejNepozitek/ProceduralLevelGenerator-Unity/tree/v1.0.3).

I am also working on a **PRO version** of the plugin that will be paid and will contain additional features like **platformers generator**, performance benchmarking tools, more control over the inputs for the generator, simple **fog of war**, more example scenes and other.

## Features

- **Complete control over the structure of generated level.** Instead of generating completely random dungeons, you specify how many rooms you want and how they should be connected and the algorithm generates levels that follow exactly that structure.
- **Complete control over the look of individual rooms.** You can draw room templates using Unity built-in Tilemap feature. You can use all available tools (brushes, rule tiles, etc.) to design room templates.
- **Rooms either directly connected by doors or connected by corridors.** You can choose to either connect rooms by corridors or directly via doors.
- **Easy to customize.** The plugin is ready to be customized and extended.
- **Supports Unity 2018.4 and newer**.
- **2 example scenes included.**

## Limitations
- **Alpha version.** There may be some **breaking changes** in the API.
- **Some inputs are too hard for the generator.** You need to follow some guidelines in order to achieve good performance.
- **Not suitable for large levels.** The generator usually works best for levels with less than 30 rooms.
- **Not everything can be configured via editor.** You need to have programming knowledge in order to generate anything non-trivial.

## Workflow 

### 1. Draw rooms and corridors

![](https://ondrejnepozitek.github.io/ProceduralLevelGenerator-Unity/img/v2/room_templates_multiple.png)

### 2. Prepare the structure of the level

![](https://ondrejnepozitek.github.io/ProceduralLevelGenerator-Unity/img/v2/examples/example1_level_graph2.png)

### 3. Generate levels

![](https://ondrejnepozitek.github.io/ProceduralLevelGenerator-Unity/img/v2/generated_levels_multiple.png)

## Examples

![](https://ondrejnepozitek.github.io/ProceduralLevelGenerator-Unity/img/original/example1_result1.png)

![](https://ondrejnepozitek.github.io/ProceduralLevelGenerator-Unity/img/original/example1_result_reallife1.png)

![](https://ondrejnepozitek.github.io/ProceduralLevelGenerator-Unity/img/original/example2_result1.png)

![](https://ondrejnepozitek.github.io/ProceduralLevelGenerator-Unity/img/original/example2_result_reallife1.png)

## Terms of use

The plugin can be used in bot commercial and non-commerical projects but **cannot be redistributed or reselled**. If you want to include this plugin in your own asset, please contact me and we will figure that out.


