[//]: # "How to generate levels without corridors?"

In the majority of example scenes that are included in Edgar, you can see corridors connecting individual rooms in the level. However, it is also possible to generate levels with no corridors between rooms. 

### Approach 1 (Not recommended)

In the "Input Config" section of the generator component, you can uncheck the "Use Corridors" field. However, this is not the recommended approach. In Edgar, corridors are very useful. After a level is generated, corridors are placed only after non-corridor rooms. As a result, corridors overwrite the walls of rooms, carving holes in them and making it possible to go from one room to another. If you uncheck the "Use Corridors" option, you will end up with a bunch of rooms but no way to traverse the level. This configuration is very niche as it requires you to write some post-processing logic to add doors/holes to the rooms.

### Approach 2 (Recommended)

The recommended approach is to have a pair (or more) of specially designed corridor room templates. The trick is that these corridors are only 2 tiles wide so it looks like there are no corridors at all. However, these corridors allow you to control how exactly two neighboring rooms connect. Below is an example of corridor room templates used in the [Dead Cells](../../examples/dead-cells.md#corridors) example scene.

<Gallery cols={2} fixedHeight>
    <Image src="2d/examples/dead_cells/underground/hor.png" caption="2-tile wide horizontal corridor" />
    <Image src="2d/examples/dead_cells/underground/ver.png" caption="2-tile wide vertical corridor" />
</Gallery>

### Additional resources

- The special corridors are used in the [Dead Cells example scene](../../examples/dead-cells.md#corridors)
- The special corridors are used in the [Platformer 1 example scene](../../examples/platformer-1.md#doors-and-corridors)