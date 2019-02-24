---
id: runPipeline
title: Run pipeline
---

There are currently two ways to run a pipeline. The first way is to add the *DungeonGeneratorPipeline* component to a GameObject in a scene, setup the pipeline in the editor and then click the *Generate* button. The second way is to create an instance of the `PipelineRunner` class and then call the `Run` method that accepts an array of pipeline items and a payload objact.