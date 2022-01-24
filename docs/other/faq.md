---
title: FAQ
---

import TOCInline from '@theme/TOCInline';

This document contains solutions to common questions that are often asked on Discord and other channels.

### Table of Contents

<TOCInline toc={toc} maxHeadingLevel={2} />

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