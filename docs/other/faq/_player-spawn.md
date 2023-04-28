[//]: # "How to spawn the player in a specific room"

The easiest solution is to design a special *spawn* room template and place the player prefabs inside that room template. Then, make this room template the only room template for the *Spawn* room in your level graph. This approach is described in [Example 1](../../examples/example-1.md#spawn-room).

Another option is to move your player in a post-processing logic. Instead of putting the player prefabs inside the *Spawn* room template, you can just mark the spawn spot with an empty game object. Then, after a level is generated, you can run a post-processing script that will move the player to the marked position. This approach is described in the [Dead Cells example](../../examples/dead-cells.md#spawn-position).