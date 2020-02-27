---
title: Fixed input
---

This pipeline task is used to setup an input for the dungeon generator from a given level graph. Prepared input is added to the payload. It is called *Fixed input* because the input is created through GUI and is therefore always the same. If you want to precedurally create level graphs, you have to implement your own pipeline task.

**Basic information:**
- Menu location: *Dungeon generator/Pipeline/Fixed input*
- Implementation: `FixedInputConfig` and `FixedInputTask` classes

**Options:**
- *Level Graph*: level graph to be used
- *Use Corridors*: whether corridors are enabled or not
    - with corridors enabled, there must be at least one corridor room template in a given level graph