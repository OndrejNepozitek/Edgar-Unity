---
title: Run pipeline
---

There are currently three ways to run a pipeline:
- Add the *DungeonGeneratorPipeline* component to a GameObject in a scene, setup the pipeline in the editor and then click the *Generate* button.
- Setup the *DungeonGeneratorPipeline* component as in the previous way and then call the `Generate` method on the component. 
- Create an instance of the `PipelineRunner` class and then call the `Run` method that accepts an array of pipeline items and a payload object.