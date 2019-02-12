---
id: motivation
title: Motivation
---

Dungeon generators usually let you configure how many rooms do you want to generate, their sizes, how long corridors do you want, etc. If you want to control the structure of generated dungeons, they sometimes let you specify how sparse or dense should the dungeons be or how many corridors should be connected to each room. And based on that configuration, they generate a random dungeon layout. 

For me, the biggest advantage of such generators is that the setup is usually very simple and you can start generating your dungeons literally in a matter of minutes. Another advantage is that these algorithms can often quickly generate layouts consisting of tens or even hundreds of rooms because there are not that many constraints on the structure of the dungeon so the process of finding a suitable dungeon is quite simple.

While these generators may be suitable for a majority of games TODO

The main goal of this dungeon generator is to give game designers a complete control over the structure of generated layouts. This is achieved by specifying so-called level (connectivity) graphs which describe how many rooms you want to generate and how they should be connected to one another.