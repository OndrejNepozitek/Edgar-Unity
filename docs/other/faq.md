---
title: FAQ
---

import TOCInline from '@theme/TOCInline';
import TrackGeneratorProgress from './faq/_track-generator-progress.md';
import EndlesslevelGeneration from './faq/_endless-level-generation.md';
import LevelsWithoutCorridors from './faq/_levels-without-corridors.md';
import LocksAndKeys from './faq/_locks-and-keys.md';
import NonRectangularRooms from './faq/_non_rectangular_rooms.md';
import SaveAndLoad from './faq/_save-and-load.md';
import ErrorInBuild from './faq/_error-in-build.md';
import Pathfinding from './faq/_pathfinding.md';
import Enemies from './faq/_enemies.md';

This document contains solutions to common questions that are often asked on Discord and other channels.

#### Table of Contents

<TOCInline toc={toc} maxHeadingLevel={2} />

## How to spawn a player in a specific room

The easiest solution is to design a special *spawn* room template and place the player prefabs inside that room template. Then, make this room template the only room template for the *Spawn* room in your level graph. This approach is described in [Example 1](../examples/example-1.md#spawn-room).

Another option is to move your player in a post-processing logic. Instead of putting the player prefabs inside the *Spawn* room template, you can just mark the spawn spot with an empty game object. Then, after a level is generated, you can run a post-processing script that will move the player to the marked position. This approach is described in the [Dead Cells example](../examples/dead-cells.md#spawn-position).

## What to do with a `TimeoutException`

Sometimes, when you want to generate a level, you get a `TimeoutException` in the console instead. The error means that the generator was not able to produce a level in a given time limit which is 10 seconds by default. The error can have two different meanings: 

- the level graph is too hard for the generator (there are too many rooms, too many cycles, restrictive room templates, etc.)
- or there is a problem somewhere in the configuration (maybe the doors of two neighboring room templates are not compatible)

Usually, it is the second case. To help you fix the error, the generator dumps some diagnostic information *above* the error in the console. The type of information that you can find in the console is for example that the lengths of doors are suspicious or that there are maybe too many rooms in the level graph.

If you are not able to fix the problem yourself, come to our Discord and I will try to help. Also, you can read the [Performance tips](../basics/performance-tips.md) page.

## How to deal with wider walls

Some tilesets have walls that are wider than a single tile. If you try to approach this scenario the same way as 1-wide walls, you will find out that the corridor will not go through the whole multi-tile-wide wall but only through the first tile. The solution for this problem is using the *Outline Override* feature to modify the outline of corridors. This setup is described in the [Example 2](../examples/example-2.md) tutorial where there is a tileset with an additional layer of wall tiles on top of horizontal walls. (The most important part is the [Vertical corridos](../examples/example-2.md#vertical-corridors) section.)

## Changes to a room template are lost after a level is generated

It often happens that you want to change the default structure of a room template. Maybe you want to add a collider, add another tilemap layer, or change the properties of the grid like the cell size. But when you hit the *Generate* button, the changes are not there and the level looks exactly like before.

The reason is that after a level is generated, all the room templates are merged into a single set of shared tilemaps. So, if you want to change something, you also have to instruct the generator that the changes should also be applied to the shared tilemaps. You can find a dedicated guide [here](../guides/room-template-customization.md).

## Rooms are generated too close to one another

The generator will often produce a level where the walls of one room are right next to the walls of another room. Usually, this is perfectly okay, but sometimes, it may cause problems. For example, if rule tiles are used for walls, a room that is too close may interfere with tiles in a different room.

To solve this problem, look for the `Minimum Room Distance` field in the dungeon generator inspector and increase the value to *2*. You can increase the value even more for an even larger distance between rooms. Just keep in mind that every increase in this field makes it harder for the generator to produce a level (and can, therefore, lead to timeouts). Read more about the `Minimum Room Distance` config [here](../generators/dungeon-generator.md#generator-config).

## Send the same level to multiple players (in a multiplayer game)

If you want to use the generator in a multiplayer scenario, you might wonder how to distribute the same level to all the players. The easiest solution is to send the **seed** of the generator to every player and then run the generator with the seed on all clients. You can read more about the seed [here](../generators/dungeon-generator.md#other-config) and how to see the seed programmatically [here](../generators/dungeon-generator.md#change-the-configuration-from-a-script).

## Keep prefab references when generating levels inside the editor

There is a dedicated guide [here](../recipes/prefabs-in-editor.md).

## Is it possible to track the progress of the generator?

<TrackGeneratorProgress />

## Is Edgar suitable for an endless level generation?

<EndlesslevelGeneration />

## How to generate levels without corridors?

<LevelsWithoutCorridors />

## TODO How to implement locks and keys?

<LocksAndKeys />

## Is it possible to have non-rectangular rooms?

<NonRectangularRooms />

## How to implement a save and load system?

<SaveAndLoad />

## The generator works in the editor but not in the build

<ErrorInBuild />

## How to handle pathfinding?

<Pathfinding />

## How to spawn enemies?

<Enemies />