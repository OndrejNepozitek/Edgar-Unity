[//]: # "Is it possible to have non-rectangular rooms?"

Short answer: Yet, it is.

You can have room templates of any shape as long as it conforms to the underlying grid. Throughout this documentation, you can find many non-rectangular rooms in various example scenes:

<Gallery cols={2} fixedHeight>
    <Image src="2d/examples/gungeon/room_templates/normal5.png" />
    <Image src="2d/examples/gungeon/room_templates/shop.png" />
    <Image src="2d/examples/platformer1/room2.png" />
    <Image src="2d/examples/dead_cells/underground/teleport2.png" caption="Teleport" />
</Gallery>


However, there are some benefits to having mostly box-shaped rooms:

- If a room template has many straight walls => that usually means there can be many available door positions on the outline of the room template => which in turn usually makes the performance of the generator better.
- The underlying algorithm works better with rectangular rooms than with other irregular shapes. For example, it is usually much easier to successfully lay out a square room rather than a big L-shaped room.

That being said, you should not have to worry too much if you use irregular room shapes in moderation. It is completely fine to have rounded corners or a few really irregular room shapes. You can always experiment and see for yourself whether it affects performance in your specific use case.