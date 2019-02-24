---
id: graphBasedGenerator
title: Graph based generator
---

This pipeline task is used to generate to generate a dungeon using the graph-based dungeon generator algorithm. The task takes its input from the payload.

**Basic information:**
- Menu location: *Dungeon generator/Generators/Graph based generator*
- Implementation: `GraphBasedGeneratorConfig` and `GraphBasedGeneratorTask` classes

**Options:**
- *Show Debug Info*: whether to show how debug information such as time to generate the layout, how many iterations of the algorithm were needed, etc.
- *Center Grid*: whether to center the grid so that the dungeon appears in the center
- *Apply template*: whether to copy tiles from individual room templates to the shared dungeon tilemap
    - set to false if you want to modify the output of the generator 
- *Timeout* (in milliseconds): how much time can the generator spent before aborting