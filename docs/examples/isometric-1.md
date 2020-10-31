---
title: (PRO) Isometric 1
---

import { Image, Gallery, GalleryImage } from "@theme/Gallery";

In this tutorial, we will use [this tileset](https://www.kenney.nl/assets/isometric-prototype-tiles) by [@KenneyNL](https://twitter.com/KenneyNL) to create very basic isometric levels. Be sure to check their work out if you like the tileset. 

<Image src="img/v2/examples/isometric1/result1.png" caption="Example result" />

## Scope

This example is only a very simple showcase of generating isometric levels.

It **includes**:

- Basic isometric room templates
- Custom tilemap layers structure
- Custom room template initializer

It **does not include**:

- Any characters to move around with
- Colliders

## Tilemap layers

The *Grid* is set to *Isometric* and we use the following structure of tilemaps:

- Level 0 - Floor
- Level 0 - Walls
- Level 0.5 - Floor
- Level 0.5 - Walls
- Level 1 - Floor
- Level 1 - Walls
- Colliders

For each elevation level we have 2 tilemap layers - one for floor tile and one for wall (and decoration) tiles. Between the levels 0 and 1 there is a half-level because stairs in the tileset are halfway between 0 and 1. And there is also one additional layer for colliders which is not used in this example but can be utilized to place collider tiles.

Room template prefabs can be created via *Create -> Edgarr -> Examples -> Isometric 1 -> Room template*.

## Room templates

Below you can see some of the basic room templates:

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/v2/examples/isometric1/room1.png" caption="Basic room" />
    <GalleryImage src="img/v2/examples/isometric1/room2.png" caption="Basic room" />
    <GalleryImage src="img/v2/examples/isometric1/room3.png" caption="Basic room" />
    <GalleryImage src="img/v2/examples/isometric1/room5.png" caption="Basic room" />
</Gallery>

## Corridors

Below you can see some of the corridors: (there are also longer versions)

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/v2/examples/isometric1/corridor1short.png" caption="Short corridor" />
    <GalleryImage src="img/v2/examples/isometric1/corridor2short.png" caption="Short corridor" />
</Gallery>


## Level graph

<Image src="img/v2/examples/isometric1/level_graph.png" caption="Level graph" />

## Results

<Image src="img/v2/examples/isometric1/result2.png" caption="Example result" />
<Image src="img/v2/examples/isometric1/result3.png" caption="Example result" />