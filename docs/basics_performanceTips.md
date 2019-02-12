---
id: basics_performanceTips
title: Performance tips
---

## Room templates

**Try to provide as many door positions as possible.** The core idea of the algorithm is that it picks a random room and slightly changes its position, hopefully making new connections with neighbouring rooms. If there are too few available door positions, the algorithm will often break already existing connections when trying to connect the room to other neighbours, resulting in a poor performance.

## Level graphs

**Limit the number of rooms.** The core idea of the algorithm is that it decomposes the level graph into smaller subgraphs and tries to lay them out one at a time, connecting them to already laid out rooms. If this step fails, the algorithm needs to backtrack to a previous configuration. If we have too many rooms, backtracking can get quite costly, making the performance bad. We successfully generated dungeons with up to 60 rooms but it also depends on the overall complexity of the level graph - see the next point.

**Limit the number of cycles.** Cycles are very hard to lay out so make sure that there are not too many of them. If you are not sure if cycles are the problem, try to remove some edges that form cycles and see if it makes the performance better.

**Make sure that default room templates make sense for rooms without their own room templates.** The easiest way to configure room templates it to add all templates as *Default room templates*, making them available to all rooms in the level graph. Now imagine that you design a room template for your dead-end rooms and that it can be connected via doors to only one neighbouring room. Adding this room template to *Default room templates* will make the performance worse because the algoritm will waste time trying to use it for rooms that have more than 1 neighbour. Instead, you should add this room template only to rooms that have just a single neighbour.

