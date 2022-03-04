<h1 align="center">
  <br>
  Edgar for Unity
  <br>
</h1>

<h4 align="center">Configurable procedural level generator for Unity</h4>

<p align="center">
  <a href="https://ondrejnepozitek.github.io/Edgar-Unity/docs/introduction/"><img src="https://img.shields.io/badge/online-docs-important" /></a>
  <a href="https://github.com/OndrejNepozitek/Edgar-Unity/workflows/Build/badge.svg"><img src="https://github.com/OndrejNepozitek/Edgar-Unity/workflows/Build/badge.svg" /></a>
  <a href="https://github.com/OndrejNepozitek/Edgar-Unity/releases/tag/v2.0.0-alpha.5"><img src="https://img.shields.io/github/v/release/OndrejNepozitek/Edgar-Unity" /></a>
  <a href="https://assetstore.unity.com/packages/tools/utilities/edgar-pro-procedural-level-generator-212735?aid=1100lozBv&pubref=edgar-readme"><img src="https://img.shields.io/badge/buy-PRO%20version-important" /></a>
  <a href="https://img.shields.io/badge/Unity-%3E%3D%202018.4-blue"><img src="https://img.shields.io/badge/Unity-%3E%3D%202018.4-blue" /></a>
  <a href="https://discord.gg/syktZ6VWq9"><img src="https://img.shields.io/discord/795301453131415554?label=discord" /></a>
</p>

<p align="center">
  <a href="#introduction">Introduction</a> |
  <a href="#key-features">Key features</a> |
  <a href="#pro-version">PRO version</a> |
  <a href="#limitations">Limitations</a> |
  <a href="#getting-started">Getting started</a> |
  <a href="#installation">Installation</a> |
  <a href="#workflow">Example</a> |
  <a href="#get-in-touch">Get in touch</a>
</p>

<!--
<p align="center">
  <a href="https://ondrejnepozitek.github.io/Edgar-Unity/">Website</a> |
  <a href="https://ondrejnepozitek.github.io/Edgar-Unity/docs/introduction/">Documentation</a> |
  <a href="https://github.com/OndrejNepozitek/Edgar-Unity/releases">Releases</a> |
  <a href="https://ondrejnepozitek.itch.io/edgar-pro">PRO version on itch.io</a> |
</p>
-->

<!--
<p align="center">
  Need info? Check the <a href="https://ondrejnepozitek.github.io/Edgar-Unity/docs/introduction/">docs</a> or <a href="https://ondrejnepozitek.github.io/Edgar-Unity/">website</a> |
  Or <a href="https://github.com/OndrejNepozitek/Edgar-Unity/issues/new">create an issue</a>
</p>
-->
                                                   
<p align="center">
<img src="http://zuzka.nepozitek.cz/output4.gif" width='600'"
</p>
                                                             
## Introduction

