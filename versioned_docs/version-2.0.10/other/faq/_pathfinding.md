[//]: # "How to handle pathfinding?"

It is common to have enemies that can pathfind towards the player. There are many ways to achieve that such as using the built-in [Unity navmesh](https://docs.unity3d.com/Manual/nav-BuildingNavMesh.html), using 2D navmesh from [NavMeshPlus](https://github.com/h8man/NavMeshPlus) or using the [A* Pathfinding Project](https://arongranberg.com/astar/) asset. If you have a static level, you can usually just press a button to initialize the pathfinding solution and that is all. In Edgar, it is almost as easy as that. The only difference is that you have to call the pathfinding script **after** the level is generated. The simplest way to achieve that is to write a simple [custom [post-processing script](../../generators/post-process.md#custom-post-processing) that runs after the generator produces a level. Each pathfinding asset usually provides an API that can be called from a script, which is exactly what you need to call from the post-processing logic.

### Additional resources

- How does post-processing work in Edgar - [Post-processing](../../generators/post-process.md)
- How to write custom post-processing logic - [Custom post-processing](../../generators/post-process.md#custom-post-processing)