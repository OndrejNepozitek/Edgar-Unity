---
id: basics_levelGraphs
title: Level graphs
---

Layout graph is an abstraction that lets us control the structure of generated layouts. 

> **Note:** In the context of this plugin, the term *graph* is used to refer to a mathematical structure consisting of nodes and edges, not a way to visualise functions.

## Basics

Layout graph consists of rooms and room connections. Each room corresponds to a room in a generated layout and each connection tells the algorithm that the two rooms must be connected either directly to each other or via a corridor.

Below you can see a simple level graph with 5 rooms and 4 connections. If we use this level graph as an input for the algorithm, each generated dungeon will have exactly 5 rooms and *room 1* will be connected to every other room in the dungeon.

![](assets/basic_level_graph.png)
*Simple level graph with 5 rooms and 4 room connections.*

> **Note:** It is not important how we draw the graph. It is only important how many rooms there are and which rooms are connected to each other.

## Limitations

### Planar graphs

Level graphs must be **planar**. We say that a graph is planar if it can be drawn on the plane in such a way that no two edges intersect. In our case that means that no two connection lines may intersect. If the input graph was not planar, we would not be able to find a dungeon without rooms or corridors overlapping one another.

> **Note:** A level graph may be planar even if we draw it in a way that some edges intersect. It is because even if one drawing of the graph is "incorrect", that does not mean that there are intersecting edges in all drawings.

### Connected graphs

Level graphs must be **connected**. We say that a graph is connected if there is a path between every pair of vertices. Below you can see a level graph that is not connected because there is no path between vertices on the left side and vertices on the right side.

![](assets/not_connected_level_graph.png)
*Example of a level graph that is not connected.*

## Creating level graphs

## Room templates