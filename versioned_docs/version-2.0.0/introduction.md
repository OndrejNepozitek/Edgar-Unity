---
id: introduction
title: Introduction
---

This project is a Unity plugin for procedural generation of 2D dungeons and aims to give game designers a **complete control** over generated levels. It combines procedural generation and **handmade room templates** to generate levels with a **feeling of consistency**. Under the hood, the plugin uses my .NET [procedural level generator](https://github.com/OndrejNepozitek/ProceduralLevelGenerator).

Similar approaches are used in games like [**Enter the Gungeon**](https://www.boristhebrave.com/2019/07/28/dungeon-generation-in-enter-the-gungeon/) or [**Dead Cells**](https://www.indiedb.com/games/dead-cells/news/the-level-design-of-a-procedurally-generated-metroidvania).

## Features

- **Complete control over the structure of generated level.** Instead of generating completely random dungeons, you specify how many rooms you want and how they should be connected, and the algorithm generates levels that follow exactly that structure.
- **Complete control over the look of individual rooms.** You can draw room templates using Unity built-in Tilemap feature. You can use all available tools (brushes, rule tiles, etc.) to design room templates.
- **Rooms either directly connected by doors or connected by corridors.** You can choose to either connect rooms by corridors or directly via doors.
- **Easy to customize.** The plugin is ready to be customized and extended.
- **Supports Unity 2018.4 and newer.**
- **Multiple example scenes included.**

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