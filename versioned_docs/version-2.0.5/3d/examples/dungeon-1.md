---
title: Dungeon 1
---

The goal of this tutorial is to show you a more realistic setup of the 3D generator. You can also see a GIF of this setup in [this tweet](https://twitter.com/OndrejNepozitek/status/1507377003200233472).

<Gallery cols={2} fixedHeight>
    <Image src="3d/examples/dungeon1/result_2_4.png" caption="Example result" />
    <Image src="3d/examples/dungeon1/result_5_4.png" caption="Example result" />
</Gallery>

> **Note:** All files from this example can be found at <Path path="3de:Dungeon1" />.

## Simple example

### Models

For this tutorial, I am going to use the [Modular Dungeons Pack](https://quaternius.com/packs/modulardungeon.html) by [@Quaternius](https://twitter.com/quaternius). Be sure to check them out if you like the models.

<Image src="3d/examples/dungeon1/models.png" />

### Blocks

The first step that I always do is I create prefabs for the base building blocks - floors and walls. The main goal is to make sure that they interact well together and that they can be snapped to the grid easily.

<Image src="3d/examples/dungeon1/blocks.png" caption="Floor block, wall block, and how they interact together" />

### Generator settings

The next step is to create an instance of the `GeneratorSettings` scriptable object to configure the size of the grid that will be used for the level. The sizes of the floor tile model is `2x2` units, therefore, I am going to configure the grid to be `2x2` units wide.

<Image src="3d/examples/dungeon1/generator_settings.png" caption="Setup of the Generator settings object" />

### Room template

The next step is to create the first room template. I usually start with something simple to make sure that all the blocks work together.

<Image src="3d/examples/dungeon1/room_2_no_doors.png" caption="Basic room template" />

> **Note:** The yellow outline gizmo is shown below the room template because the floor blocks are positioned with `y < 0`, thus being part of another layer of tiles.

As always, make sure to leave enough holes for doors to be added in the next step.

### Door prefab

The next step is to create a prefab for our doors. I am going to use the Arch model that you can see below.

<Image src="3d/examples/dungeon1/arch.png" caption="The Arch model that will be used for doors" />

The Arch is 4 units longs which means that our door should be 2 tiles wide. Create a new door prefab <Path path="3d:Door" par />, assign the generator settings, set the width to 2 tiles. Next, create a child game object, call it *Connector* and place the arch inside it. Align the arch properly. And finally, assign the *Connector* object to the *Connectors* section of the door inspector. The result should look like this: (check the [Basics](./basics.md) example if this part is too fast for you)

<Image src="3d/examples/dungeon1/door_connector.png" caption="Door setup with the arch connector" />

It is important to align the connector properly. This is what works for this setup:

<Gallery cols={2} fixedHeight>
    <Image src="3d/examples/dungeon1/door_side.png" caption="Alignment side view" />
    <Image src="3d/examples/dungeon1/door_top.png" caption="Alignment top view" />
</Gallery>

Next, it is time to add a door blocker. I am going to set up the wall block to be used when a door opening was not chosen by the generator. Make sure to align the blocker inside the green door gizmo.

<Image src="3d/examples/dungeon1/door_blocker.png" caption="Complete door setup" />

Next, place the door prefab inside all the holes that we left in the room template. Make sure to adjust the `Repeat` parameter so that each door covers the whole hole inside the wall.

<Image src="3d/examples/dungeon1/room_2.png" caption="Room template with doors" />

### Corridor

The next step is to create a corridor. Now that we have the door prefab prepared, it is just a matter of building/designing the actual corridor and placing the doors.

<Image src="3d/examples/dungeon1/corridor_1.png" caption="Basic corridor room template" />

### Level graph

The last step in this basic setup is to create a simple level graph and use the two room templates that we created. As I mentioned previously, it is a good idea to start simple to make sure that the base setup works, any only add more complexity later.

<Image src="3d/examples/dungeon1/level_graph.png" caption="Very simple level graph" />

### Results

We are now ready to generate our first levels. The resulting levels are relatively simple, but there is already happening quite a lot under the hood. We can see that the door connectors are aligned properly, unused door positions are filled with door blockers, and the generator should be quite fast because the level is really simple.

<Image src="3d/examples/dungeon1/result.png" caption="Generated level" />

> **Note:** I know that the shadows and lightning do not make sense if it is an underground dungeon, but I think it is good enough for the purposes of this tutorial.

## Real-life example

For the next part of this tutorial, I want to make the levels more realistic by adding additional room template and improving the level graph structure. 

### Additional room templates

Below are some of the room templates that I created for this part of the tutorial. Note how I add door openings to all places where it makes sense, because I need the generator perform really well as I plan to increase the complexity of the level in the [Extra stuff](#extra-stuff) section.

<Gallery cols={2} fixedHeight>
    <Image src="3d/examples/dungeon1/bedroom.png" caption="Bedroom" />
    <Image src="3d/examples/dungeon1/prison.png" caption="Prison" />
    <Image src="3d/examples/dungeon1/hub.png" caption="Hub" />
    <Image src="3d/examples/dungeon1/treasure.png" caption="Treasure" />
</Gallery>

### Custom input setup

When working on any non-trivial level with Edgar, I recommend using [Custom rooms and connection](../../basics/level-graphs.md#pro-custom-rooms-and-connections) together with [Custom input setup](../../generators/custom-input.md). By using these 2 features, you get much more control of the whole generator setup. For example, when working with a larger number of room templates, it is a good idea to not assign room templates to rooms directly, but rather assign a type to each room and control room templates based on that type. By doing so, you can introduce more variation to individual room types without having to configure rooms individually.

The first step is creating classes for the custom rooms and connection. You can see the code below. Once you have these classes ready, you have to open your level graph and configure it so that it uses your custom implementation. If you already have some rooms in your level graph, it is a good idea to delete all of them as they were created with the default rooms and connections implementation. 

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';

<Tabs>
  <TabItem value="room" label="Custom room" default>

<ExternalCode name="3d_dungeon1_room" />
    
  </TabItem>
  <TabItem value="connection" label="Custom connection" default>

<ExternalCode name="3d_dungeon1_connection" />
    
  </TabItem>
</Tabs>

Once your custom rooms and connections are ready, you should be able to pick a room type when you configure a room in the level graph editor window.

<Image src="3d/examples/dungeon1/room_type.png" caption="Room type dropdown when configuring a room in the level graph editor" />

Next, we need the logic that decides which set of room templates is used for a given room type. I use a simple switch statement here, but you can use anything you want.

<ExternalCode name="3d_dungeon1_roomTemplates" />

The last step is to put everything together and implement a custom input setup logic by creating a class that inherits from `DungeonGeneratorInputBaseGrid3D`. The implementation below might look intimidating, but there is not really that much going on. The main idea is that you:

- go through all the rooms in the level graph,
- cast each room to our custom room implementation type,
- compute room templates for a given room based on its type,
- and add the room to the level description which is later given to the dungeon generator.

Something similar also happens with the corridor rooms and room templates.

<ExternalCode name="3d_dungeon1_inputSetup" />

When the implementation is ready, you have to create an instance of the input setup scriptable object. Then, inside the dungeon generator component, switch input type to *Custom Input* and assign the scriptable object there. The result should look like this:

<Image src="3d/examples/dungeon1/input_setup.png" caption="Custom input setup inside the dungeon generator component" />

### Level graph

Now, we can come up with a more realistic level structure. The main idea is as follows: you enter the dungeon, have to make your way around a trap, then you arrive at a large hub with multiple exits. Two of the exists lead to social rooms like bedroom, storage or kitchen. Another exit leads to a branch which contains two rooms like treasure or prison. The last exit leads to the entrance of the boss room and then to the boss room itself, where you are going to face the bandit boss.

<Image src="3d/examples/dungeon1/level_graph_2.png" caption="More realistic level graph" />

### Results

The generated levels are starting to look quite nice.

<Image src="3d/examples/dungeon1/result_2_2.png" caption="Generated level" />
<Image src="3d/examples/dungeon1/result_2_3.png" caption="Generated level" />

## Extra stuff

For this last section, I want to showcase some of the advanced stuff that you can do with the generator:

1. disable corridors between some rooms
2. add a Skyrim-like shortcut from the boss room back to the entrance

### Disable corridors between some rooms

The default behaviour is that you either have corridors between all rooms or not have corridors at all. But if you are using custom input setup, there is no one stopping you from having corridors between some rooms and direct connections between other rooms.

Any why would you want that? For example, the boss entrance room is already something like a corridor, so why have another corridor between it and the boss room? Another example might be the area of social rooms in the level graph. I decided that I would like them better if they were closer to one another.

The setup is relatively simple because most of it was already done in the custom connection and custom input setup. In the custom connection, you just have to make sure that there is a checkbox that lets use choose if a connection should use a corridor or not.

<ExternalCode name="3d_dungeon1_connection" />

And in the input setup, we need a condition that either adds a corridor or a direct connection.

<ExternalCode name="3d_dungeon1_inputSetupCorridors" />

#### Results

In the generated level below, you can see that there are no corridors between some neighbouring rooms.

<Image src="3d/examples/dungeon1/result_3_1.png" caption="Generated level" />

> **Note:** When generating levels without corridors, you must make sure that it is possible to connect the rooms without the use of corridors.

### Skyrim-like shortcut

The next thing I wanted to experiment with was adding a Skyrim-like shortcut that would lead from the boss room back to the entrance.

#### Modified Boss and Boss entrance room templates

The first step is to make sure that we can add additional exits to the Boss room while still making sure that the entrance uses the central door. To do this, I decided to create a special *Boss* door socket (orange colour in the screenshot below) so that the rooms connect properly. 

<Gallery cols={2} fixedHeight>
    <Image src="3d/examples/dungeon1/boss.png" caption="Boss room with special socket" />
    <Image src="3d/examples/dungeon1/bossentrance.png" caption="Boss entrance room with special socket" />
</Gallery>

#### Intermediate results

Below we can see some intermediate results using this new setup with the shortcut from the Boss room. *Spoiler alert*: this setup does not perform very well because it is quite hard for the generator to find a suitable layout for the loop/cycle that was formed by adding the shortcut path. 

<Gallery cols={2} fixedHeight>
    <Image src="3d/examples/dungeon1/level_graph_4.png" caption="Level graph with a new path from Boss to Entrance" />
    <Image src="3d/examples/dungeon1/result_4_1.png" caption="Generated level" />
</Gallery>

#### Better performance and cave room templates

The intermediate results have 2 problems:

- the generator sometimes timeouts
- I want the shortcut rooms to be in a different style than the ordinary rooms

The reason why the performance is not the best is that there is not much freedom in the loop that we are trying to lay out. The loop always starts with a small trap room, then a large hub room, then boss entrance, boss room and finally the two shortcut rooms. Aside from the shortcut rooms, all the other rooms have a fixed shape - there is only a single room template for each one. One approach to improve the performance would be to add more room templates with different shapes so that the algorithm has more options. But in this tutorial, I want to show you a different approach - optimizing the shortcut rooms in order to improve the performance.

First, I created a couple of darker cave-like corridors. They are just like normal corridor, but with a single difference - they use a different door socket so that they can be only connected to other cave-like rooms. Because the corridor use different walls, you also have to create a new door prefab for them.

<Gallery cols={2} fixedHeight>
    <Image src="3d/examples/dungeon1/dcorridor_1.png" caption="Cave corridor room template" />
    <Image src="3d/examples/dungeon1/dcorridor_2.png" caption="Cave corridor room template" />
</Gallery>

Next, I created cave-like room templates for normal rooms. These room template might look quite weird at first as they do not have any walls - they have doors all over the outline. There are two reasons for that:

1. higher number of possible door positions improves the performance of the generator
2. it fits the style of a more organic cave as it will be harder to distinguish room borders

This approach works because all the unused door positions will be filled by door blockers.

<Gallery cols={2} fixedHeight>
    <Image src="3d/examples/dungeon1/croom_1.png" caption="Cave room template" />
    <Image src="3d/examples/dungeon1/croom_2.png" caption="Cave room template" />
</Gallery>

The last room template that we need is a corridor that can connect a normal room to a cave room:

<Image src="3d/examples/dungeon1/corridortransition.png" caption="Transition corridor from a normal room to a cave room" />

#### Level graph

Next, the level graph has to be changed so that the new cave rooms are used. All the corridors can be put into a single pool of corridor room templates because the door sockets will make sure that the correct corridor is always used.

<Image src="3d/examples/dungeon1/level_graph_5.png" caption="Final level graph containing 2 cave rooms" />

#### Results

You can see some final results below. The performance is now quite good, the generator is usually able to produce a level in under a second.

<Image src="3d/examples/dungeon1/result_5_1.png" caption="Generated level" />
<Image src="3d/examples/dungeon1/result_5_2.png" caption="Generated level" />