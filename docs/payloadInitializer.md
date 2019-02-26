---
id: payloadInitializer
title: Payload initializer
---

This is not really a pipeline task but it is closely related to the pipeline as it initializes the payload.

**Basic information:**
- Menu location: *Dungeon generator/Pipeline tasks/Payload initializer*
- Implementation: `PayloadInitializer` class
- Creates payload of type `PipelinePayload`

**Options:**
- *Tilemap Layers Handler*: sets the tilemap layers handler or uses the default one if not set, see 
- *Use Random Seed*: whether wa want to use a random seed for the random numbers generator or use a fixed seed
- *Random Generator Seed*: which seed to use for the random numbers generators
- *Print Used Seed*: whether the current seed of the generator should be printed to the console window
    - useful if we use a random seed and see there is something wrong with a generated dungeon and want to investigate that