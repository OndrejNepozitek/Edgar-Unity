---
title: Post-processing
---

After a level is generated, you may often want to run some additional logic like spawning enemies, etc. This can be achieved by providing your own post-processing logic that will be called after the level is generated, and will be provided with information about the level. 

To better understand how the generator works, I will first describe which post-processing is done by the generator itself and then provide ways to extend this behaviour and provide your own logic. You can skip right to [Custom post-processing](./post-processing.md#custom-post-processing) if that is what you are looking for.

## Built-in post-processing steps

#### 1. Center level

In this step, the whole level is moved in a way that its centre ends up at (0,0). This step is only used to make it easier to go through multiple generated levels without having to move the camera around.

#### 2. Process connectors and blockers

In this step, the generator goes through the door handlers of individual room templates and spawns blockers on tiles where no door was added, and adds connectors on tiles where a door was added.

## Custom post-processing

> **Note:** This part of the guide lives in the 2D section of the documentation: [here](../../generators/post-process.md#custom-post-processing)
>
> The concepts are the same in the 2D version, you just have to replace the `Grid2D` class name suffix with `Grid3D`. For example, `DungeonGeneratorPostProcessingGrid2D` -> `DungeonGeneratorPostProcessingGrid3D`.

<Difference2D3D />