[//]: # "Is it possible to track the progress of the generator?"

Short answer: No.

Unfortunately, due to the non-linear and randomized nature of Edgar's procedural level generation algorithm, it's not possible to track the progress of the generator in a meaningful way.

The algorithm works by placing rooms one by one until all of them are correctly positioned. However, as the placement of each room is influenced by the position of previously placed rooms, the generator may have to backtrack and "un-place" some already placed rooms to achieve the desired layout. The backtracking behavior of the algorithm is unpredictable and depends on a range of factors, such as the specific seed used and the placement of the first few rooms.

As a result, trying to track the progress of the generator is not a viable option. We recommend using a simple loading indicator to indicate that the level is being generated. This will provide players with an idea of the current state of the game, without causing any confusion or frustration caused by inaccurate progress tracking.