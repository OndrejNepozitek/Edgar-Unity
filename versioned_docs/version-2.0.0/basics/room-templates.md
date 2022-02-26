---
title: Room templates
---

Room templates are one of the main concepts of the generator. They describe how individual rooms in the dungeon look and how they can be connected to one another. 

<Image src="2d/room_templates/room_template_complete.png" caption="Example of a complete room template. Outline of the room template is highlighted with yellow and possible door positions are red." />

## Creating room templates

To create a new room template, you have to:

- navigate to the folder where the room template prefab should be saved
- right click in the *Project window* and choose <Path path="2d:Dungeon room template" />
- (optional) rename the prefab file to anything you want


### Room template structure

Below you can see the room template structure after the room template is created:

- **Tilemaps** game object that contains several tilemaps attached as children
- **Room Template** script attached to the root game object
- **Doors** script attached to the root game object

<Image src="2d/room_templates/room_template_inspector.png" caption="Room template structure" obsolete />

## Designing room templates

We will use Unity [Tilemaps](https://docs.unity3d.com/Manual/class-Tilemap.html) to design our room templates, so you should be familiar with that. By default, room templates come with several tilemap layers that are children of the *Tilemap* game object:

- **Floor** - order 0 
- **Walls** - order 1, with collider
- **Collideable** - order 2, with collider
- **Other 1** - order 3
- **Other 2** - order 4
- **Other 3** - order 5

It is **VERY IMPORTANT** that all the room templates have exactly the same structure of tilemaps because the generator will copy all the tiles from individual room templates to shared tilemaps. If you need a different structure of tilemaps, you can override the default behaviour. See [Room template customization](../guides/room-template-customization.md).

<Image src="2d/room_templates/room_template_drawing.gif" caption="You can use all the available tools (brushes, rule tiles, etc.) to draw room templates" obsolete />

### Limitations

The underlying algorithm works with polygons, not tilemaps, tiles and sprites. We are interested in the outlines of individual room templates. However, there are some limitations as to how a room template may look like in order for the algorithm to be able to compute its outline. The goal of this section is to describe which rules we should follow when designing room templates.

<Image src="2d/room_templates/outline.png" caption="The yellow color shows the outline of the room template as it is seen by the generator" />

<Image src="2d/room_templates/invalid_outline.png" caption="If the outline is invalid, we can see a warning in the *Room Template* script" obsolete />

> **Note:** The underlying algorithm does not care about individual tilemaps layers. Instead, it merges all the layers together and then finds all the non-null tiles. Therefore, the outline of the room template will be the same no matter which tilemap layers we use.

#### One connected component

I will not go into formal definitions. The image below should be self-explanatory.

<Gallery cols={2} fixedHeight>
    <Image src="2d/room_templates/one_connected_component_nok.png" caption="Wrong" />
    <Image src="2d/room_templates/one_connected_component_ok.png" caption="Correct" />
</Gallery>

> **Note:** You can see that the algorithm computed some outline (yellow) in the wrong room template. The current implementation stops after any outline is found and does not check whether all tiles are contained in that outline. This will be improved in the future.

#### Each tile at least two neighbours

Each tile must be connected to at least two neighbouring tiles. In the image below, both tiles in the upper row are connected to only a single neighbour, so the room shape is not valid. If we need these two tiles, we can use **Outline override** that is described in the next section.

<Gallery cols={2} fixedHeight>
    <Image src="2d/room_templates/at_least_two_neighbors_nok.png" caption="Wrong" />
    <Image src="2d/room_templates/at_least_two_neighbors_ok.png" caption="Correct" />
</Gallery>

#### May contain holes

There may be holes inside the room template.

<Gallery cols={2} fixedHeight>
    <Image src="2d/room_templates/no_holes_ok_1.png" caption="Correct" />
    <Image src="2d/room_templates/no_holes_ok_2.png" caption="Correct" />
</Gallery>

> **NOTE:** This was not possible in the 1.x.x version.

### Outline override

If we really need to have a room template whose outline is not valid, we can use the so-called *Outline override*. It can be enabled by clicking the *Add outline override* button in the *Room template* script. As a result, a new tilemap layer called *Outline override* is created. With this functionality enabled, the algorithm ignores all the other layers when computing the outline. Moreover, nothing that is drawn on this layer will be used in the resulting level, so we can use any tiles to draw on this layer. 

> **Note:** When we are done with drawing the outline, we can make the layer (game object) inactive so that we can see how the room template actually looks like. However, **we must not destroy the game object**.

<Gallery cols={2} fixedHeight>
    <Image src="2d/room_templates/outline_override_active.png" caption="We can use any tiles to draw the outline" />
    <Image src="2d/room_templates/outline_override_inactive.png" caption="If we disable the Outline override game object, we should still see that the outline is overridden" />
</Gallery>

### Bounding box outline handler

In some situations, it would be useful to have an outline which looks like the bounding box of all the tiles in the room template. For example, it can be used to handle an outline of some platformer levels (see the images below). It is also possible to add padding to the top of the outline, which is convenient if we need to add doors that are higher than the outline.

To add the *Bounding box outline handler* click the **Add bounding box outline handler** button in the *Room template* inspector.

<Gallery cols={2} fixedHeight>
    <Image src="2d/room_templates/bounding_box_invalid.png" caption="Invalid outline" />
    <Image src="2d/room_templates/bounding_box_override.png" caption="Corrected manually with Outline override" />
    <Image src="2d/room_templates/bounding_box.png" caption="Corrected automatically with Bounding box outline handler" />
    <Image src="2d/room_templates/bounding_box_padding_top.png" caption="Example of Padding top 3" />
</Gallery>

<Image src="2d/room_templates/bounding_box.gif" caption="Example bounding box outline usage" />


## Adding doors

When we are happy with how the room template looks, we can add doors. By specifying door positions, we tell the algorithm how can individual room templates be connected.

The algorithm may connect two room templates if:
- there exist door positions with the same length
- the two room templates do not overlap after we connect them
    - but they may share tiles on the outlines

> **Note:** In some level generators, if you define *N* doors, it means that the room must be connected to *N* neighbours. That is not the case here! By adding door positions, **you specify where a door can be**. But if there is a room template with 20 possible door positions, the generator might still use this room template for a room that has only a single neighbour. Moreover, a high number of available door position usually means better performance.

### How to interpret door gizmos

Before we start adding doors to our room templates, I think it is important to understand how to read the editor gizmos that represent doors. In the image below (left), we can see an example room template with red rectangles showing the available door positions. The **dashed red rectangles** represent individual **door lines** where a door line is set of all the possible doors inside rectangle. The **solid red rectangles** show the **length of the doors**. In the room template below, all doors are 2 tiles wide. The solid rectangle also contains information about how many door positions there are in the door line.

The GIF on the right shows an animation of all the possible doors positions from the room template on the left. An important thing to understand is that the door positions can overlap, and it is even good for the performance of the generator. The reason for that is that there are more possible door positions to choose from, so the generator finds a valid layout faster. 

<Gallery cols={2}>
    <Image src="2d/room_templates/doors/doors_visuals.png" caption="Example room template" />
    <Image src="2d/room_templates/doors/doors_animation.gif" caption="Animation of all the possible door positions" />
</Gallery>

### Door modes

To manipulate with the doors, there must be a `Doors` component attached to the root of the room template prefab. 

There are currently three different ways of defining door positions. A universal rule of all the different modes is that all door positions must be on the outline of the corresponding room template.

#### Simple mode

In the *simple mode*, you specify how wide should all the doors be and the margin, i.e. how far from corners of the room template must the doors be. This door mode is great for when you do not really care where exactly the doors can be. This door mode also usually has the best performance because there are many door positions to choose from.

Below you can see how this door mode looks in the editor.

<Image src="2d/room_templates/doors/simple1.png" caption="Simple door mode - length 1, margin 2" />

In side-scroller games, there are often different requirements for horizontal and vertical doors. For example, the player might be 3 tiles high but only 1 tile wide, so we would need wider vertical doors. To achieve this, we can change the *Mode* dropdown to `Different Horizontal And Vertical`. With this setting enabled, we can now choose different properties for vertical and horizontal doors. Or we might also disable one type of doors.

<Image src="2d/room_templates/doors/simple2.png" caption="Simple door mode - different vertical and horizontal doors" />

#### Manual mode

In the *manual mode*, you have to manually specify all the door positions of the room template. This door mode is great for when you have only a couple of doors at very specific positions.

To start adding doors, click the *Manual mode* button in the *Doors* script and then click the *Add door positions* button to toggle edit mode. Then you can simply draw door positions by clicking on the first tile of the door and dragging the mouse to the last tile of the door.

<Image src="2d/room_templates/doors/manual.gif" caption="Manual mode setup" />

In the example above, we can see that we can have doors with different lengths - vertical doors are 3 tiles high and horizontal doors are 1 tile wide.

> **Note:** If you accidentally add a door position that you did not want to add, there are two ways of removing doors:
>
> 1. Click the *Delete all door positions* button to delete all the door positions.
> 2. Click the *Delete door positions* button and then click on door positions that should be deleted.

> **Note:** With multiple doors overlapping, the GUI gets quite messy. You should usually use the *hybrid mode* when you have overlapping doors.

> **Note:** The inspector script currently lets you add door positions that are not on the outline of the room shape. It will, however, result in an error when trying to generate a dungeon. It should be improved in the future.

#### Hybrid mode

The *hybrid mode* is somewhere between the *simple* and *manual* modes. Instead of drawing individual door positions (like in the manual mode), we can draw whole door lines (multiple doors at once). 

To start adding doors, click the *Manual mode* button in the *Doors* script and then click the *Add door positions* button to toggle edit mode. **Then you have to configure the length of the doors in the field below.** This is the main difference when compared to the *manual mode*. In the manual mode, the length of doors is determined by the movement of the mouse. But in the hybrid mode, the length of doors is configured in the editor, and the movement of the mouse specifies how many doors there are next to each other.

<Image src="2d/room_templates/doors/hybrid.gif" caption="Hybrid mode setup" />

> **Note:** The *hybrid mode* is great for when you cannot use the simple mode and the manual mode would require too much time to set up. Also, the hybrid mode also nicely handles **overlapping doors** because the definition of door lines implicitly contains them. Moreover, the hybrid mode also leads to **better performance** (when compared to the manual mode) because it promotes having many doors and the doors are in a format that the generator can easily work with.

### (PRO) Door sockets

By default, when the generator computes how can two room templates be connected, it looks for doors with the same length. If you want to have more control over this process, you can use [Door sockets](../guides/door-sockets.md).

### (PRO) Door directions

By default, all doors are undirected, meaning that they can be used both as an entrance or as an exit. With manual door mode, it is possible to configure doors as entrance-only or exit-only. When combined with directed level graphs, it gives you more control over generated levels. See the [Directed level graphs](../guides/directed-level-graphs.md) guide for more information and examples.

## Repeat mode

Each *Room template* script has a field called *Repeat Mode* that is initially set to *Allow Repeat*. Using this field, we can tell the algorithm whether the room template can be used more than once in generated levels. There are the following possibilities:

- **Allow repeat** - The room template may repeat in generated levels.
- **No immediate** - Neighbors of the room template must be different.
- **No repeat** - The room template can be used at most once.

<Image src="2d/room_templates/repeat_mode.png" caption="Specific positions mode" />

> **Note:** Instead of setting the *Repeat mode* on a per room template basis, you can use global override which is configured directly in the dungeon generator.

> **Note:** If you provide too few room templates, they may repeat in generated levels even if you choose the **No immediate** or **No repeat** options. To make sure that the repeat mode is satisfied, please provide enough room templates to choose from.

## Corridors

The algorithm distinguishes two types of room templates - basic room templates and room templates for corridor rooms. In theory, we can use any room template with at least two doors to act as a corridor room template. **However, to make the algorithm fast, we should follow these recommendations**:

1. There should be exactly two door positions.
2. The two doors should be on the opposite sides of the room template.
3. The corridor should not be too long or too wide.

<Gallery cols={2} fixedHeight>
    <Image src="2d/room_templates/corridor_ok1.png" caption="Recommended - narrow straight corridor" />
    <Image src="2d/room_templates/corridor_ok2.png" caption="OK - little too wide but should be ok" />
    <Image src="2d/room_templates/corridor_nok1.png" caption="Not recommended - doors not on opposite sides" />
    <Image src="2d/room_templates/corridor_nok2.png" caption="Not recommended - more than 2 door positions" />
</Gallery>

## Final steps

After creating a room template game object, you can simply save it as a prefab, and it is ready to be used in a level graph.