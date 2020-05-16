---
title: Migration from v1 to v2
---

> **Make sure to have a backup of your work before you decide to migrate.**

### **Dungeon Generator Pipeline** replaced with **Dungeon Generator** script

We want to make the generator as easy to use as possible and the whole pipeline was quite hard to setup. Even if we did not need any custom functionality, we still had to create several scriptable objects to create the pipeline. We decided to replace the pipeline with a much simpler script (**Dungeon Generator**) which should be hopefully easier to use and as flexible as the pipeline.

#### Migration

- Read the [Generator setup](../basic/generator-setup) page and add the **Dungeon Generator** script to an empty game object
- Instead of using the **Fixed Input** task, assign your level graph directly to the *Level Graph* field in the generator
- Instead of using *pipeline tasks* to implement post processing logic, see the [Post processing](../generators/post-process) and move your logic to a post process task

### Room templates migration

The structure of room templates is now slightly different:

- tilemaps are now children of the *Tilemaps* game object and of the root game object
- *Room template* script is added to the root game object
- the sorting order of *Walls* is now 1 and the sorting order of *Floor* is now 0

You can convert a room template automatically by following these steps:

- open the room template in the **prefab mode** (it does not work if we simply put the prefab into a scene)
- add **Room Template Migration V1 to V2** component to the root game object of the prefab
- press the **Convert** button of the component
- save the prefab