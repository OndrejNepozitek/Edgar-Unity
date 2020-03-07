---
title: Post processing
---

import { Image, Gallery, GalleryImage } from "@theme/Gallery";

After a level is generated, we may often want to run some additional logic like spawning enemies, etc. This can be achieved by providing your own post processing logic that will be called after the level is generated and provided with information about the level. 

To better understand how the generator works, we will first describe which post processing is done by the generator itself and then provide ways to extend this behaviour and provide your own logic.

## Built-in post processing steps

> **TODO:** This section is not completed

### 0. Instantiate room template with correct positions

### 1. Initialize shared tilemaps

### 2. Copy rooms to shared tilemaps

### 3. Center grid

### 4. Disable room template renderers

### 5. Disable room template colliders 

## Custom post processing

If we want to add our own post processing logic, we have to create a class that inherits from `DungeonGeneratorPostProcessBase`. And because the base class is a ScriptableObject, we need to add the `CreateAssetMenu` attribute so we are able to create an instance of that ScriptableObject. After a level is generated, the `Run` method is called and that is the place where we put our post process logic.


    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Docs/My custom post process", fileName = "MyCustomPostProcess")]
    public class MyCustomPostProcess : DungeonGeneratorPostProcessBase
    {
        public override void Run(GeneratedLevel level, LevelDescription levelDescription)
        { 
            // Implement the logic here
        }
    }

With the implementation ready, we now have to create an instance of that ScriptableObject by right clicking in the project view and *Create -> Dungeon generator -> Examples -> Docs -> My custom post process*. And the last step is to drag and drop this GameObject in the *Custom post process tasks* section of the dungeon generator.

<Image src="img/v2/examples/example1/custom_post_process.png" caption="Add the ScriptableObject to the Custom post process tasks array" />