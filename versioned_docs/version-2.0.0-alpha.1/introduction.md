---
id: introduction
title: Introduction
---

import { Image, Gallery, GalleryImage } from "@theme/Gallery";

This project is a Unity plugin for procedural generation of 2D dungeons and aims to give game designers a **complete control** over generated levels. It combines procedural generation and **handmade room templates** to generate levels with a **feeling of consistency**. Under the hood, the plugin uses my .NET [procedural level generator](https://github.com/OndrejNepozitek/ProceduralLevelGenerator).

Similar approaches are used in games like [**Enter the Gungeon**](https://www.boristhebrave.com/2019/07/28/dungeon-generation-in-enter-the-gungeon/) or [**Dead Cells**](https://www.indiedb.com/games/dead-cells/news/the-level-design-of-a-procedurally-generated-metroidvania).

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

<Gallery cols={4} fixedHeight>
    <GalleryImage src="img/v2/examples/example1_room1.png" />
    <GalleryImage src="img/v2/examples/example1_room2.png" />
    <GalleryImage src="img/original/example1_spawn.png" />
    <GalleryImage src="img/original/example1_boss.png" />
    <GalleryImage src="img/original/example1_corridor_horizontal.png" />
    <GalleryImage src="img/original/example1_corridor_vertical.png" />
    <GalleryImage src="img/v2/examples/example1_corridor_horizontal2.png" />
    <GalleryImage src="img/v2/examples/example1_corridor_vertical2.png" />
</Gallery>

### 2. Prepare the structure of the level

<Image src="img/v2/examples/example1_level_graph2.png" height={500} />

### 3. Generate levels

<Gallery cols={4} fixedHeight>
    <GalleryImage src="img/v2/examples/example1_result_reallife2.png" />
    <GalleryImage src="img/v2/examples/example1_result_reallife3.png" />
    <GalleryImage src="img/v2/examples/example1_result_reallife4.png" />
    <GalleryImage src="img/v2/examples/example1_result_reallife5.png" />
</Gallery>

## Examples

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/original/example1_result1.png" caption="Example 1" />
    <GalleryImage src="img/original/example1_result_reallife1.png" caption="Example 1" />
    <GalleryImage src="img/original/example2_result1.png" caption="Example 2" />
    <GalleryImage src="img/original/example2_result_reallife1.png" caption="Example 2" />
</Gallery>

## Terms of use

The plugin can be used in bot commercial and non-commerical projects but **cannot be redistributed or reselled**. If you want to include this plugin in your own asset, please contact me and we will figure that out.