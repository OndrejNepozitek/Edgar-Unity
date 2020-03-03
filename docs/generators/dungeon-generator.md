---
title: Dungeon generator
---

import { Image, Gallery, GalleryImage } from "@theme/Gallery";

## Minimal setup

- Add the **Dungeon Generator Component** to any Game Object in the Scene.
- Assign your level graph to the **Level Graph** field.
- Hit the **Generate dungeon** button or enable **Generate on start** and enter play mode

<Image src="img/v2/generators/dungen_generator_inspector.png" caption="Dungeon generator runner" width="500px" />

## Configuration

#### Input config

- **Level Graph** - Level graph that should be used. Must not be null.
- **Use corridors** - Whether corridors should be used between neighboring rooms. If enabled, corridor room templates must be provided in the level graph.

#### Generator config

- **Root Game Object** - Game Object to which the generated level will be attached. New Game Object will be created if null.
- **Timeout** - How long (in milliseconds) should we wait for the algorithm to generate a level. We may sometimes create an input that is too hard for the algoritm so it is good to stop after some amount of time with an error.
- **Repeat Mode Override** - Whether to override the repeat mode of individual room templates.
    - **No override** - Nothing is overriden, keep repeat modes from room templates.
    - **Allow repeat** - All room templates may repeat in generated levels.
    - **No immeadiate** - Neighboring room must have different room templates.
    - **No repeat** - All rooms must have different room templates.

> **Note:** If you provide too few room templates, they may repeat in generated levels even if you choose the **No immeadiate** or **No repeat** options. To make sure that the repeat mode is satisifed, please provide enough room templates to choose from.

#### Post processing config

Please refer see the [Post processing](../generators/post-process) page to find detailed information this configuration.

- **Initialize Shared Tilemaps** - Whether to initialize tilemaps that will hold the generated level.
- **Tilemap Layers Handler** - Which tilemap layers handler should be used to initialize shared tilemaps. Uses the `DungeonTilemapLayersHandler` if not set.
- **Copy Tiles To Shared Tilemaps** - Whether to copy tiles from individual room template to the shared tilemaps.
- **Center Grid** - Whether to move the level so that its center is approximately at (0,0). Useful for debugging in Scene view in editor.
- **Disable Room Template Renderers** - Whether to disable tilemap renderers of individual rooms, useful only when *Copy Tiles To Shared Tilemaps* is enabled.
- **Disable Room Template Colliders** - Whether to disable tilemap colliders of individual rooms, useful only when *Copy Tiles To Shared Tilemaps* is enabled.

> **TODO:** Link to page with tilemap layers.

#### Other config

- **Use Random Seed** - Whether to use a random seed for each new generated level. 
- **Random Generator Seed** - Random generator seed that will be used when **Use Random Seed** is disabled. Useful for debugging.
- **Generate On Start** - Whether to generate a new level when play mode is entered.

## (TODO) Call the generator from script