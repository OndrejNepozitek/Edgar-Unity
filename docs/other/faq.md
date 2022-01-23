---
title: FAQ
---

## Rooms are generated too close to one another

The generator will often produce a level where the walls of one room are right next to the walls of another room. Usually, this is perfectly okay, but sometimes, it may cause problems. For example, if rule tiles are used for walls, a room that is too close may interfere with tiles in a different room.

To solve this problem, look for the `Minimum Room Distance` field in the dungeon generator inspector and increase the value to *2*. You can increase the value even more for even larger distance between rooms. Just keep it mind that every increase of this field makes it harder for the generator to produce a level (and can, therefore, lead to timeouts). Read more about the `Minimum Room Distance` config [here](../generators/dungeon-generator.md#generator-config).

## Send the same level to multiple players (in a multiplayer game)

If you want to use the generator in a multiplayer scenario, you might wonder how to distribute the same level to all the players. The easiest solution is to send the **seed** of the generator to every player and then run the generator with the seed on all clients. You can read more about the seed [here](../generators/dungeon-generator.md#other-config) and how to see the seed programmatically [here](../generators/dungeon-generator.md#change-the-configuration-from-a-script).