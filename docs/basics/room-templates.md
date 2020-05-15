---
title: Room templates
---

import { Image, Gallery, GalleryImage } from "@theme/Gallery";

Room templates are one of the main concepts of the algorithm. They describe how individual rooms in the dungeon look and how they can be connected to one another. 

<Image src="img/v2/room_templates/room_template_complete.png" caption="Example of a complete room template. Outline of the room template is hightlighted with yellow and possible door positions are red." />

## Creating room templates

There are currently two ways of creating room templates: either via the *Create asset* menu dialog or manually.

### Create via *Create* menu dialog

To create a new room template, we have to:
- navigate to the folder where should the room template prefab be saved
- right click in the Project window and choose *Create -> Dungeon generator -> Dungeon room template*
- (optional) rename the prefab file to anything you want

### Create manually

To create a new room template manually, we have to:
- create an empty game object
- add *Dungeon Room Template Initializer* component
- click the *Initialize room template* button
- create a prefab from that game object

The whole process can be seen on the video below:

<Image src="img/v2/room_templates/creating_room_templates.gif" caption="Initializing room template using the Dungeon room template initializer script" />

### Room template structure

Below you can see the room template structure after we use the room template initializer:
- **Tilemaps** game object that contains several tilemaps attached as children
- **Room Template** script attached to the root game object
- **Doors** script attached to the root game object

<Image src="img/v2/room_templates/room_template_inspector.png" caption="Room template structure" />

## Designing room templates

