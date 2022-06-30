---
id: introduction
title: Introduction
---

This is the part of the documentation that focuses on the 3D version of Edgar. Some guides are shared between the 2D and 3D versions because the concepts are very similar.

## Features

See also the [main introduction](../introduction.md) in the shared docs.

- Generate 3D levels with a predefined structure
- Room templates have to be aligned to the grid but can be of any shape (not only rectangles)
- Rooms can have different elevations (but they cannot be on top of each other) - more info [here](./guides/different-elevations.md)

## Limitations

- It is not possible to generate multi-story dungeons because the algorithm works on a 2D grid
- You must make sure that all your room templates are aligned to the underlying grid

## Differences: 3D vs 2D version

- Room templates are allowed to be rotated in generated 3D levels
- Doors are handled differently in the 3D version
- Some features like Fog of War are only available in the 2D version

## Examples

<Gallery cols={2} fixedHeight>
    <Image src="3d/examples/basics/result_reallife_3.png" caption="Example result" />
    <Image src="3d/examples/dungeon1/result_5_4.png" caption="Example result" />
</Gallery>