---
title: Keep prefabs in editor
---

After a level is generated, each room template prefab that was used in the level goes through the `Object.Instantiate()` method. This method removes the connection to the original prefab, which basically means that the whole prefab is unpacked (described [here in Unity docs](https://docs.unity3d.com/ScriptReference/Object.Instantiate.html).) However, it might be sometimes useful to keep the references to the prefabs. For example, if the level generator is used in the Editor and then manual changes are made to that level.

## Solution

There is currently no simple solution that would require just ticking some checkbox in the Editor. However, it is possible to change the default behaviour by changing a line or two directly in the source code of the asset.

Locate the `GeneratorUtilsGrid2D` class and inside the class there is the `InstantiateRoomTemplate` (as shown in the source code below). In that method, there are two constants which you can change to keep prefab references when generating levels directly in the Editor.

<ExternalCode name="2d_keepPrefabsInEditor" />