---
title: Corridors correction
---

import { Image, Gallery, GalleryImage } from "@theme/Gallery";

With some tilesets, it is not possible to correctly connect corridors to rooms without the need of some postprocess. The goal of this tutorial is to demonstrate how to use pipeline tasks to correct such problems. I will use the tileset from [Example 2](example2.md) to describe the problem and show how to fix it.

## Problem

The problem with this tileset is that there is an additonal row of lighter tiles above all wall tiles. This is problematic because we must put all door positions on the outline of a room layout and that means that we cannot correctly connect corridor rooms. This can be seen in the images below.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/original/example2_room1.png" caption="Basic room template" />
    <GalleryImage src="img/original/example2_wrong_corridor.png" caption="Incorrent vertical corridor" />
    <GalleryImage src="img/original/example2_wrong_corridor2.png" caption="Incorrect connection" />
    <GalleryImage src="img/original/example2_wrong_corridor3.png" caption="Incorrect connection" />
</Gallery>

> **Note:** This problem could be also fixed by making it possible to have door position outside the outline, but that is currently not supported by the dungeon generator library. 

## Solution

The solution is remove the top and bottom rows from the vertical corridor room template (see below) and then write a script that fixes the connection.

<Gallery cols={2} fixedHeight>
    <GalleryImage src="img/original/example2_corridor_vertical.png" caption="Correct vertical corridor" />
    <GalleryImage src="img/original/corridors_correction_before_fix.png" caption="Before correction" />
    <GalleryImage src="img/original/corridors_correction_layout.png" caption="Correction layout" />
</Gallery>

> **Note:** Only the topmost and bottommost rows of tiles are used from the correction layout. The middle rows are present only to make it easier to draw the correction layout.

> **Note:** We can use the correction layout to correct corridors with different widths by simply repeating the middle tile.

The implementation itself is not very interesting but you can find it [here](https://github.com/OndrejNepozitek/ProceduralLevelGenerator-Unity/blob/master/Assets/ProceduralLevelGenerator/Examples/Example2/Pipeline%20tasks/CorridorsCorrectionConfig.cs). It is mostly just playing with indices and copying tiles from the correction layout to the dungeon.