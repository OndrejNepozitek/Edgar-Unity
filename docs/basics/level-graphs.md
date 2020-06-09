---
title: Level graphs
---

import { Image, Gallery, GalleryImage } from "@theme/Gallery";

Level graph is an abstraction that lets us control the structure of generated levels. 

> **Note:** In the context of this plugin, the term *graph* is used to refer to a mathematical structure consisting of nodes and edges, not a way to visualise functions.

## Basics

Level graph consists of rooms and room connections. Each room corresponds to a room in a generated level and each connection tells the algorithm that the two rooms must be connected either directly to each other or via a corridor.

Below you can see a simple level graph with 5 rooms and 4 connections. If we use this level graph as an input for the algorithm, each generated dungeon will have exactly 5 rooms and *room 1* will be connected to every other room in the dungeon.

<Image src="img/v2/level_graphs/basic_level_graph.png" caption="Simple level graph with 5 rooms and 4 room connections" />

> **Note:** It is not important how we draw the graph. It is only important how many rooms there are and which rooms are connected to each other.

## Limitations

### Planar graphs

Level graphs must be **planar**. We say that a graph is planar if it can be drawn on the plane in such a way that no two edges intersect. In our case that means that no two connection lines may intersect. If the input graph was not planar, we would not be able to find a dungeon without rooms or corridors overlapping one another.

> **Note:** A level graph may be planar even if we draw it in a way that some edges intersect. It is because even if one drawing of the graph is "incorrect", that does not mean that there are intersecting edges in all the drawings.

### Connected graphs

Level graphs must be **connected**. We say that a graph is connected if there is a path between every pair of vertices. Below you can see a level graph that is not connected because there is no path between vertices on the left side and vertices on the right side.

<Image src="img/v2/level_graphs/not_connected_level_graph.png" caption="Example of a level graph that is not connected" />

## Creating level graphs

*LevelGraph* is a ScriptableObject that can be created by navigating to *Create -> Dungeon generator -> Level graph*. Below you can see how are level graphs displayed in the Inspector window.

<Image src="img/v2/level_graphs/level_graph_inspector.png" caption="Level graph in the Inspector window" />

### Graph editor

The Graph editor window can be opened by clicking the *Open graph editor* button.

<Image src="img/v2/level_graphs/level_graph_window.png" caption="Graph editor window" />

Window controls:
- *Selected graph*: name of the currently selected level graph
- *Select in inspector*: selects the current graph in the inspector window
- *Select level graph*: selects a different level graph

Working with level graphs:
- *Create room*: double click on an empty space in the grid
- *Configure room*: double click on an existing room
- *Delete room*: right click on a room and select *Delete room*
- *Move room*: left click and then drag around
- *Add connection*: hold *ctrl* while left-clicking a room and then move the cursor to a different room
- *Delete connection*: right click on a connection handle and select *Delete connection*

<Image src="img/v2/level_graphs/level_graph_controls.gif" caption="Level graph controls" />

## Room templates

When we have our rooms and connections, it is time to setup room templates. In the *Level graph* inspector window above, we can see 2 sections - *Default room templates* and *Corridor room templates*. These section are used to specify which room templates are available for which room. Below you can see the setup from [Example 1](../examples/example-1.md).

<Image src="img/v2/level_graphs/level_graph_inspector2.png" caption="Example of assigned room templates" />

### Room templates sets

It may sometimes be useful to group our room templates into groups like *Shop rooms*, *Boss rooms*, etc. We can create a so-called **Room templates set** by navigating to *Create -> Dungeon generator -> Room templates set*. It is a simple ScriptableObject that holds an array of room templates and we can use it instead of assigning individual room templates one by one. The main advantage is that if we later decide to add a new shop room template, we do not have to change all the shop rooms to include this new template - we simply add it to the room templates set.

<Image src="img/v2/level_graphs/room_templates_set.png" caption="Example of a room templates set that holds all our basic rooms. If we add another room template later, the change gets propagated to all the rooms in the level graph that are using this room templates set." />

### Default room templates

#### Room templates

Array of room templates that will be used for rooms that have no room shapes assigned. We can use this for our basic rooms and then configure our special rooms (spawn room, boss room, etc.) to use a different set of room templates.

#### Room templates sets

Array of room templates sets that will be used for rooms that have no room shapes assigned. Room templates from these sets are used together with individual room templates.

### Corridor room templates

#### Room templates

Array of room templates that will be used for corridor rooms. These room templates will be used if we setup the algorithm to use corridors instead of connecting rooms directly by doors. Can be left empty if we do not want to use corridors.

#### Room templates sets

Array of room templates sets that will be used for corridor rooms. Room templates from these sets are used together with individual room templates.

### Configuring individual rooms

If we double click on a room in the Graph editor, it gets selected and we can configure it in the inspector. We can set the name of the room which will be displayed in the Graph editor. We can also assign room templates and room templates sets that will be used only for this room. By assigning any room template or room template set, we override the default room templates that are set in the level graph itself.

<Image src="img/v2/level_graphs/room_inspector1.png" caption="Configuration of a spawn room" />

## (PRO) Custom rooms and connections