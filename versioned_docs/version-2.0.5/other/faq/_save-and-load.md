[//]: # "How to implement save and load system?"

There is no built-in feature for saving and loading generated levels because we feel like each game has very different requirements and it would be very hard to come up with a universal save system that would work for everyone. However, you should be able to implement your own save and load system.

### Step 1 - Store the seed of the generator

In the majority of procedural level generators, there is something called the **seed of the generator**. It is (usually) a number that controls the randomness of the generator. If you use the same seed twice, the generator will produce the same level. You can learn more about seeds and how to use them on the [Dungeon generator](../../generators/dungeon-generator.md#controlling-the-seed-of-the-generator) page.

Knowing how seeds work can help you implement the save and load system. When you save the game, you store the seed of the generator. That is a very simple task because the seed is just a number so it is very easy to save to a file. When you want to load the level, you will read the seed and configure Edgar to generate a level with that seed. Edgar will generate the same level and run all the post-processing steps.

> **Note**: If you have some custom post-processing logic doing something random, you must make sure to use a seeded random generator. For example, if you want to spawn random enemies at random locations in a post-processing code, you should use the provided `Random` property available in all post-processing classes, or you need to handle the seeds yourself. If you do not do that, the load system might not be fully deterministic. 

### Step 2 - Save the dynamic parts of the level

Most levels are not static - they change in some way after they are generated. For example, enemies are defeated or damaged, chests are looted, and doors are opened. All these changes you have to track yourself. You must be able to save these changes when the game gets saved and apply these changes to the level after it is generated with the original seed. There are no shortcuts built-in for this in Edgar as each game has different needs and different entities to track.

### Additional resources

- What is the seed of the generator and how to use it - ([Controlling the seed of the generator](../../generators/dungeon-generator.md#controlling-the-seed-of-the-generator) in the Dungeon generator guide)