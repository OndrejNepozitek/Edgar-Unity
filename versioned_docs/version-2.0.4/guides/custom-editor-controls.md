---
title: (PRO) Custom editor controls
---

In this guide, we will see how to override the default look of level graph editor rooms and connections in order to add custom icons.

> **Note:** This pages is not meant to be an in-depth guide. Please look at the source code of this example for more information.

<Image src="2d/guides/custom_editor_controls/result.png" caption="Result of this guide. Custom icons for rooms and connection." height="500" /> 

> **Note:** All files from this example can be found at <Path path="2de:CustomEditorControls" />.

## Create custom room type

Create a custom room type as documented [here](../basics/level-graphs.md#pro-custom-rooms-and-connections). I also created a `RoomType` enum which will control if a room has an icon next to it.

<ExternalCode name="2d_customEditorControls_customRoom" />

## Create custom room control

Create a class that will override the default look of rooms in the level graph editor. First, create a class that inherits from `RoomControl`. Then, add the `CustomRoomControl` attribute and specify for which room should the custom control be used.

After that, you can override the default implementation of the `RoomControl` class. In this example, I extended the default implementation with a logic that checks the type of the room and displays an icon if there is any assigned to that room type. You can find more details in the source code.

<ExternalCode name="2d_customEditorControls_roomControl" />

## Custom connections and connection editors

The process of creating custom connection editors is exactly the same as for rooms. Just replace the word `room` with `connection` in class names.