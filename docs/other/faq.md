---
title: FAQ
---

import TOCInline from '@theme/TOCInline';

This document contains solutions to common questions that are often asked on Discord and other channels.

#### Table of Contents

<TOCInline toc={toc} maxHeadingLevel={2} />

## How to spawn player in a specific room?

The easiest solution is to design a special *spawn* room template and place the player prefabs inside that room template. Then, make this room template the only room template for the *Spawn* room in your level graph. This approach is described in [Example 1](../examples/example-1.md#spawn-room).

Another option is to move your player in a post-processing logic. Instead of putting the player prefabs inside the *Spawn* room template, you can just mark the spawn spot with an empty game object. Then, after a level is generated, you can run a post-processing script that will move the player to the marked position. This approach is described in the [Dead Cells example](../examples/dead-cells.md#spawn-position).

## What to do with a `TimeoutException`

Sometimes, when you want to generate a level, you get a `TimeoutException` in the console instead. The error means that the generator was not able to produce a level in a given time limit which is 10 seconds by default. The error can have two different meanings: 

- the level graph is too hard for the generator (there are too many rooms, too many cycles, restrictive room templates, etc.)
- or there is a problem somewhere in the configuration (maybe the doors of two neighbouring room templates are not compatible)

Usually, it is the second case. To help you fix the error, the generator dumps some diagnostic information *above* the error in the console. The type of information that you can find in the console is for example that the lengths of doors are suspicious or that there are maybe too many rooms in the level graph.

If you are not able to fix the problem yourself, come to our Discord and I will try to help.

## Changes to a room template are lost after a level is generated

It often happens that you want to change the default structure of a room template. Maybe you want to add a collider, add another tilemap layer, or change the properties of the grid like the cell size. But when you hit the *Generate* button, the changes are not there and the level looks exactly like before.

The reason is that after a level is generated, all the room templates are merged to a single set of shared tilemaps. So, if you want to change something, you also have to instruct the generator that the changes should also be applied to the shared tilemaps. You can find a dedicated guide [here](../guides/room-template-customization.md).

## Rooms are generated too close to one another

The generator will often produce a level where the walls of one room are right next to the walls of another room. Usually, this is perfectly okay, but sometimes, it may cause problems. For example, if rule tiles are used for walls, a room that is too close may interfere with tiles in a different room.

To solve this problem, look for the `Minimum Room Distance` field in the dungeon generator inspector and increase the value to *2*. You can increase the value even more for even larger distance between rooms. Just keep it mind that every increase of this field makes it harder for the generator to produce a level (and can, therefore, lead to timeouts). Read more about the `Minimum Room Distance` config [here](../generators/dungeon-generator.md#generator-config).

## Send the same level to multiple players (in a multiplayer game)

If you want to use the generator in a multiplayer scenario, you might wonder how to distribute the same level to all the players. The easiest solution is to send the **seed** of the generator to every player and then run the generator with the seed on all clients. You can read more about the seed [here](../generators/dungeon-generator.md#other-config) and how to see the seed programmatically [here](../generators/dungeon-generator.md#change-the-configuration-from-a-script).

## Keep prefab references when generating levels inside editor

There is a dedicated guide [here](../recipes/prefabs-in-editor.md).