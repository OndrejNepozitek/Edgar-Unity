---
title: (PRO) Fog of war
---

In this guide, we will learn how to enable a simple fog of war functionality.

<Image src="2d/guides/fog_of_war/example.gif" caption="Fog of War example (exported to a GIF with low FPS, everything is smooth ingame)" /> 

## Limitations

- **Performance on large levels** - Not suitable for very large levels (e.g. area larger than 500x500 tiles) - see [Implementation and performance](guides/fog-of-war.md#implementation-and-performance)
- **Cell size of the grid** - The Fog of War currently works only when the *Cell size* of the Grid is set to *1* and the *Cell gap* set to *0*. If you want to use this feature and have a different cell size or cell gap, please let me know on [Github](https://github.com/OndrejNepozitek/Edgar-Unity/issues) and I will try to improve the implementation.

## Prerequisites

- **Built-in Render Pipeline** - works in Unity 2018.4+
- **Lightweight Render Pipeline (LWRP)** - version >= 6.5 of LWRP is needed (and therefore Unity 2019.2+)
- **Universal Render Pipeline (URP)** - version >= 7.0 of URP is needed (and therefore Unity 2019.3+)
    - **IMPORTANT!** - this feature **does not** work with the **2D Renderer (Experimental)** as it is still experimental and people in Unity have not yet implemented custom renderer features!
- **High Definition Render Pipeline (HDRP)** - currently not supported

> **Note:** Additional setup steps are needed for LWRP and URP, see below.

## Example scene

An example scene can be found at <Path path="2de:FogOfWarExample" />. 

> **Note:** Please see the [Add custom renderer feature](fog-of-war.md#add-custom-renderer-feature) setup step below to make the example scene work in URP/LWRP.

## Setup

There are several steps that need to be done to enable the *Fog of War* feature. I tried to make the setup as simple as possible, but it still requires some manual work.

### Add Fog of War component

The first step is to add the **Fog of War** component to the game object that holds the main camera of your game. The component has several settings, see the [Configuration](fog-of-war.md#configuration-and-examples) section.

### Include shader in build

This feature uses a custom shader that you have to manually include in the build of you game. Navigate to <Path path="Edit/Project Settings/Graphics" /> and add the *Edgar/FogOfWar* shader to the *Always Included Shaders* list. If you do not do that, the game will work in the editor, but it won't work in a standalone build.

### Add custom renderer feature

**(URP and LWRP only)** - This step is only required if you use URP or LWRP as they do not support the [OnRenderImage()](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnRenderImage.html) hook. To work around that, you have to manually add a custom renderer feature.

**LWRP**: The first step is to create a custom **Forward Renderer** asset if you do not have one already. This can be done in <Path path="Create/Rendering/LWRP/Forward Renderer" />. You have to make sure that this renderer is used in the render pipeline instead of the default one.

**URP**: With URP, there should already exist a **Forward Renderer** asset that was created with the render pipeline asset. Locate the renderer.

The next step is to open the **Forward Renderer** asset. Click the *Add Renderer Feature* and choose the *Fog Of War Feature*. Based on the render pipeline that you are using, the feature should be called *Fog Of War LWRP Feature* or *Fog Of War URP Feature*.

> **Note:** If you do not see the *Fog Of War Feature*, make sure you meet the requirements as described [above](fog-of-war.md#prerequisites). The feature is conditionally compiled only if there are all the required packages installed.

### Add custom post-processing logic

After a level is generated, we have to set up the Fog of War component. The best place to do that is in a [custom post-processing task](generators/post-process.md#custom-post-processing). Sample code with comments can be seen below:

<ExternalCode name="2d_fogOfWar_postProcessing" />

> **Note:** More information about the API of the *FogOfWar* component can be found in the [API](fog-of-war.md#api) section.

After enabling this post-processing task and starting the game, we should see that all but the spawn room are hidden in the fog. The last thing that we have to handle is to reveal rooms when we enter them.

### Add trigger colliders

The last step is to add trigger colliders that will reveal a room when the player enters that room. There are at least two possible ways of doing that:

1. Add colliders to all room templates and always reveal only the room for which the collider was triggered
2. Add colliders only to corridor room templates and when you trigger the collider reveal both the collider and its neighbours

In this tutorial, I decided to use the second option as it, in my opinion, looks better. This is the final result that we want to achieve:

<Image src="2d/guides/fog_of_war/colliders_goal.gif" caption="Rooms are revealed after a corridor is visited" /> 

To achieve this, we have to slightly modify our corridor room templates. In each corridor room template, we create a child game object with a collider that has *Is Trigger* set to true. The collider should mark the area where the player has to stand to trigger the fog of war script. The easiest thing to do is to add a collider that covers the whole floor of the corridor. However, it is also possible to make the collider slightly smaller and trigger the script only when the player is closer to the centre of the corridor. Our example setup can be seen below:

<Image src="2d/guides/fog_of_war/colliders_setup.png" caption="Setup of the corridor Fog of War trigger" /> 

The last step is to add a simple component (to the game object with the collider) that will call the Fog of War script when the collider is triggered:

<ExternalCode name="2d_fogOfWar_triggerHandler" />

After you modify all your corridor room templates, the Fog of War should work as expected.

## API

Up-to-date API documentation can be found [here][FogOfWar].

## Configuration and examples

### Fog colour

The *FogColor* property specifies the colour of tiles that are hidden in the fog. It defaults to black but in our example scene we use purple as that is the colour which works best with the tileset.

<Gallery cols={2}>
    <Image src="2d/guides/fog_of_war/fog_color_purple.png" caption="FogColor set to purple" />
    <Image src="2d/guides/fog_of_war/fog_color_black.png" caption="FogColor set to black" />
</Gallery>

### Transition mode

The *Transition mode* property specifies what to do when there are two neighbouring tiles with different fog values (i.e. one tile is less revealed than the other tile). If we use the *Smooth* mode, the colours of pixels on the two tiles will smoothly interpolate from one fog value to the other. On the other hand, if we use the *Tile Based* mode, no interpolation will be used and there will be a strict division between the fog values of the two tiles. There is also a third mode now - the *Custom* mode that is somewhere between the two previous modes and adds two additional parameters - *Tile granularity* and *Fog smoothness*.

<Gallery cols={2}>
    <Image src="2d/guides/fog_of_war/transition_mode_smooth.gif" caption="TransitionMode set to Smooth (gif)" />
    <Image src="2d/guides/fog_of_war/transition_mode_tile_based.gif" caption="TransitionMode set to Tile Based (gif)" />
</Gallery>

#### Fog value

Before we dive into the next two parameters, it is good to know what is the *fog (transparency) value* of a pixel. When we want to show that a pixel is affected by the fog of war effect, we interpolate between the colour of that pixel and the fog colour. The value that controls the interpolation is called the *fog value*. This *fog value* is always in the *[0,1]* range. To completely hide a pixel in the fog, we set its *fog value* to 0. To completely show a pixel, we set its *fog value* to 1. If we want to animate the transition between pixels being hidden and then revealed, we can gradually increase the *fog value*, starting with the initial *fog value* and going all the way up to 1.

#### `TileGranularity`

This parameter controls into how many pixel chunks is each tile divided. The actual relation between the value of this parameter and the number of chunks is that if the value is equal to *X*, each tile is divided into *X\*X* same-sized chunks. That means that if we set the value to 2, we will get *2x2=4* chunks in total - each tile is divided into quarters.

The main property of individual chunks is that each pixel in a chunk has the same *fog value* as all the other pixels in the chunk. Both the *Tile-based* and *Smooth* transition modes are special cases of this division to chunks. The *Tile-based* mode equals to setting *TileGranularity* to 1 - each tile is a single chunk. The *Smooth* mode equals to setting *TileGranularity* to infinity - each pixel is independent of other pixels. 

<Gallery cols={2}>
    <Image src="2d/guides/fog_of_war/granularity_1.gif" caption="TileGranularity set to 1 (gif)" />
    <Image src="2d/guides/fog_of_war/granularity_2.gif" caption="TileGranularity set to 2 (gif)" />
</Gallery>

#### `FogSmoothness`

This parameter controls how many possible *fog values* there are for every pixel. In other words, the parameter controls how many possible steps there are between the *fog values* 0 and 1. The exact formula is *stepSize = 1 / fogSmoothness*. For example, by setting the *FogSmoothness* to *2*, the step size is equal to *1/4 = 0.25* and there are 5 possible fog values - *0*, *0.25*, *0.5*, *0.75* and *1*.

If you want to emphasize the transitions between individual *fog values* set the parameter to a lower value, for example *10*. If you want to have a very smooth transition between individual *fog values*, use a high value, for example *100*.

<Gallery cols={2}>
    <Image src="2d/guides/fog_of_war/smoothness_2.gif" caption="FogSmoothness set to 2 (gif)" />
    <Image src="2d/guides/fog_of_war/smoothness_10.gif" caption="FogSmoothness set to 10 (gif)" />
</Gallery>

### Wave mode

The first available mode is the **Wave mode**. With the *Wave mode*, the fog reveals based on the distance of individual tiles from the player. The tiles that are close to the player are revealed sooner than tiles that are far from the player. There are two properties that alter the behaviour when in the *Wave mode*: 

#### `WaveSpeed`

The **WaveSpeed** property specifies how fast the wave moves and its unit is *tiles per second*. If we have a tile that is 15 tiles away from the player and the wave speed is set to 5, then the tile will be fully revealed 15/5 = 3 seconds after triggering the fog of war script.

<Gallery cols={2}>
    <Image src="2d/guides/fog_of_war/wave_speed_10.gif" caption="WaveSpeed set to 10 tiles per second (gif)" />
    <Image src="2d/guides/fog_of_war/wave_speed_1.gif" caption="WaveSpeed set to 1 tile per second (gif)" />
</Gallery>

#### `WaveRevealThreshold`

The **WaveRevealThreshold** property specifies for how long is a tile completely hidden before it starts to be revealed. If we set the value to 0, all tiles to be revealed will immediately have non-zero opacity. The tiles that are close to the player will quickly have their full opacity while it will take longer for tiles that are far away. If we set the value to 0.5 and have a tile that should be fully revealed after 3 seconds, it will make the tile completely hidden for 0.5 * 3 = 1.5 seconds and only after that the opacity will be increased, and the tile will be fully revealed after additional 1.5 seconds. As a result, it will look like the wave actually moves.

<Gallery cols={2}>
    <Image src="2d/guides/fog_of_war/wave_reveal_threshold_0.gif" caption="WaveRevealThreshold set to 0 (gif)" />
    <Image src="2d/guides/fog_of_war/wave_reveal_threshold_0_5.gif" caption="WaveRevealThreshold set to 0.5 (gif)" />
</Gallery>

> **Note:** It is not recommended to use values higher than 0.5 in the combination with the Smooth transition mode.

### Fade In mode

The second available mode is the **Fade In mode**. With the *Fade In mode*, all the tiles are revealed at the same time no matter the distance from the player. There is a single property that alters the behaviour of this mode:

#### `FadeInDuration`

The **FadeInDuration** property specifies after how many seconds should the tiles be completely revealed. Value *0* means that all the tiles will be revealed immediately after triggering the fog of war script.

<Gallery cols={2}>
    <Image src="2d/guides/fog_of_war/fade_in_duration_0.gif" caption="FadeInDuration set to 0 seconds (gif)" />
    <Image src="2d/guides/fog_of_war/fade_in_duration_5.gif" caption="FadeInDuration set to 5 seconds (gif)" />
</Gallery>

### Reveal Corridors

The **RevealCorridors** checkbox specifies whether you want to reveal some tiles from neighbouring corridor rooms even though the rooms themselves are not yet fully revealed. The main purpose of this feature is aesthetics - in my opinion, the game looks better when the corridor tiles gradually fade away. There are two properties that alter the behaviour of this feature:

<Gallery cols={2}>
    <Image src="2d/guides/fog_of_war/reveal_corridors_enabled.png" caption="RevealCorridors enabled" />
    <Image src="2d/guides/fog_of_war/reveal_corridors_disabled.png" caption="RevealCorridors disabled" />
</Gallery>

#### `RevealCorridorsTiles`

The **RevealCorridorsTiles** property specifies how many tiles of each corridor should be revealed. To be more precise, it specifies the maximum distance of tiles (counted from the doors of the corridor) that should be revealed.

<Gallery cols={2}>
    <Image src="2d/guides/fog_of_war/reveal_corridors_tiles_1.png" caption="RevealCorridorsTiles set to 1" />
    <Image src="2d/guides/fog_of_war/reveal_corridors_tiles_3.png" caption="RevealCorridorsTiles set to 3" />
</Gallery>

#### `RevealCorridorsGradually`

The **RevealCorridorsGradually** property specifies whether corridor tiles should gradually fade out until they are barely visible.

<Gallery cols={2}>
    <Image src="2d/guides/fog_of_war/reveal_corridors_gradually_enabled.png" caption="RevealCorridorsGradually enabled" />
    <Image src="2d/guides/fog_of_war/reveal_corridors_gradually_disabled.png" caption="RevealCorridorsGradually disabled" />
</Gallery>

### Initial fog transparency

The **InitialFogTransparency** property controls the initial transparency of the fog. The valid range for this parameter is *[0,1]*. By default, this value is set to *0*, which means that tiles that are covered in fog are completely hidden. 

<Gallery cols={2}>
    <Image src="2d/guides/fog_of_war/transparency_0.png" caption="InitialFogTransparency set to 0" />
    <Image src="2d/guides/fog_of_war/transparency_0_15.png" caption="InitialFogTransparency set to 0.15" />
</Gallery>

## Implementation and performance

The Fog of War is implemented as an Image Effect (built-in render pipeline) or a Render Feature (URP). The script keeps track of which tiles should be revealed and passes this information to a shader via a texture where each pixel represents a single tile in the level. Based on the colour of individual pixels, the shader lerps between the actual tile graphics and the fog colour. All transitions are done on a CPU - when the fog changes, an updated texture is sent to the GPU.

The main performance bottleneck is when some revealed tiles do not fit in the fog texture. The script starts with a 1x1 texture and when the first room is revealed, it creates a new texture that is large enough to fit all the revealed tiles (plus a bit extra to make sure that we do not have to do this too often). This process repeats every time the texture is too small. The problem is that as the texture gets larger, more CPU time is needed to create the texture and upload it to the GPU. I did a simple benchmark, and it looks like the performance is good enough for levels with up to 250k tiles (i.e. the bounding box of the level is approx. 500x500 tiles). For larger levels, the game freezes for a while when the texture is recreated. However, the average level generated by the plugin should be much smaller than that, so the performance of the game should not be affected by the Fog of War script.

> **Note:** If you have problems with the performance of the Fog of War script, please create a Github issue.

If you want to have the minimum possible performance footprint, do not use any animated transitions. Use the *Fade In* mode and set the *Fade In Duration* to *0 seconds*. With this configuration, the script will compute something only in the frame when a room is revealed.

## Advanced

### Serialization

TBD.