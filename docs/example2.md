---
id: example2
title: Example 2
---

In this tutorial, we will use [this tileset](https://0x72.itch.io/dungeontileset-ii) by [0x72](https://0x72.itch.io/). Be sure to check their work out if you like the tileset. We will not care about room decorations - we will use just basic walls, floor and  door tiles.

<div class="two-columns">
<div>

![](assets/example2_result1.png)
*Simple example*

</div>

<div>

![](assets/example2_result_reallife1.png)
*Real-life example*

</div>
</div>

> **Note:** I recommend reading [Example 1](example1.md) first as this is a little bit harder to setup and I will not repeat the basics here.

> **Note:** All files from this example can be found at *ProceduralLevelGenerator/Examples/Example2*.

## Simple example

The goal is to create two basic rectangular room remplates of different sizes and a room template for both horizontal and vertical corridors.

> **Note:** This tileset is trickier than the one used in [Example 1](example1.md) because there is an additional row of ligther tiles above all horizontal wall tiles. It will cause us problems when working with corridors.

### Basic rooms romplates

For this example, I am using doors with two different lengths (1 and 2). That means that we have to use the *Specific positions mode* mode because the *Simple mode* can only handle doors with the same length. As you can see below, the door positions look quite messy because they overlap.

<div class="two-columns">
<div>

![](assets/example2_room1.png)
*Smaller room*

</div>

<div>

![](assets/example2_room2.png)
*Bigger room*

</div>
</div>

### Corridors

As I said before, there is a problem with corridors when working with this tileset. To be more precise, there is a problem with vertical corridors because of the additional roow of tiles above wall tiles. If we were to design our vertical corridors as in Example 1, we would end up with something like this:

<div class="two-columns">
<div>

![](assets/example2_wrong_corridor.png)
*Incorrent vertical corridor*

</div>

<div>

![](assets/example2_wrong_corridor2.png)
*Incorrent connection*

</div>
</div>

Unfortunately, we cannot solve this problem just by designing our room templates in a different way. Therefore, we will have to solve this by a pipeline task - the procedure is described in the [Corridors correction](corridorsCorrection.md) tutorial. For this tutorial it is sufficient to know that we have to design our vertical corridors without the top and the bottom rows.

<div class="two-columns">
<div>

![](assets/example2_corridor_horizontal.png)
*Horizontal corridor*

</div>

<div>

![](assets/example2_corridor_vertical.png)
*Vertical corridor*

</div>
</div>

### Level graph

![](assets/example2_level_graph1.png)
*Level graph*

### Results

<div class="two-columns">
<div>

![](assets/example2_result2.png)
*Example result*

</div>

<div>

![](assets/example2_result3.png)
*Example result*

</div>
</div>

## Real-life example

To create something that is closer to a real-life example, we will add more rooms to the level graph, add special room templates for spawn and boss rooms and also two more corridor room templates.

### Spawn and boss rooms

<div class="two-columns">
<div>

![](assets/example2_spawn.png)
*Spawn room template*

</div>

<div>

![](assets/example2_boss.png)
*Boos room template*

</div>
</div>

### Additional room template

We can make generated dungeons more interesting by adding smaller room templates to our dead-end rooms.

<div class="two-columns">
<div>

![](assets/example2_room3.png)
*Additional room tempalte*

</div>
</div>

### Wider corridors

<div class="two-columns">
<div>

![](assets/example2_corridor_horizontal2.png)
*Wider horizontal corridor*

</div>

<div>

![](assets/example2_corridor_vertical2.png)
*Wider vertical corridor*

</div>
</div>

### Level graph

So the goal is to have more rooms than in the simple example and also a spawn room and a boss room. You can see one such level graph below.

![](assets/example2_level_graph2.png)
*Level graph*

### Results

![](assets/example2_result_reallife2.png)
*Example result*

![](assets/example2_result_reallife3.png)
*Example result*
