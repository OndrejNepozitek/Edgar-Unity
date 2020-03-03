---
title: Room templates
---

import { Image, Gallery, GalleryImage } from "@theme/Gallery";

Room templates are one of the main concepts of the algorithm. They describe how individual rooms in the dungeon look and how they can be connected to one another. 

## Terminology
- Room layout - how a room looks, tiles with sprites, walls, floors, furniture, etc.
- Room shape - floor plan of a room layout
- Doors - all possible positions of doors of a room layout
- Room template - room layout together with doors

## Room layout

We will use Unity [Tilemaps](https://docs.unity3d.com/Manual/class-Tilemap.html) to design our room layouts so you should be familiar with that. The whole room template consists of a *Grid* *GameObject* that has one or more child *Tilemap* *GameObjects* and a *Doors* script attached. You can use all available tools (brushes, rule tiles, etc.) to paint tilemaps. 

<Image src="img/original/room_layout.png" caption="Example room layout" />

### Tilemaps

The plugin is also prepared to handle layouts consisting of multiple tilemaps if you want to have multiple layers of tiles. In fact, the default configuration of the plugin uses several tilemaps because you cannot really do that much with a single layer of tiles. The default structure of tilemaps is as follows:

- **Walls** - order 0, with collider
- **Floor** - order 1 
- **Collideable** - order 2, with collider
- **Other** 1 - order 3
- **Other 2** - order 4
- **Other 3** - order 5

It is **VERY IMPORTANT** that all room layouts are structured exactly like this because tiles from tile layouts will be copied to correspoding tilemaps of generated dungeon layouts. The names of individual tilemaps are not really important - I just thought that it may be better than simply numbering them and the algorithm currently does not care whether there are walls in the first tilemap or not.

To make it easier to create room templates, there is a *DefaultRoomTeplateInitializer* script that can be added to an empty *GameObject* and then used to create the correct structure of tilemaps. See the video below.

<Image src="img/original/creating_tilemaps.gif" caption="Initializing tilemaps using the DefaultRoomTeplateInitializer script" />

If you need a different structure of tilemaps, you can override the default behaviour. See [Tilemap layers](tilemapLayers.md).

> **Feedback needed:** The default structure of tilemaps aims to provide a reasonable structure for game designers to start creating room layouts. However, I have got no experience with working with tilemaps in real projects so I would like to hear any feedback on whether this structure is a good default or not.

### Room shape

The underlying algorithm works with polygons, not tilemaps, tiles and sprites. We call these polygons room shapes and they are simply outlines/floor plans of corresponding room layouts. However, not all room shapes are valid in the context of the algorithm. The goal of this section is to describe how can room shapes look like.

<Image src="img/original/room_shape.png" caption="The green outline shows the room shape of a corresponding room layout" />

> **Note:** Because we are only interested in the outline of a room layout, the internal structure (number, order) of tilemaps is irrelevant. For the purpose of the computation we can imagine that all tiles are in a single tilemap.

#### One connected component

I will not go into formal definitions. The image below should be self-explanatory.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/original/one_connected_component_nok.png" caption="Wrong" />
    <GalleryImage src="img/original/one_connected_component_ok.png" caption="Correct" />
</Gallery>

#### Each tile atleast two neighbours

Each tile must be connected to at least two neigbouring tiles. In the image below, both tiles in the upper row are connected to only a single neighbour so the room shape is not valid. If we need these two tiles, we can simly fill the upper row with *empty* or transparent tiles.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/original/at_least_two_neighbours_nok.png" caption="Wrong" />
    <GalleryImage src="img/original/at_least_two_neighbours_ok.png" caption="Correct" />
</Gallery>

#### Without holes

There must be no holes in room layouts (a null tile surrounded by non-null tiles). Again, we can fill such holes with *empty* or transparent tiles if we need them there.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/original/no_holes_nok.png" caption="Wrong" />
    <GalleryImage src="img/original/no_holes_ok.png" caption="Correct" />
</Gallery>

> **NOTE:** This is only a limitation of the current algorithm for computing room shapes from room layouts and the constraint will be probably dropped in the future.

## Doors

When we have our room layout ready, we can add doors. By specifying door positions, we tell the algorithm how can individual room templates be connected together.

The algorithm may connect two room templates if:
- there exist door positions with the same length
- the two room templates do not overlap after we connect them
    - they may share tiles on outlines of corresponding room shapes

### Door modes

There are currently two ways of defining door positions. Both ways are currently controlled by the *Doors* component that is automatically added to the parent GameObject after using the *DefaultRoomTeplateInitializer*.

In both modes, all door positions must be on the outline of the corresponding room shape.

> **Note:** There are situations where we might want to relax this requirement and allow door positions that are not on the outline. So maybe that will be possible in the future versions of the plugin.

#### Simple mode

In the simple mode, you specify how long should all doors be and at least how far from corners of the room layout they should be positioned. Below you can see how this mode looks.

<Image src="img/original/doors_simple1.png" caption="Simple door mode - length 1, distance from corners 2" />

Each red rectangle shows available door positions. You can see that there are no door positions in the bottom-right part of the layout - that is because no tile is placed at least 2 tiles from all corners. If we change the door length to 2, we will loose the door positon on the right side of the room layout because there is space only for a single tile.

<Image src="img/original/doors_simple2.png" caption="Simple door mode - length 2, distance from corners 2" />

> **Note:** There is currently an inconsistency in how are door positions displayed. In the *simple mode*, each red rectangle represents a set of door positions, while in the *specific positions mode*, each rectangle represents exactly one door position. The reason for this is that it is exactly how the procedural dungeon generator library handles that, but it might be counter-intuitive for users of the plugin and may change in the future.

#### Specific positions mode

In the *Specific positions mode*, you have to manually specify all door positions of a room layout. This mode gives you a complete control over available door positions.

To start adding doors, click the *Specific positions* button in the *Doors* script and then click the *Add door positions* button to toggle edit mode. Then you can simply draw door positions as seen in the video below.

<Image src="img/original/doors_specific1.gif" caption="Specific positions mode" />

You can see that I am creating doors of various lengths. And at the end of the video, you can also see that individual door positions may overlap.

> **Note:** If you accidentally add a door position that you did not want to add, you have to *Delete all door positions* and start over. This is far from ideal and should be improved in the future.

> **Note:** With multiple doors overlapping, the GUI gets quite messy. In order to make it more clear, I show diagonals of individual rectangles. And it gets even more messy when you have doors of various sizes overlapping. I thought about adding a switch that would show only doors with a specified length.

> **Note:** The inspector script currently lets you add door positions that are not on the outline of the room shape. It will, hovewer, result in an error when trying to generate a dungeon. It should be improved in the future.

## Corridors

The algorithm distinguishes two types of room tamples - basic room templates and room templates for corridor rooms. There are currently 2 limitations regarding doors in corridor room templates:
1. There must be exactly two door positions.
2. The two door positions must be on the opposite sides of the room layout.

The images below demonstrate what is a correct corridor room template and what is not.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/original/corridor_ok1.png" caption="Correct" />
    <GalleryImage src="img/original/corridor_ok2.png" caption="Correct" />
    <GalleryImage src="img/original/corridor_nok1.png" caption="Wrong - Doors not on opposite sides" />
    <GalleryImage src="img/original/corridor_nok2.png" caption="Wrong - More than 2 door positions" />
</Gallery>

> **Note:** This is a limitation of the dungeon generator library and not the plugin itself. I plan to completely rework corridors in the future.

## Final steps

After creating a room template GameObject, you can simply save it as an asset and it is ready to be used in a level graph.