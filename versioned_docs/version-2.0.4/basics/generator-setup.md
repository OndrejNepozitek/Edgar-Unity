---
title: Generator setup
---

With our level graph prepared, we are now ready to generate our very first dungeon. In order to do that, we have to set up our procedural dungeon generator.

The setup is very easy:

1. Create an empty game object in the scene
2. Add the **Dungeon Generator** component to that game object
3. Assign your level graph to the **Level Graph** field
4. Hit the **Generate dungeon** button or enable **Generate on start** and enter play mode

<Image src="2d/generators/dungeon_generator_inspector.png" caption="Dungeon generator runner" width="500px" />

> **Note:** The goal of this guide is not to describe individual options of the generator but rather to describe how to get an instance of the generator with a reasonable default configuration. If you are interested in individual options, head to the [Dungeon generator](../generators/dungeon-generator.md) page.