[//]: # "How to spawn enemies?"

There are multiple approaches to spawning enemies in Edgar. Let's go through them to see what are their strengths and weaknesses.

### Approach 1: Put enemies inside room templates

The simplest solution is to put your enemy prefabs directly inside room templates. Everything that is inside a room template gets instantiated together with that room template. This might be a good solution if you want to just quickly test some enemies without any complex setup.

Pros of this solution:

- Simple setup
- Place the enemies exactly where you want them

Cons of this solution: 

- Always the same enemies in each room template (although each room template can have different enemies)
- Unable to scale the number/type of enemies based on the game difficulty

If you want to make this approach a bit better, you can always place more enemies than you want and then write a piece of post-processing logic that randomly hides some of the enemies, making the whole setup a bit less static.

### Approach 2: Put spawn point markers inside room templates

The main idea of this approach is that instead of placing enemy prefabs inside room templates, you use empty game objects as markers for places where enemies can be spawned. After a level is generated, you use a post-processing script to get all these marker objects and you choose which enemies to spawn. This approach is used in the [Dead Cells example](../../examples/dead-cells.md#enemies) in the Enemies section and it also includes a code snippet.

This approach has many advantages:

- You can place the markers only in places where it is suitable to spawn enemies
- You can place many markers but spawn only a few enemies, resulting in randomized enemy positions
- You can dynamically choose which enemies to spawn based on the biome, difficulty, current room, etc.
- You can choose the number of enemies to spawn (as long as it is not more than the total number of markers)

### Approach 3: Use colliders to find suitable spawning places

The third approach is completely procedural. After a level is generated, you can go through each room and generate random points inside the collider of the floor tilemap layer. For each such point, you can check if there is already an enemy/another object and if not, spawn an enemy there. This approach is used in the [Enter the Gungeon example](../../examples/enter-the-gungeon.md#enemies) in the Enemies section.

The main disadvantage of this approach is that it can be very slow. Depending on the number of enemies and the size of the room, the algorithm can have problems generating enemies in a reasonable time.

### Recommended approach

I usually recommend Approach 2 because it is very robust. It needs a bit of work to set up all the spawn point markers but otherwise it is a very good approach.