This project is a Unity plugin for procedural generation of 2D dungeons (and platformers) and aims to give game designers a **complete control** over generated levels. It combines **graph-based approach** to procedural generation with **handmade room templates** to generate levels with a **feeling of consistency**. Under the hood, the plugin uses [Edgar for .NET](https://github.com/OndrejNepozitek/Edgar-DotNet).

### Graph-based approach

You decide exactly how many rooms you want in a level and how they should be connected, and the generator produces levels that follow exactly that structure. Do you want a boss room at the end of each level? Or da shop room halfway through the level? Everything is possible with a graph-based approach.

### Handmade room templates

The appearance of individual rooms is controlled with so-called room templates. These are pre-authored building blocks from which the algorithm chooses when generating a level. They are created with Unity tilemaps, but they can also contain additional game objects such as lights, enemies or chests with loot. You can also assign different room templates to different types of rooms. For example, a spawn room should probably look different than a boss room.

## Key features

- Procedural dungeon generator
- Describe the structure of levels with a graph of rooms and connections 
- Control the appearance of rooms with handmade room templates 
- Connect rooms either directly with doors or with short corridors
- Easy to customize with custom post-processing logic
- Supports Unity 2018.4 and newer
- Currently works only in 2D but may support 3D in future
- Comprehensive [documentation](https://ondrejnepozitek.github.io/Edgar-Unity/docs/introduction/)
- Multiple example scenes included

## PRO version

There are two versions of this asset - free version and PRO version. The free version contains the core functions of the generator and should be fine for simple procedural dungeons. The PRO version can be bought [on the Unity Asset Store](https://assetstore.unity.com/packages/tools/utilities/edgar-pro-procedural-level-generator-212735?aid=1100lozBv&pubref=edgar-readme) and contains some additional features. As of now, the PRO version contains features like platformer generator or isometric levels and also two advanced example scenes. If you like this asset, please consider buying the PRO version to support the development.

- Coroutines - Call the generator as a coroutine so that the game does not freeze when generating a level ([docs](https://ondrejnepozitek.github.io/Edgar-Unity/docs/generators/dungeon-generator#pro-with-coroutines))
- Custom rooms - It is possible to add additional fields to rooms and connections in a level graph ([docs](https://ondrejnepozitek.github.io/Edgar-Unity/docs/basics/level-graphs#pro-custom-rooms-and-connections))      
- Platformers - Generator that is able to produce platformer levels ([docs](https://ondrejnepozitek.github.io/Edgar-Unity/docs/generators/platformer-generator), [example](https://ondrejnepozitek.github.io/Edgar-Unity/docs/examples/platformer-1))
- Isometric - Simple example of isometric levels ([example](https://ondrejnepozitek.github.io/Edgar-Unity/docs/examples/isometric-1))
- Dead Cells - Tutorial on how to generate levels that are similar to Dead Cells ([docs](https://ondrejnepozitek.github.io/Edgar-Unity/docs/examples/dead-cells))
- Enter the Gungeon - Tutorial on how to generate levels that are similar to Enter the Gungeon ([docs](https://ondrejnepozitek.github.io/Edgar-Unity/docs/examples/enter-the-gungeon/))
- Custom input - Modify a level graph before it is used in the generator (e.g. add a random secret room) ([docs](https://ondrejnepozitek.github.io/Edgar-Unity/docs/generators/custom-input))
- Fog of War - Hide rooms in a fog until they are explored by the player ([docs](https://ondrejnepozitek.github.io/Edgar-Unity/docs/guides/fog-of-war))
- Minimap support ([docs](https://ondrejnepozitek.github.io/Edgar-Unity/docs/guides/minimap))
- Door sockets - Use door sockets to specify which doors are compatible ([docs](https://ondrejnepozitek.github.io/Edgar-Unity/docs/guides/door-sockets))
- Directed level graphs - Use directed level graphs together with entrance-only and exit-only doors to have better control over generated levels ([docs](https://ondrejnepozitek.github.io/Edgar-Unity/docs/guides/directed-level-graphs))

## Limitations

- Still in alpha version - there may be some breaking changes in the API
- Some level graphs may be too hard for the generator - see the [guidelines](https://ondrejnepozitek.github.io/Edgar-Unity/docs/basics/performance-tips)
- The graph-based approach is not suitable for large levels - we recommend less than 30 rooms
- Not everything can be configured via editor - some programming knowledge is needed for more advanced setups
                      
## Getting started

Install the asset (instructions are below) and head to the [documentation](https://ondrejnepozitek.github.io/Edgar-Unity/docs/introduction). The documentation describes all the basics and also multiple example scenes that should help you get started. 
                      
## Installation

There are several ways of installing the plugin:

### via .unitypackage

Go to Releases and download the unitypackage that's included in every release. Then import the package to Unity project (*Assets -> Import package -> Custom package*).

#### How to update
In order to be able to download a new version of the plugin, **we recommend to not change anything inside the Assets/ProceduralLevelGenerator folder**. At this stage of the project, files are often moved, renamed or deleted, and Unity does not handle that very well.

The safest way to update to the new version is to completely remove the old version (*Assets/ProceduralLevelGenerator* directory) and then import the new version. (Make sure to backup your project before deleting anything.)      

### via Package Manager
Add the following line to the `packages/manifest.json` file under the `dependencies` section (you must have git installed):
```
 "com.ondrejnepozitek.edgar.unity": "https://github.com/OndrejNepozitek/Edgar-Unity.git#upm"
```
To try the examples, go to the Package Manager, find this plugin in the list of installed assets and import examples.

> Note: When importing the package, I've got some weird "DirectoryNotFoundException: Could not find a part of the path" errors even though all the files are there. If that happens to you, just ignore that.

#### How to update
After installing the package, Unity adds something like this to your `manifest.json`:

```
  "lock": {
    "com.ondrejnepozitek.edgar.unity": {
      "hash": "fc2e2ea5a50ec4d1d23806e30b87d13cf74af04e",
      "revision": "upm"
    }
  }
```

Remove it to let Unity download a new version of the plugin.

### Do not clone the repository

When installing the plugin, use the two methods described above. If you clone the repository directly, you will probably get an error related to some tests and missing types/namespaces. This error is caused by the fact that the plugin is developed with Unity 2018.4 (for better compatibility), but the assembly definition format was changed in newer versions (a reference to the Edgar .NET assembly is missing).
                                                             
## Workflow 

### 1. Draw rooms and corridors

![](https://ondrejnepozitek.github.io/Edgar-Unity/intro/room_templates_multiple.png)

### 2. Prepare the structure of the level

<img src="https://ondrejnepozitek.github.io/Edgar-Unity/intro/level_graph.png" height="500" />

### 3. Generate levels

![](https://ondrejnepozitek.github.io/Edgar-Unity/intro/generated_levels_multiple.png)

## Examples

![](https://ondrejnepozitek.github.io/Edgar-Unity/intro/example1_result1.png)

![](https://ondrejnepozitek.github.io/Edgar-Unity/intro/example1_result2.png)

![](https://ondrejnepozitek.github.io/Edgar-Unity/intro/example2_result1.png)

![](https://ondrejnepozitek.github.io/Edgar-Unity/intro/example2_result2.png)

## Get in touch

If you have any questions or want to show off what you created with Edgar, come chat with us in our [discord server](https://discord.gg/syktZ6VWq9). Or if you want to contact me personally, use my email ondra-at-nepozitek.cz or send me a message on Twitter at @OndrejNepozitek.

## Terms of use

The plugin can be used in both commercial and non-commercial projects, but **cannot be redistributed or resold**. If you want to include this plugin in your own asset, please contact me, and we will figure that out.


