[//]: # "What to do with a `TimeoutException`?"

Sometimes, when you want to generate a level, you get a `TimeoutException` in the console instead. The error means that the generator was not able to produce a level in a given time limit which is 10 seconds by default. The error can have two different meanings: 

- the level graph is too hard for the generator (there are too many rooms, too many cycles, restrictive room templates, etc.)
- or there is a problem somewhere in the configuration (maybe the doors of two neighboring room templates are not compatible)

Usually, it is the second case. To help you fix the error, the generator dumps some diagnostic information *above* the error in the console. The type of information that you can find in the console is for example that the lengths of doors are suspicious or that there are maybe too many rooms in the level graph.

If you are not able to fix the problem yourself, come to our Discord and I will try to help. Also, you can read the [Performance tips](../../basics/performance-tips.md) page.