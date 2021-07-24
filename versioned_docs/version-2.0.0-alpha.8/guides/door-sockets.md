---
title: (PRO) Door sockets
---

import { Image, Gallery, GalleryImage } from "@theme/Gallery";

In this guide, we will learn about door sockets and how to use them.

<Image src="img/v2/guides/door_sockets/result2.png" caption="Door sockets used to connect normal rooms and skull rooms with special corridors." />

## Introduction

**Door sockets are a mechanism to instruct the generator that only some doors are compatible.** By default, when the generator computes how can two room templates be connected, it looks for doors with the same length. However, that might not always fit our needs. For example, imagine that we have two or more biomes in our game, and we need a special transition corridor to connect these biomes correctly. With the default behaviour, that is not possible because the generator does not know that some doors are not compatible. That is where door sockets come into play. You can assign a door socket to each door, and the generator will connect only doors with the same socket.

> **Note:** If you use door sockets in a way that it restricts the generator too much, you can negatively impact the performance of the generator.

## Example setup

In this example setup, we will build on the [Example 1](../examples/example-1.md) and introduce a special type of room - the skull room. We will use door sockets to connect normal rooms and skull rooms properly.

> **Note:** All files from this example can be found at *Edgar/Examples/DoorSockets*.

### Create a door socket

The first step is to create a door socket. We want to have two different door types - normal doors and skull doors. There are two ways of how to approach this. First, we can create a door socket for each door type. Or second, we can utilize the fact that null sockets are not compatible with any other socket and create a door socket just for skull doors. I decided to use the second option because it requires less initial work because we do not have to go through all the already created doors and assign the default socket.

To create a door socket, head to *Create -> Edgar -> Door socket*, and that will create a new file in your current folder. If you open the file, you will see a colour field that controls the display colour of doors with this socket. I highly recommend using different colours for individual sockets to make it easy to distinguish between different sockets. I decided to use green colour in this example.

<Image src="img/v2/guides/door_sockets/socket.png" caption="Door socket for skull doors" />

### Use the socket

Next, I created a skull room. I used the simple door mode and assigned the skull socket. You can see the result below.

<Image src="img/v2/guides/door_sockets/skull_room.png" caption="Skull room" />

If we want to connect this room to normal rooms, we need a corridor with one skull door and one normal door. In fact, if we have both vertical and horizontal corridors and want both transition directions, we need 4 corridor room templates in total.

<Gallery cols={2}>
    <GalleryImage src="img/v2/guides/door_sockets/skull_hor_6x1_1.png" />
    <GalleryImage src="img/v2/guides/door_sockets/skull_hor_6x1_2.png" />
    <GalleryImage src="img/v2/guides/door_sockets/skull_ver_1x6_1.png" />
    <GalleryImage src="img/v2/guides/door_sockets/skull_ver_1x6_2.png" />
</Gallery>

You can also have non-corridor rooms with some doors being skull doors and some doors being normal doors. An example of such room template can be seen below.

<Image src="img/v2/guides/door_sockets/skull_room_2.png" caption="Skull room" />

> **Note:** For this room template, you can no longer use the simple door mode as it allows only a single socket. Instead, you have to use the manual door mode. Also, it is good to use the **Default socket for new doors** field instead of creating doors without a socket and that assigning the correct socket manually. First, let the field null and create all the default doors. Then, switch the socket to the Skull socket and add all the remaining doors.

### Results

<Image src="img/v2/guides/door_sockets/result1.png" />

## Advanced

The default socket implementation considers two doors to be compatible if the sockets are equal. If you need more control over this, you can implement your own socket class that inherits from the `DoorSocketBase` class and provide an implementation of the `IsCompatibleWith(IDoorSocket otherSocket)` method. An example of a behaviour that is not possible with the default implementation:

- Socket A is compatible with B
- Socket B is comptaible with C
- Socket A is not compatible with C