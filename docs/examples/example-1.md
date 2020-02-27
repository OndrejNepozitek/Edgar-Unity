---
title: Example 1
---

import { Image, Gallery, GalleryImage } from "@theme/Gallery";

In this tutorial, we will use [this tileset](https://pixel-poem.itch.io/dungeon-assetpuck) by [@pixel_poem](https://twitter.com/pixel_poem). Be sure to check their work out if you like the tileset. We will not care about room decorations - we will use just basic walls, floor and  door tiles. 

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/original/example1_result1.png" caption="Simple example" />
    <GalleryImage src="img/original/example1_result_reallife1.png" caption="Real-life example" />
</Gallery>

> **Note:** All files from this example can be found at *ProceduralLevelGenerator/Examples/Example1*.

## Simple example

The goal is to create two basic rectangular room remplates of different sizes and a room template for both horizontal and vertical corridors. We will use the smaller room template for our dead-end rooms and the bigger room template for other rooms.

### Basic rooms romplates

There should be nothing hard about the design of the two rectangular room templates. We use the *simple doors mode configured* to door length 1 and corner distance 2. We need corner distance 2 in order to easily connect corridors.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/original/example1_room1.png" caption="Bigger room" />
    <GalleryImage src="img/original/example1_room2.png" caption="Smaller room" />
</Gallery>

### Corridors

Corridors are very simple with this tileset. We use the *specific positions* doors mode to choose the two possible door positions. And because corridors are by default placed after non-corridor rooms, these room templates just work without the need of any scripting.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/original/example1_corridor_horizontal.png" caption="Horizontal corridor" />
    <GalleryImage src="img/original/example1_corridor_vertical.png" caption="Vertical corridor" />
</Gallery>

We just need to make sure that we do not allow door positions of non-corridor rooms that are closer than 2 tiles from corners. Below you can see what would happen otherwise. It is possible to allow that but we would have to create a pipeline task that would fix such cases.

<Image src="img/original/example1_wrong_corridor.png" caption="Incorrect corridor connection" />

### Level graph

With only two room templates for non-corridor rooms, we must think about which level graphs are possible to lay out and which are not. For example, using only the bigger room template, the algorithm is not able to lay out cycles of lengths 3 and 5 because there simply is not any way to form these cycles with such room templates. But cycles of length 4 are possible so that is what we do here.

<Image src="img/original/example1_level_graph1.png" caption="Level graph" />

### Results

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/original/example1_result2.png" caption="Example result" />
    <GalleryImage src="img/original/example1_result3.png" caption="Example result" />
</Gallery>

## Real-life example

To create something that is closer to a real-life example, we will add more rooms to the level graph, add special room templates for spawn and boss rooms and also two more corridor room templates.

### Spawn and boss rooms

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/original/example1_spawn.png" caption="Spawn room template" />
    <GalleryImage src="img/original/example1_boss.png" caption="Boos room template" />
</Gallery>

### Additional room template

Even for ordinary rooms, we can have non-rectangular room templates.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/original/example1_room3.png" caption="Additional room tempalte" />
</Gallery>

### Longer corridors

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/original/example1_corridor_horizontal2.png" caption="Longer horizontal corridor" />
    <GalleryImage src="img/original/example1_corridor_vertical2.png" caption="Longer vertical corridor" />
</Gallery>

### Level graph

So the goal is to have more rooms than in the simple example and also a spawn room and a boss room. You can see one such level graph below.

<Image src="img/original/example1_level_graph2.png" caption="Level graph" />

You can also see that with corridors of different lengths a more room templates to choose from, we can now have cycles of various sizes. The boss and spawn rooms have assigned only a single room template.

### Results

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/original/example1_result_reallife2.png" caption="Example result" />
    <GalleryImage src="img/original/example1_result_reallife3.png" caption="Example result" />
</Gallery>
