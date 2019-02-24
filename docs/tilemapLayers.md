---
id: tilemapLayers
title: ! Tilemap layers
---

The default structure of tilemaps was already described on the [Room templates](roomTemplates.md) page. The goal of this tutorial is to show you how we can override the default behaviour.

## Overriding defaults

To override the default behaviour, we have to create a class that inherits from the `AbstractTilemapLayersHandler` class:

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