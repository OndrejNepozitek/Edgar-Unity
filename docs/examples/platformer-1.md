---
title: (PRO) Platformer 1
---

In this tutorial, we will use [this tileset](https://www.kenney.nl/assets/abstract-platformer) by [@KenneyNL](https://twitter.com/KenneyNL) to create basic platformer levels. Be sure to check out their work if you like the tileset. We will not care about room decorations very much - the goal of this tutorial is to demonstrate all the basic steps needed to create platformer levels.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="2d/examples/platformer1/result1.png" caption="Example result" />
    <GalleryImage src="2d/examples/platformer1/result2.png" caption="Example result" />
</Gallery>

> **Note:** I recommend reading [Example 1](example-1.md) first as this is a bit harder to set up, and I will not repeat the basics here.

## Limitations

If this is your first time reading about procedural platformers, please see the [Limitations section](../generators/platformer-generator#limitations) of the Platformer generator page.

## Room templates

Below you can see a few of the room templates that were created for this example.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="2d/examples/platformer1/start.png" caption="Start" />
    <GalleryImage src="2d/examples/platformer1/goal.png" caption="Goal" />
    <GalleryImage src="2d/examples/platformer1/room2.png" caption="Basic room" />
    <GalleryImage src="2d/examples/platformer1/room3.png" caption="Basic room" />
    <GalleryImage src="2d/examples/platformer1/room6.png" caption="Basic room" />
    <GalleryImage src="2d/examples/platformer1/room8.png" caption="Basic room" />
</Gallery>

## Doors and corridors

Even though there are no real corridors used in generated levels, we use the corridor feature to make sure that neighbouring rooms do not share walls.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="2d/examples/platformer1/corridor_horizontal.png" caption="Horizontal corridor" />
    <GalleryImage src="2d/examples/platformer1/corridor_vertical.png" caption="Vertical corridor" />
</Gallery>

Moreover, there is a small problem with doors because there are no background tiles inside room templates. And that means that when the corridor rooms are placed over non-corridor rooms, all the walls remain there, and it is not possible to go from one room to another (as can be seen in the image below). We usually do not have this problem because when there are background tiles, they are placed over walls and everything is working.

<Image src="2d/examples/platformer1/no_holes_between_rooms.png" caption="There are no holes between individual rooms because we have no background tile in room templates." />

The solution is quite simple. We have to create a simple post-processing task that goes through all door positions and deletes all the door tiles.

<ExternalCode name="2d_platformer1_postProcessing" />

> **Note:** The term *doors* is used throughout the text but in this context it simply means a connection between two rooms.

## Level graph

<Image src="2d/examples/platformer1/level_graph.png" caption="Level graph" />

## Results

<Gallery cols={2} fixedHeight>
    <GalleryImage src="2d/examples/platformer1/result3.png" caption="Example result" />
    <GalleryImage src="2d/examples/platformer1/result4.png" caption="Example result" />
</Gallery>