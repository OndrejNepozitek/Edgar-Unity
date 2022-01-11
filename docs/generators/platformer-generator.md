---
title: (PRO) Platformer generator
---

## Minimal setup

- Add the **Platformer Generator Component** to any Game Object in the Scene.
- Assign your level graph to the **Level Graph** field.
- Hit the **Generate platformer** button or enable **Generate on start** and enter play mode

## Configuration and usage

The configuration is currently the same as for the Dungeon Generator. See [this](../generators/dungeon-generator#configuration) page. The only difference is that if we want to use the generator from code, we use the `PlatformerGenerator` class.

## Default tilemaps structure

Platformer room templates can be created via <Path path="2d:Platformer room template" /> and their default tilemaps structure is the following:

- **Background**
- **Walls** - collider
- **Platforms** - collider and platform effector
- **Collideable** - collider
- **Other 1**
- **Other 2**

## Limitations

There are some limitation regarding the platformer generator.

### Acyclic level graphs

We should use only **acyclic** graphs, i.e. graphs without cycles. The room templates for platformers are often too restrictive to allow cycles. The generator currently allows graphs with cycles but it often happens that it is not able to generate any level.

### Solvability of generated levels

The generator cannot guarantee that all the levels are solvable, i.e. it is possible to successfully traverse generated levels without being stuck at dead-ends caused by impossible jumps, etc. Probably the easiest way to handle that is to design room templates and level graphs in such a way that the generator cannot connect two rooms in a way that it is not possible go one from the first one to the other one.