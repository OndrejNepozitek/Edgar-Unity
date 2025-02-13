---
title: Generator setup
---

When you have a level graph and room templates prepared, you are now ready to generator you first dungeon. In order to do that, you have to set up the dungeon generator itself.

The setup is very easy:

1. Create an empty game object in the scene (name it however you like, I usually use *Dungeon Generator*)
2. Add the **Dungeon Generator (Gid3D)** component to that game object
3. Assign your *GeneratorSettings* to the **Generator Settings** field
4. Assign your level graph to the **Level Graph** field
5. Hit the **Generate dungeon** button or enable **Generate on start** and enter play mode

<Image src="3d/generator_setup/component.png" caption="Dungeon generator component" width="500px" />

The generator comes with a reasonable default configuration, so you should be able to generate your first level just by providing your level graph.

> **Note:** The goal of this guide is not to describe individual options of the generator but rather to describe how to get an instance of the generator with a reasonable default configuration. If you are interested in individual options, head to the [Dungeon generator](../generators/dungeon-generator.md) page.