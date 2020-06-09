---
title: Level structure and rooms data
---

import { Image, Gallery, GalleryImage } from "@theme/Gallery";

## Level structure

In the image below, we can see the structure of a level. The game object that holds the level has two children - **Tilemaps** game object and **Rooms** game object:

- **Tilemaps** game object holds all the tilemap layers.
- **Rooms** game object holds instance of all room templates that are used in the level. Name of each of the children is formed as **"{roomName} - {roomTemplate}"** to make it easier to find a specific room when debugging.

<Image src="img/v2/basics/level_structure.png" caption="Structure of the level" />

> **Note:** If you want to retrieve any of the game object from a script, the best-practice is to use the [GeneratorConstants](https://ondrejnepozitek.github.io/ProceduralLevelGenerator-UnityApiDocs/master/api/ProceduralLevelGenerator.Unity.Generators.Common.Utils.GeneratorConstants.html#fields) static fields instead of using hardcoded names.

## Rooms information

The generator also produces information about individual rooms in the level - their positions, which room template is used, what are neighbours of the room, etc. All this information is exposed through the [RoomInstance](https://ondrejnepozitek.github.io/ProceduralLevelGenerator-UnityApiDocs/master/api/ProceduralLevelGenerator.Unity.Generators.Common.Rooms.RoomInstance.html#properties) class.

There are at least two ways of getting an instance of this class:

- **From the game object of the room.**