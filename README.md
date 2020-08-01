<h1 align="center">
  <br>
  Edgar for Unity
  <br>
</h1>

<h4 align="center">Configurable procedural level generator for Unity</h4>

<p align="center">
  <a href="https://github.com/OndrejNepozitek/Edgar-Unity/workflows/Build/badge.svg">
     <img src="https://github.com/OndrejNepozitek/Edgar-Unity/workflows/Build/badge.svg" />
  </a>
  <a href="https://github.com/OndrejNepozitek/Edgar-Unity/releases/tag/v2.0.0-alpha.5">
     <img src="https://img.shields.io/github/v/release/OndrejNepozitek/Edgar-Unity" />
  </a>
  <a href="https://ondrejnepozitek.itch.io/edgar-pro">
     <img src="https://img.shields.io/badge/itch.io-Edgar%20PRO-important" />
  </a>
  <a href="https://img.shields.io/badge/Unity-%3E%3D%202018.4-blue">
     <img src="https://img.shields.io/badge/Unity-%3E%3D%202018.4-blue" />
  </a>
</p>
                                                   
<p align="center">
<img src="http://zuzka.nepozitek.cz/output4.gif" width='600'"
</p>
                                                             
## Introduction

This project is a Unity plugin for procedural generation of 2D dungeons (and platformers) and aims to give game designers a **complete control** over generated levels. It combines **graph-based approach** to procedural generation with **handmade room templates** to generate levels with a **feeling of consistency**. Under the hood, the plugin uses [Edgar for .NET](https://github.com/OndrejNepozitek/Edgar-DotNet).

### Graph-based approach

You decide exactly how many rooms you want in a level and how they should be connected, and the generator produces levels that follow exactly that structure. Do you want a boss room at the end of each level? Or da shop room halfway through the level? Everything is possible with a graph-based approach.

### Handmade room templates

The appearance of individual rooms is controlled with so-called room templates. These are pre-authored building blocks from which the algorithm chooses when generating a level. They are created with Unity tilemaps but they can also contain additional game objects such as lights, enemies or chests with loot. You can also assign different room templates to different types of rooms. For example, a spawn room should probably look different than a boss room.

## Key features

- Describe the structure of levels with a graph of rooms and connections
- Control the appearance of rooms with handmade room templates
- Connect rooms either directly with doors or with short corridors
- Easy to customize with custom post-processing logic

- **Easy to customize.** The plugin is ready to be customized and extended.
- **Supports Unity 2018.4 and newer**.
- **2 example scenes included.**

## Limitations
- **Alpha version.** There may be some **breaking changes** in the API.
- **Some inputs are too hard for the generator.** You need to follow some guidelines in order to achieve good performance.
- **Not suitable for large levels.** The generator usually works best for levels with less than 30 rooms.
- **Not everything can be configured via editor.** You need to have programming knowledge in order to generate anything non-trivial.
                                                                                                                    
## Installation

There are several ways of installing the plugin:

### via Package Manager
Add the following line to the `packages/manifest.json` file under the `dependencies` section (you must have git installed):
```
 "com.ondrejnepozitek.procedurallevelgenerator": "https://github.com/OndrejNepozitek/ProceduralLevelGenerator-Unity.git#upm"
```
To try the examples, go to the Package Manager, find this plugin in the list of installed assets and import examples.

> Note: When importing the package, I've got some weird "DirectoryNotFoundException: Could not find a part of the path" errors even though all the files are there. If that happens to you, just ignore that.

#### How to update
After installing the package, Unity adds something like this to your `manifest.json`:

```
  "lock": {
    "com.ondrejnepozitek.procedurallevelgenerator": {
      "hash": "fc2e2ea5a50ec4d1d23806e30b87d13cf74af04e",
      "revision": "upm"
    }
  }
```

Remove it to let Unity download a new version of the plugin.

### via .unitypackage

Go to Releases and download the unitypackage that's included in every release. Then import the package to Unity project (*Assets -> Import package -> Custom package*).

#### How to update
In order to be able to download a new version of the plugin, **we recommend to not change anything inside the Assets/ProceduralLevelGenerator folder**. At this stage of the project, files are often moved, renamed or deleted, and Unity does not handle that very well.

The safest way to update to the new version is to completely remove the old version (*Assets/ProceduralLevelGenerator* directory) and then import the new version. (Make sure to backup your project before deleting anything.)                                                         
                                                             
## Workflow 

### 1. Draw rooms and corridors

![](https://ondrejnepozitek.github.io/Edgar-Unity/img/v2/room_templates_multiple.png)

### 2. Prepare the structure of the level

![](https://ondrejnepozitek.github.io/Edgar-Unity/img/v2/examples/example1_level_graph2.png)

### 3. Generate levels

![](https://ondrejnepozitek.github.io/Edgar-Unity/img/v2/generated_levels_multiple.png)

## Examples

![](https://ondrejnepozitek.github.io/Edgar-Unity/img/original/example1_result1.png)

![](https://ondrejnepozitek.github.io/Edgar-Unity/img/original/example1_result_reallife1.png)

![](https://ondrejnepozitek.github.io/Edgar-Unity/img/original/example2_result1.png)

![](https://ondrejnepozitek.github.io/Edgar-Unity/img/original/example2_result_reallife1.png)

## Terms of use

The plugin can be used in bot commercial and non-commerical projects but **cannot be redistributed or reselled**. If you want to include this plugin in your own asset, please contact me and we will figure that out.


