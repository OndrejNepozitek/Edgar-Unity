---
title: Generator setup
---

import { Image, Gallery, GalleryImage } from "@theme/Gallery";

With our level graph prepared, we are now ready to generate our very first dungeon. In order to do that, we have to setup our procedural dungeon generator. There are two ways to do that - you either use the prefab that comes with the plugin or you create your own instance of the generator.

> **Note:** The goal of this guide is not to describe individual options of the generator but rather to describe how to get an instance of the generator with reasonable default configuration. If you are interested in individual options, head to the *Pipeline tasks* section.

## Using prefab

The easiest way to start generating dungeons is to use the *Dungeon Generator* prefab that is located at *ProceduralDungeonGenerator/Examples/BasicDungeonGenerator*. Simply drag and drop it to the scene and open in in the Inspector. It should look like the image below.

<Image src="img/original/dungeon_generator_inspector1.png" caption="Basic dungeon generator script" />

Now you can simply drop you level graph to the *Level Graph* property in the generator pipeline and then hit the *Generate* button to generate a layout.

> **Note:** The dungeon generator script is made of ScriptableObjects. Therefore, you cannot duplicate this prefab and think that you have a separate copy of the generator as it is linked to the same instance of the ScriptableObjects. If you want to create another instance of the generator, follow the *Manual setup* section below.

## Manual setup

To create our own dungeon generator, we have to follow a few simple steps:

1. Create an empty GameObject and add the *DungeonGeneratorPipeline* component
2. Create [Fixed input](fixedInput.md) ScriptableObject (*Dungeon generator/Pipeline/Fixed input*) and add it as the first element in the *GeneratorPipeline*
3. Create [Graph based generator](graphBasedGenerator.md) ScriptableObject (*Dungeon generator/Generators/Graph based generator*) and add it as the second element in the *GeneratorPipeline*
4. Create [PayloadInitializer](payloadInitializer.md) ScriptableObject (*Dungeon generator/Pipeline/Payload initializer*) and add it ti the *Payload Initializer* field