We will use Unity [Tilemaps](https://docs.unity3d.com/Manual/class-Tilemap.html) to design our room templates so you should be familiar with that. By default, room templates come with several tilemap layers that are children of the *Tilemap* game object:

- **Floor** - order 0 
- **Walls** - order 1, with collider
- **Collideable** - order 2, with collider
- **Other 1** - order 3
- **Other 2** - order 4
- **Other 3** - order 5

It is **VERY IMPORTANT** that all the room templates have exactly the same structure of tilemaps because the generator will copy all the tiles from individual room templates to shared tilemaps. If you need a different structure of tilemaps, you can override the default behaviour. See [Room template customization](../guides/room-template-customization).

<Image src="img/v2/room_templates/room_template_drawing.gif" caption="You can use all the available tools (brushes, rule tiles, etc.) to draw room templates" />

### Limitations

The underlying algorithm works with polygons, not tilemaps, tiles and sprites. We are interested in the outlines of individual room templates. However, there are some limitations as to how a room template may look like in order for the algorithm to be able to compute its outline. The goal of this section is to describe which rules we should follow when designing room templates.

<Image src="img/v2/room_templates/outline.png" caption="The yellow color shows the outline of the room template as it is seen by the generator" />

<Image src="img/v2/room_templates/invalid_outline.png" caption="If the outline is invalid, we can see a warning in the *Room Template* script" />

> **Note:** The underlying algorithm does not care about individual tilemaps layers. Instead, it merges all the layers together and then finds all the non-null tiles. Therefore, the outline of the room template will be the same no matter which tilemap layers we use.

#### One connected component

I will not go into formal definitions. The image below should be self-explanatory.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/v2/room_templates/one_connected_component_nok.png" caption="Wrong" />
    <GalleryImage src="img/v2/room_templates/one_connected_component_ok.png" caption="Correct" />
</Gallery>

> **Note:** You can see that the algorithm computed some outline (yellow) in in the wrong room template. The current implementation stops after any outline is found and does not check whether all tiles are contained in that outline. This will be improved in the future.

#### Each tile atleast two neighbours

Each tile must be connected to at least two neigbouring tiles. In the image below, both tiles in the upper row are connected to only a single neighbour so the room shape is not valid. If we need these two tiles, we can use **Outline override** that is described in the next section.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/v2/room_templates/at_least_two_neighbors_nok.png" caption="Wrong" />
    <GalleryImage src="img/v2/room_templates/at_least_two_neighbors_ok.png" caption="Correct" />
</Gallery>

#### May contain holes

There may be holes inside the room template.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/v2/room_templates/no_holes_ok_1.png" caption="Correct" />
    <GalleryImage src="img/v2/room_templates/no_holes_ok_2.png" caption="Correct" />
</Gallery>

> **NOTE:** This was not possible in the 1.x.x version.

### Outline override

If we really need to have a room template whose outline is not valid, we can use the so-called *Outline override*. It can be enabled by clicking the *Add outline override* button in the *Room template* script. As a result, a new tilemap layer called *Outline override* is created. With this functionality enabled, the algorithm ignores all the other layers when computing the outline. Moreover, nothing that is drawn on this layer will be used in the resulting level, so we can use any tiles to draw on this layer. 

> **Note:** When we are done with drawing the outline, we can make the layer (game object) inactive so that we can see how the room template actually looks like. However, **we must not destroy the game object**.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/v2/room_templates/outline_override_active.png" caption="We can use any tiles to draw the outline" />
    <GalleryImage src="img/v2/room_templates/outline_override_inactive.png" caption="If we disable the Outline override game object, we should still see that the outline is overriden" />
</Gallery>

### Bounding box outline handler

In some situations, it would be useful to have an outline which looks like the bounding box of all the tiles in the room template. For example, it can be used to handle outline of some platformer levels (see the images below). It is also possible to add padding to the top of the outline, which is convenient if we need to add doors that are higher than the outline.

To add the *Bounding box outline handler* click the **Add bounding box outline handler** button in the *Room template* inspector.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/v2/room_templates/bounding_box_invalid.png" caption="Invalid outline" />
    <GalleryImage src="img/v2/room_templates/bounding_box_override.png" caption="Corrected manually with Outline override" />
    <GalleryImage src="img/v2/room_templates/bounding_box.png" caption="Corrected automatically with Bounding box outline handler" />
    <GalleryImage src="img/v2/room_templates/bounding_box_padding_top.png" caption="Example of Padding top 3" />
</Gallery>

<Image src="img/v2/room_templates/bounding_box.gif" caption="Example bounding box outline usage" />


## Adding doors

When we are happy with how the room template looks, we can add doors. By specifying door positions, we tell the algorithm how can individual room templates be connected together.

The algorithm may connect two room templates if:
- there exist door positions with the same length
- the two room templates do not overlap after we connect them
    - they may share tiles on outlines of corresponding room shapes

> **Note:** In some procedural level generators, all the doors must be used by the algorithm to connect the room to other rooms. That is not the case here, we define all the possible door positions and the generator may pick only some of them.

### Door modes

There are currently two ways of defining door positions. Both ways are currently controlled by the *Doors* component that is automatically added to the room game object after using the room template initializer.

In both modes, all door positions must be on the outline of the corresponding room template.

#### Simple mode

In the simple mode, you specify how long should all doors be and at least how far from corners of the room template they should be positioned. Below you can see how this mode looks.

<Image src="img/v2/room_templates/doors_simple1.png" caption="Simple door mode - length 1, distance from corners 2" />

Each red rectangle shows available door positions. You can see that there are no door positions in the bottom-right part of the room template - that is because no tile is placed at least 2 tiles from all corners. If we change the door length to 2, we will loose the door positon on the right side of the room template because there is space only for a single tile.

<Image src="img/v2/room_templates/doors_simple2.png" caption="Simple door mode - length 2, distance from corners 2" />

> **Note:** There is currently an inconsistency in how are door positions displayed. In the *simple mode*, each red rectangle represents a set of door positions, while in the *specific positions mode*, each rectangle represents exactly one door position. The reason for this is that it is exactly how the procedural dungeon generator library handles that, but it might be counter-intuitive for users of the plugin and may change in the future.

#### Specific positions mode

In the *Specific positions mode*, you have to manually specify all door positions of the room template. This mode gives you a complete control over available door positions.

To start adding doors, click the *Specific positions* button in the *Doors* script and then click the *Add door positions* button to toggle edit mode. Then you can simply draw door positions as seen in the video below.

<Image src="img/original/doors_specific1.gif" caption="Specific positions mode" />

You can see that I am creating doors of various lengths. And at the end of the video, you can also see that individual door positions may overlap.

> **Note:** If you accidentally add a door position that you did not want to add, there are two ways of removing doors:
>
> 1. Click the *Delete all door positions* to delete all the door positions.
> 2. dsa

> **Note:** With multiple doors overlapping, the GUI gets quite messy. In order to make it more clear, I show diagonals of individual rectangles. And it gets even more messy when you have doors of various sizes overlapping. I thought about adding a switch that would show only doors with a specified length.

> **Note:** The inspector script currently lets you add door positions that are not on the outline of the room shape. It will, hovewer, result in an error when trying to generate a dungeon. It should be improved in the future.

## Repeat mode

Each *Room template* script has a field called *Repeat Mode* that is initially set to *Allow Repeat*. Using this field, we can tell the algorithm whether the room template can be used more than once in generated levels. There are the following possibilities:

- **Allow repeat** - The room tamplate may repeat in generated levels.
- **No immeadiate** - Neighbors of the room template must be different.
- **No repeat** - The room template can be used at most once.

<Image src="img/v2/room_templates/repeat_mode.png" caption="Specific positions mode" />

> **Note:** Instead of setting the *Repeat mode* on a per room template basis, you can use global override which is configured directly in the dungeon generator.

> **Note:** If you provide too few room templates, they may repeat in generated levels even if you choose the **No immeadiate** or **No repeat** options. To make sure that the repeat mode is satisifed, please provide enough room templates to choose from.

## Corridors

The algorithm distinguishes two types of room templates - basic room templates and room templates for corridor rooms. In theory, we can use any any room template with at least two doors to act as a corridor room template. **However, to make the algorithm fast, we should follow these recommendations**:

1. There should be exactly two door positions.
2. The two door should be on the opposite sides of the room template.
3. The corridor should not be too long or too wide.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/original/corridor_ok1.png" caption="Recommended - narrow straight corridor" />
    <GalleryImage src="img/original/corridor_ok2.png" caption="OK - little too wide but should be ok" />
    <GalleryImage src="img/original/corridor_nok1.png" caption="Not recommended - doors not on opposite sides" />
    <GalleryImage src="img/original/corridor_nok2.png" caption="Not recommended - more than 2 door positions" />
</Gallery>

## Final steps

After creating a room template GameObject, you can simply save it as a prefab and it is ready to be used in a level graph.