---
title: Performance tips
---

When used correctly, Edgar can generate very complex levels. Unfortunately, it is also relatively simple to prepare an input that is just too difficult for the generator, and you will end up with a `TimeoutException`. The goal of this page is to provide some performance tips that you can follow to improve the performance of the generator.

The general idea is that if you make it harder for the generator in one way (e.g. by having many rooms), you should compensate for that in some other way (e.g. by not having cycles in your level graph). Also, I recommend starting simple and only making things more complex when you get the hang of how the generator behaves.

## Room templates

**Try to provide as many door positions as possible.** I cannot stress enough how important this is. You should aim to use the *Simple* or *Hybrid* door modes as much as possible, and only use the *Manual* door mode when it is absolutely necessary. The only exception is when you are trying to generate levels without cycles, then you can get away with having a relatively small number of door positions. 

**Make sure that default room templates make sense.** The easiest way to configure room templates is to add them as *Default room templates*, making them available to all rooms in the level graph. However, it is not recommended adding room templates that can be used only in very specific scenarios. For example, if you have a secret room that has exactly one door position, you should not add it to the default list. The reason is that the generator might try to use this room template for a room that has multiple neighbours, wasting precious time. Instead, assign these unique room templates only to the rooms where it makes sense.

## Level graphs

**Limit the number of rooms.** The number of rooms in a level graph greatly affects the performance. As a rule of thumb, you should aim to have **less than 20 rooms**. However, if you follow the other performance tips, you can generate levels with up to 40 rooms.

**Limit the number of cycles.** It is very hard to generate levels with cycles. Therefore, the number of cycles greatly affects the performance. Usually, you should start with 0-1 cycles and only increase the number when you are already familiar with the core concepts of Edgar. In the [Enter the Gungeon](../examples/enter-the-gungeon.md#level-graphs) example, you can see levels graphs with up to 3 cycles and the generator is still relatively fast.

**Avoid interconnected cycles.** Cycles are hard, but interconnected cycles are even harder. If you want to have multiple cycles in a level graph, make sure that the cycles do not have any rooms in common. Usually, it should not be too hard to design your level graphs without interconnected cycles. For example, all the level graphs in *Enter the Gungeon* have this property, and it does not make the game any worse.

