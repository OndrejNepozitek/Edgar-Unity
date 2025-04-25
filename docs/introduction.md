---
id: introduction
title: Introduction
---

This project is a Unity plugin for procedural generation of 2D dungeons and aims to give game designers a **complete control** over generated levels. It combines procedural generation and **handmade room templates** to generate levels with a **feeling of consistency**. Under the hood, the plugin uses my .NET [procedural level generator](https://github.com/OndrejNepozitek/ProceduralLevelGenerator).

Similar approaches are used in games like [**Enter the Gungeon**](https://www.boristhebrave.com/2019/07/28/dungeon-generation-in-enter-the-gungeon/) or [**Dead Cells**](https://www.indiedb.com/games/dead-cells/news/the-level-design-of-a-procedurally-generated-metroidvania).

## Key features

- Procedural dungeon generator
- Describe the structure of levels with a graph of rooms and connections
- Control the appearance of rooms with handmade room templates
- Connect rooms either directly with doors or with short corridors
- Easy to customize with custom post-processing logic
- Supports Unity 2018.4 and newer
- Currently works only in 2D with experimental [3D support](/docs/3d/introduction/)
- Comprehensive documentation
- Multiple example scenes included

## PRO version

There are two versions of this asset - free version and [PRO version](https://url.ondrejnepozitek.com/edgar-docs). The free version contains the core functions of the generator and should be fine for simple procedural dungeons. The PRO version can be bought **[on the Unity Asset Store](https://url.ondrejnepozitek.com/edgar-docs)** and contains many additional features that you can see below. If you like this asset, please consider buying the PRO version to support the development.

- **Coroutines** - Call the generator as a coroutine so that the game does not freeze when generating a level ([docs](../generators/dungeon-generator#coroutines))
- **Custom rooms** - It is possible to add additional fields to rooms and connections in a level graph ([docs](../guides/room-template-customization))
- **Platformers** - Generator that is able to produce platformer levels ([docs](../generators/platformer-generator))
- **Isometric** - Simple example of isometric levels
- **Dead Cells** - Tutorial on how to generate levels that are similar to Dead Cells ([docs](../recipes/dead-cells))
- **Enter the Gungeon** - Tutorial on how to generate levels that are similar to Enter the Gungeon ([docs](../recipes/enter-the-gungeon))
- **Custom input** - Modify a level graph before it is used in the generator (e.g. add a random secret room) ([docs](../generators/custom-input))
- **Fog of War** - Hide rooms in a fog until they are explored by the player ([docs](../guides/fog-of-war))
- **Minimap support** - Simple to use minimap ([docs](../guides/minimap))
- **Door sockets** - Use door sockets to specify which doors are compatible ([docs](../guides/door-sockets))
- **Directed level graphs** - Use directed level graphs together with entrance-only and exit-only doors to have better control over generated levels ([docs](../guides/directed-level-graphs))

## Limitations
- **Some inputs are too hard for the generator.** You need to follow some guidelines in order to achieve good performance.
- **Not suitable for large levels.** The generator usually works best for levels with less than 30 rooms.
- **Not everything can be configured via editor.** You need to have programming knowledge in order to generate anything non-trivial.

## Workflow 

### 1. Draw rooms and corridors

<Gallery cols={4}>
    <Image src="2d/examples/example1/room1.png" />
    <Image src="2d/examples/example1/room2.png" />
    <Image src="2d/examples/example1/intro_spawn.png" />
    <Image src="2d/examples/example1/intro_boss.png" />
    <Image src="2d/examples/example1/intro_corridor_horizontal.png" />
    <Image src="2d/examples/example1/intro_corridor_vertical.png" />
    <Image src="2d/examples/example1/corridor_horizontal2.png" />
    <Image src="2d/examples/example1/corridor_vertical2.png" />
</Gallery>

### 2. Prepare the structure of the level

<Image src="2d/examples/example1/level_graph2.png" height={500} />

### 3. Generate levels

<Gallery cols={4}>
    <Image src="2d/examples/example1/result_reallife2.png" />
    <Image src="2d/examples/example1/result_reallife3.png" />
    <Image src="2d/examples/example1/result_reallife4.png" />
    <Image src="2d/examples/example1/result_reallife5.png" />
</Gallery>

## Examples

<Gallery>
    <Image src="2d/examples/example1/result_reallife2.png" caption="Example 1" />
    <Image src="2d/examples/example1/result_reallife1.png" caption="Example 1" />
    <Image src="2d/examples/example2/result1.png" caption="Example 2" />
    <Image src="2d/examples/example2/result_reallife1.png" caption="Example 2" />
</Gallery>

## Terms of use

The plugin can be used in both commercial and non-commercial projects but **cannot be redistributed or resold**. If you want to include this plugin in your own asset, please contact me, and we will figure that out.