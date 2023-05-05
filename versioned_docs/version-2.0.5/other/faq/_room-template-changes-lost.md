[//]: # "Changes to a room template are lost after a level is generated"

It often happens that you want to change the default structure of a room template. Maybe you want to add a collider, add another tilemap layer, or change the properties of the grid-like the cell size. But when you hit the *Generate* button, the changes are not there and the level looks exactly like before.

The reason is that after a level is generated, all the room templates are merged into a single set of shared tilemaps. So, if you want to change something, you also have to instruct the generator that the changes should also be applied to the shared tilemaps. You can find a dedicated guide [here](../../guides/room-template-customization.md).