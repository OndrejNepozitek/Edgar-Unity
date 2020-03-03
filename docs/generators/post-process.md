---
title: Post processing
---

After a level is generated, we may often want to run some additional logic like spawning enemies, etc. This can be achieved by providing your own post processing logic that will be called after the level is generated and provided with information about the level. 

To better understand how the generator works, we will first describe which post processing is done by the generator itself and then provide ways to extend this behaviour and provide your own logic.

## Built-in post processing steps

### 0. Instantiate room template with correct positions

### 1. Initialize shared tilemaps

### 2. Copy rooms to shared tilemaps

### 3. Center grid

### 4. Disable room template renderers

### 5. Disable room template colliders 

## Custom post processing