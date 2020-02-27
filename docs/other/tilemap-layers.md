---
title: Tilemap layers
---

import { Image, Gallery, GalleryImage } from "@theme/Gallery";

The default structure of tilemaps was already described on the [Room templates](roomTemplates.md) page. The goal of this tutorial is to show you how we can override the default behaviour.

## Overriding defaults

To override the default behaviour, we have to create a class that inherits from the `AbstractTilemapLayersHandler` class. You can see the default implementation in the `TilemapLayersHandler` class.

```csharp
public abstract class AbstractTilemapLayersHandler : ScriptableObject, ITilemapLayersHandler
{
    /// <summary>
    /// Initializes the structure of tilemaps of a given gameObject.
    /// </summary>
    /// <remarks>
    /// Adds child GameObjects with tilemap components attached. Multiple tilemaps are
    /// used to layer individual tiles over one another. This is also the place to add
    /// colliders and setup sorting order.
    /// </remarks>
    /// <param name="gameObject">Parent GameObject of created tilemaps.</param>
    public abstract void InitializeTilemaps(GameObject gameObject);
}
```

Then you have to assign this tilemap layers handler to the *Tilemap Layers Handler* field of the payload initializer that is used by the generator. That will ensure that all generated dungeons will have this structure of tilemaps. The last step is to ensure that all room templates have the same structure of tilemaps as generated dungeons. With the default structure, we use the *Default Room Template Initializer* script to initialize room templates. If we want to use a different structure, we have to use the *Configurable Room Template Initializer* and assign our new tilemap layers handler.

<Image src="img/original/configurable_room_template_initializer.png" caption="Configurable room template initializer script" />

> **Note:** It is **VERY IMPORTANT** to to choose the right structure of tilemap layers before creating a larger number of room templates because it is very difficult to convert room templates from one structure to another.