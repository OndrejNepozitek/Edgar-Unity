---
title: FAQ
---

## Rooms are generated too close to one another

The generator will often produce a level where the walls of one room are right next to the walls of another room. Usually, this is perfectly okay, but sometimes, it may cause problems. For example, if rule tiles are used for walls, a room that is too close may interfere with tiles in a different room.

To solve this problem, look for the `Minimum Room Distance` field in the dungeon generator inspector and increase the value to *2*. You can increase the value even more for even larger distance between rooms. Just keep it mind that every increase of this field makes it harder for the generator to produce a level (and can, therefore, lead to timeouts).