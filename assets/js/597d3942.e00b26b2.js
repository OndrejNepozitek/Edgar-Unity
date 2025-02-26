"use strict";(self.webpackChunkmy_website=self.webpackChunkmy_website||[]).push([[3837],{603905:function(e,t,o){o.d(t,{Zo:function(){return d},kt:function(){return c}});var a=o(667294);function r(e,t,o){return t in e?Object.defineProperty(e,t,{value:o,enumerable:!0,configurable:!0,writable:!0}):e[t]=o,e}function n(e,t){var o=Object.keys(e);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);t&&(a=a.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),o.push.apply(o,a)}return o}function i(e){for(var t=1;t<arguments.length;t++){var o=null!=arguments[t]?arguments[t]:{};t%2?n(Object(o),!0).forEach((function(t){r(e,t,o[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(o)):n(Object(o)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(o,t))}))}return e}function l(e,t){if(null==e)return{};var o,a,r=function(e,t){if(null==e)return{};var o,a,r={},n=Object.keys(e);for(a=0;a<n.length;a++)o=n[a],t.indexOf(o)>=0||(r[o]=e[o]);return r}(e,t);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);for(a=0;a<n.length;a++)o=n[a],t.indexOf(o)>=0||Object.prototype.propertyIsEnumerable.call(e,o)&&(r[o]=e[o])}return r}var s=a.createContext({}),m=function(e){var t=a.useContext(s),o=t;return e&&(o="function"==typeof e?e(t):i(i({},t),e)),o},d=function(e){var t=m(e.components);return a.createElement(s.Provider,{value:t},e.children)},p={inlineCode:"code",wrapper:function(e){var t=e.children;return a.createElement(a.Fragment,{},t)}},h=a.forwardRef((function(e,t){var o=e.components,r=e.mdxType,n=e.originalType,s=e.parentName,d=l(e,["components","mdxType","originalType","parentName"]),h=m(o),c=r,u=h["".concat(s,".").concat(c)]||h[c]||p[c]||n;return o?a.createElement(u,i(i({ref:t},d),{},{components:o})):a.createElement(u,i({ref:t},d))}));function c(e,t){var o=arguments,r=t&&t.mdxType;if("string"==typeof e||r){var n=o.length,i=new Array(n);i[0]=h;var l={};for(var s in t)hasOwnProperty.call(t,s)&&(l[s]=t[s]);l.originalType=e,l.mdxType="string"==typeof e?e:r,i[1]=l;for(var m=2;m<n;m++)i[m]=o[m];return a.createElement.apply(null,i)}return a.createElement.apply(null,o)}h.displayName="MDXCreateElement"},726459:function(e,t,o){o.r(t),o.d(t,{frontMatter:function(){return l},contentTitle:function(){return s},metadata:function(){return m},toc:function(){return d},default:function(){return k}});var a=o(487462),r=o(263366),n=(o(667294),o(603905)),i=["components"],l={title:"Room templates"},s=void 0,m={unversionedId:"basics/room-templates",id:"version-2.0.2/basics/room-templates",title:"Room templates",description:"Room templates are one of the main concepts of the generator. They describe how individual rooms in the dungeon look and how they can be connected to one another.",source:"@site/versioned_docs/version-2.0.2/basics/room-templates.md",sourceDirName:"basics",slug:"/basics/room-templates",permalink:"/Edgar-Unity/docs/2.0.2/basics/room-templates",editUrl:"https://github.com/OndrejNepozitek/Edgar-Unity/tree/docusaurus/versioned_docs/version-2.0.2/basics/room-templates.md",tags:[],version:"2.0.2",frontMatter:{title:"Room templates"},sidebar:"docs",previous:{title:"Quickstart",permalink:"/Edgar-Unity/docs/2.0.2/basics/quickstart"},next:{title:"Level graphs",permalink:"/Edgar-Unity/docs/2.0.2/basics/level-graphs"}},d=[{value:"Creating room templates",id:"creating-room-templates",children:[{value:"Room template structure",id:"room-template-structure",children:[],level:3}],level:2},{value:"Designing room templates",id:"designing-room-templates",children:[{value:"Limitations",id:"limitations",children:[{value:"One connected component",id:"one-connected-component",children:[],level:4},{value:"Each tile at least two neighbours",id:"each-tile-at-least-two-neighbours",children:[],level:4},{value:"May contain holes",id:"may-contain-holes",children:[],level:4}],level:3},{value:"Outline override",id:"outline-override",children:[],level:3},{value:"Bounding box outline handler",id:"bounding-box-outline-handler",children:[],level:3}],level:2},{value:"Adding doors",id:"adding-doors",children:[{value:"How to interpret door gizmos",id:"how-to-interpret-door-gizmos",children:[],level:3},{value:"Door modes",id:"door-modes",children:[{value:"Simple mode",id:"simple-mode",children:[],level:4},{value:"Manual mode",id:"manual-mode",children:[],level:4},{value:"Hybrid mode",id:"hybrid-mode",children:[],level:4}],level:3},{value:"(PRO) Door sockets",id:"pro-door-sockets",children:[],level:3},{value:"(PRO) Door directions",id:"pro-door-directions",children:[],level:3}],level:2},{value:"Repeat mode",id:"repeat-mode",children:[],level:2},{value:"Corridors",id:"corridors",children:[],level:2},{value:"Final steps",id:"final-steps",children:[],level:2}],p=function(e){return function(t){return console.warn("Component "+e+" was not imported, exported, or provided by MDXProvider as global scope"),(0,n.kt)("div",t)}},h=p("Image"),c=p("Path"),u=p("Gallery"),g={toc:d};function k(e){var t=e.components,o=(0,r.Z)(e,i);return(0,n.kt)("wrapper",(0,a.Z)({},g,o,{components:t,mdxType:"MDXLayout"}),(0,n.kt)("p",null,"Room templates are one of the main concepts of the generator. They describe how individual rooms in the dungeon look and how they can be connected to one another. "),(0,n.kt)(h,{src:"2d/room_templates/room_template_complete.png",caption:"Example of a complete room template. Outline of the room template is highlighted with yellow and possible door positions are red.",mdxType:"Image"}),(0,n.kt)("h2",{id:"creating-room-templates"},"Creating room templates"),(0,n.kt)("p",null,"To create a new room template, you have to:"),(0,n.kt)("ul",null,(0,n.kt)("li",{parentName:"ul"},"navigate to the folder where the room template prefab should be saved"),(0,n.kt)("li",{parentName:"ul"},"right click in the ",(0,n.kt)("em",{parentName:"li"},"Project window")," and choose ",(0,n.kt)(c,{path:"2d:Dungeon room template",mdxType:"Path"})),(0,n.kt)("li",{parentName:"ul"},"(optional) rename the prefab file to anything you want")),(0,n.kt)("h3",{id:"room-template-structure"},"Room template structure"),(0,n.kt)("p",null,"Below you can see the room template structure after the room template is created:"),(0,n.kt)("ul",null,(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("strong",{parentName:"li"},"Tilemaps")," game object that contains several tilemaps attached as children"),(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("strong",{parentName:"li"},"Room Template")," script attached to the root game object"),(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("strong",{parentName:"li"},"Doors")," script attached to the root game object")),(0,n.kt)(h,{src:"2d/room_templates/room_template_inspector.png",caption:"Room template structure",obsolete:!0,mdxType:"Image"}),(0,n.kt)("h2",{id:"designing-room-templates"},"Designing room templates"),(0,n.kt)("p",null,"We will use Unity ",(0,n.kt)("a",{parentName:"p",href:"https://docs.unity3d.com/Manual/class-Tilemap.html"},"Tilemaps")," to design our room templates, so you should be familiar with that. By default, room templates come with several tilemap layers that are children of the ",(0,n.kt)("em",{parentName:"p"},"Tilemap")," game object:"),(0,n.kt)("ul",null,(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("strong",{parentName:"li"},"Floor")," - order 0 "),(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("strong",{parentName:"li"},"Walls")," - order 1, with collider"),(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("strong",{parentName:"li"},"Collideable")," - order 2, with collider"),(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("strong",{parentName:"li"},"Other 1")," - order 3"),(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("strong",{parentName:"li"},"Other 2")," - order 4"),(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("strong",{parentName:"li"},"Other 3")," - order 5")),(0,n.kt)("p",null,"It is ",(0,n.kt)("strong",{parentName:"p"},"VERY IMPORTANT")," that all the room templates have exactly the same structure of tilemaps because the generator will copy all the tiles from individual room templates to shared tilemaps. If you need a different structure of tilemaps, you can override the default behaviour. See ",(0,n.kt)("a",{parentName:"p",href:"/Edgar-Unity/docs/2.0.2/guides/room-template-customization"},"Room template customization"),"."),(0,n.kt)(h,{src:"2d/room_templates/room_template_drawing.gif",caption:"You can use all the available tools (brushes, rule tiles, etc.) to draw room templates",obsolete:!0,mdxType:"Image"}),(0,n.kt)("h3",{id:"limitations"},"Limitations"),(0,n.kt)("p",null,"The underlying algorithm works with polygons, not tilemaps, tiles and sprites. We are interested in the outlines of individual room templates. However, there are some limitations as to how a room template may look like in order for the algorithm to be able to compute its outline. The goal of this section is to describe which rules we should follow when designing room templates."),(0,n.kt)(h,{src:"2d/room_templates/outline.png",caption:"The yellow color shows the outline of the room template as it is seen by the generator",mdxType:"Image"}),(0,n.kt)(h,{src:"2d/room_templates/invalid_outline.png",caption:"If the outline is invalid, we can see a warning in the *Room Template* script",obsolete:!0,mdxType:"Image"}),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," The underlying algorithm does not care about individual tilemaps layers. Instead, it merges all the layers together and then finds all the non-null tiles. Therefore, the outline of the room template will be the same no matter which tilemap layers we use.")),(0,n.kt)("h4",{id:"one-connected-component"},"One connected component"),(0,n.kt)("p",null,"I will not go into formal definitions. The image below should be self-explanatory."),(0,n.kt)(u,{cols:2,fixedHeight:!0,mdxType:"Gallery"},(0,n.kt)(h,{src:"2d/room_templates/one_connected_component_nok.png",caption:"Wrong",mdxType:"Image"}),(0,n.kt)(h,{src:"2d/room_templates/one_connected_component_ok.png",caption:"Correct",mdxType:"Image"})),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," You can see that the algorithm computed some outline (yellow) in the wrong room template. The current implementation stops after any outline is found and does not check whether all tiles are contained in that outline. This will be improved in the future.")),(0,n.kt)("h4",{id:"each-tile-at-least-two-neighbours"},"Each tile at least two neighbours"),(0,n.kt)("p",null,"Each tile must be connected to at least two neighbouring tiles. In the image below, both tiles in the upper row are connected to only a single neighbour, so the room shape is not valid. If we need these two tiles, we can use ",(0,n.kt)("strong",{parentName:"p"},"Outline override")," that is described in the next section."),(0,n.kt)(u,{cols:2,fixedHeight:!0,mdxType:"Gallery"},(0,n.kt)(h,{src:"2d/room_templates/at_least_two_neighbors_nok.png",caption:"Wrong",mdxType:"Image"}),(0,n.kt)(h,{src:"2d/room_templates/at_least_two_neighbors_ok.png",caption:"Correct",mdxType:"Image"})),(0,n.kt)("h4",{id:"may-contain-holes"},"May contain holes"),(0,n.kt)("p",null,"There may be holes inside the room template."),(0,n.kt)(u,{cols:2,fixedHeight:!0,mdxType:"Gallery"},(0,n.kt)(h,{src:"2d/room_templates/no_holes_ok_1.png",caption:"Correct",mdxType:"Image"}),(0,n.kt)(h,{src:"2d/room_templates/no_holes_ok_2.png",caption:"Correct",mdxType:"Image"})),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"NOTE:")," This was not possible in the 1.x.x version.")),(0,n.kt)("h3",{id:"outline-override"},"Outline override"),(0,n.kt)("p",null,"If we really need to have a room template whose outline is not valid, we can use the so-called ",(0,n.kt)("em",{parentName:"p"},"Outline override"),". It can be enabled by clicking the ",(0,n.kt)("em",{parentName:"p"},"Add outline override")," button in the ",(0,n.kt)("em",{parentName:"p"},"Room template")," script. As a result, a new tilemap layer called ",(0,n.kt)("em",{parentName:"p"},"Outline override")," is created. With this functionality enabled, the algorithm ignores all the other layers when computing the outline. Moreover, nothing that is drawn on this layer will be used in the resulting level, so we can use any tiles to draw on this layer. "),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," When we are done with drawing the outline, we can make the layer (game object) inactive so that we can see how the room template actually looks like. However, ",(0,n.kt)("strong",{parentName:"p"},"we must not destroy the game object"),".")),(0,n.kt)(u,{cols:2,fixedHeight:!0,mdxType:"Gallery"},(0,n.kt)(h,{src:"2d/room_templates/outline_override_active.png",caption:"We can use any tiles to draw the outline",mdxType:"Image"}),(0,n.kt)(h,{src:"2d/room_templates/outline_override_inactive.png",caption:"If we disable the Outline override game object, we should still see that the outline is overridden",mdxType:"Image"})),(0,n.kt)("h3",{id:"bounding-box-outline-handler"},"Bounding box outline handler"),(0,n.kt)("p",null,"In some situations, it would be useful to have an outline which looks like the bounding box of all the tiles in the room template. For example, it can be used to handle an outline of some platformer levels (see the images below). It is also possible to add padding to the top of the outline, which is convenient if we need to add doors that are higher than the outline."),(0,n.kt)("p",null,"To add the ",(0,n.kt)("em",{parentName:"p"},"Bounding box outline handler")," click the ",(0,n.kt)("strong",{parentName:"p"},"Add bounding box outline handler")," button in the ",(0,n.kt)("em",{parentName:"p"},"Room template")," inspector."),(0,n.kt)(u,{cols:2,fixedHeight:!0,mdxType:"Gallery"},(0,n.kt)(h,{src:"2d/room_templates/bounding_box_invalid.png",caption:"Invalid outline",mdxType:"Image"}),(0,n.kt)(h,{src:"2d/room_templates/bounding_box_override.png",caption:"Corrected manually with Outline override",mdxType:"Image"}),(0,n.kt)(h,{src:"2d/room_templates/bounding_box.png",caption:"Corrected automatically with Bounding box outline handler",mdxType:"Image"}),(0,n.kt)(h,{src:"2d/room_templates/bounding_box_padding_top.png",caption:"Example of Padding top 3",mdxType:"Image"})),(0,n.kt)(h,{src:"2d/room_templates/bounding_box.gif",caption:"Example bounding box outline usage",mdxType:"Image"}),(0,n.kt)("h2",{id:"adding-doors"},"Adding doors"),(0,n.kt)("p",null,"When we are happy with how the room template looks, we can add doors. By specifying door positions, we tell the algorithm how can individual room templates be connected."),(0,n.kt)("p",null,"The algorithm may connect two room templates if:"),(0,n.kt)("ul",null,(0,n.kt)("li",{parentName:"ul"},"there exist door positions with the same length"),(0,n.kt)("li",{parentName:"ul"},"the two room templates do not overlap after we connect them",(0,n.kt)("ul",{parentName:"li"},(0,n.kt)("li",{parentName:"ul"},"but they may share tiles on the outlines")))),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," In some level generators, if you define ",(0,n.kt)("em",{parentName:"p"},"N")," doors, it means that the room must be connected to ",(0,n.kt)("em",{parentName:"p"},"N")," neighbours. That is not the case here! By adding door positions, ",(0,n.kt)("strong",{parentName:"p"},"you specify where a door can be"),". But if there is a room template with 20 possible door positions, the generator might still use this room template for a room that has only a single neighbour. Moreover, a high number of available door position usually means better performance.")),(0,n.kt)("h3",{id:"how-to-interpret-door-gizmos"},"How to interpret door gizmos"),(0,n.kt)("p",null,"Before we start adding doors to our room templates, I think it is important to understand how to read the editor gizmos that represent doors. In the image below (left), we can see an example room template with red rectangles showing the available door positions. The ",(0,n.kt)("strong",{parentName:"p"},"dashed red rectangles")," represent individual ",(0,n.kt)("strong",{parentName:"p"},"door lines")," where a door line is set of all the possible doors inside rectangle. The ",(0,n.kt)("strong",{parentName:"p"},"solid red rectangles")," show the ",(0,n.kt)("strong",{parentName:"p"},"length of the doors"),". In the room template below, all doors are 2 tiles wide. The solid rectangle also contains information about how many door positions there are in the door line."),(0,n.kt)("p",null,"The GIF on the right shows an animation of all the possible doors positions from the room template on the left. An important thing to understand is that the door positions can overlap, and it is even good for the performance of the generator. The reason for that is that there are more possible door positions to choose from, so the generator finds a valid layout faster. "),(0,n.kt)(u,{cols:2,mdxType:"Gallery"},(0,n.kt)(h,{src:"2d/room_templates/doors/doors_visuals.png",caption:"Example room template",mdxType:"Image"}),(0,n.kt)(h,{src:"2d/room_templates/doors/doors_animation.gif",caption:"Animation of all the possible door positions",mdxType:"Image"})),(0,n.kt)("h3",{id:"door-modes"},"Door modes"),(0,n.kt)("p",null,"To manipulate with the doors, there must be a ",(0,n.kt)("inlineCode",{parentName:"p"},"Doors")," component attached to the root of the room template prefab. "),(0,n.kt)("p",null,"There are currently three different ways of defining door positions. A universal rule of all the different modes is that all door positions must be on the outline of the corresponding room template."),(0,n.kt)("h4",{id:"simple-mode"},"Simple mode"),(0,n.kt)("p",null,"In the ",(0,n.kt)("em",{parentName:"p"},"simple mode"),", you specify how wide should all the doors be and the margin, i.e. how far from corners of the room template must the doors be. This door mode is great for when you do not really care where exactly the doors can be. This door mode also usually has the best performance because there are many door positions to choose from."),(0,n.kt)("p",null,"Below you can see how this door mode looks in the editor."),(0,n.kt)(h,{src:"2d/room_templates/doors/simple1.png",caption:"Simple door mode - length 1, margin 2",mdxType:"Image"}),(0,n.kt)("p",null,"In side-scroller games, there are often different requirements for horizontal and vertical doors. For example, the player might be 3 tiles high but only 1 tile wide, so we would need wider vertical doors. To achieve this, we can change the ",(0,n.kt)("em",{parentName:"p"},"Mode")," dropdown to ",(0,n.kt)("inlineCode",{parentName:"p"},"Different Horizontal And Vertical"),". With this setting enabled, we can now choose different properties for vertical and horizontal doors. Or we might also disable one type of doors."),(0,n.kt)(h,{src:"2d/room_templates/doors/simple2.png",caption:"Simple door mode - different vertical and horizontal doors",mdxType:"Image"}),(0,n.kt)("h4",{id:"manual-mode"},"Manual mode"),(0,n.kt)("p",null,"In the ",(0,n.kt)("em",{parentName:"p"},"manual mode"),", you have to manually specify all the door positions of the room template. This door mode is great for when you have only a couple of doors at very specific positions."),(0,n.kt)("p",null,"To start adding doors, click the ",(0,n.kt)("em",{parentName:"p"},"Manual mode")," button in the ",(0,n.kt)("em",{parentName:"p"},"Doors")," script and then click the ",(0,n.kt)("em",{parentName:"p"},"Add door positions")," button to toggle edit mode. Then you can simply draw door positions by clicking on the first tile of the door and dragging the mouse to the last tile of the door."),(0,n.kt)(h,{src:"2d/room_templates/doors/manual.gif",caption:"Manual mode setup",mdxType:"Image"}),(0,n.kt)("p",null,"In the example above, we can see that we can have doors with different lengths - vertical doors are 3 tiles high and horizontal doors are 1 tile wide."),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," If you accidentally add a door position that you did not want to add, there are two ways of removing doors:"),(0,n.kt)("ol",{parentName:"blockquote"},(0,n.kt)("li",{parentName:"ol"},"Click the ",(0,n.kt)("em",{parentName:"li"},"Delete all door positions")," button to delete all the door positions."),(0,n.kt)("li",{parentName:"ol"},"Click the ",(0,n.kt)("em",{parentName:"li"},"Delete door positions")," button and then click on door positions that should be deleted."))),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," With multiple doors overlapping, the GUI gets quite messy. You should usually use the ",(0,n.kt)("em",{parentName:"p"},"hybrid mode")," when you have overlapping doors.")),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," The inspector script currently lets you add door positions that are not on the outline of the room shape. It will, however, result in an error when trying to generate a dungeon. It should be improved in the future.")),(0,n.kt)("h4",{id:"hybrid-mode"},"Hybrid mode"),(0,n.kt)("p",null,"The ",(0,n.kt)("em",{parentName:"p"},"hybrid mode")," is somewhere between the ",(0,n.kt)("em",{parentName:"p"},"simple")," and ",(0,n.kt)("em",{parentName:"p"},"manual")," modes. Instead of drawing individual door positions (like in the manual mode), we can draw whole door lines (multiple doors at once). "),(0,n.kt)("p",null,"To start adding doors, click the ",(0,n.kt)("em",{parentName:"p"},"Manual mode")," button in the ",(0,n.kt)("em",{parentName:"p"},"Doors")," script and then click the ",(0,n.kt)("em",{parentName:"p"},"Add door positions")," button to toggle edit mode. ",(0,n.kt)("strong",{parentName:"p"},"Then you have to configure the length of the doors in the field below.")," This is the main difference when compared to the ",(0,n.kt)("em",{parentName:"p"},"manual mode"),". In the manual mode, the length of doors is determined by the movement of the mouse. But in the hybrid mode, the length of doors is configured in the editor, and the movement of the mouse specifies how many doors there are next to each other."),(0,n.kt)(h,{src:"2d/room_templates/doors/hybrid.gif",caption:"Hybrid mode setup",mdxType:"Image"}),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," The ",(0,n.kt)("em",{parentName:"p"},"hybrid mode")," is great for when you cannot use the simple mode and the manual mode would require too much time to set up. Also, the hybrid mode also nicely handles ",(0,n.kt)("strong",{parentName:"p"},"overlapping doors")," because the definition of door lines implicitly contains them. Moreover, the hybrid mode also leads to ",(0,n.kt)("strong",{parentName:"p"},"better performance")," (when compared to the manual mode) because it promotes having many doors and the doors are in a format that the generator can easily work with.")),(0,n.kt)("h3",{id:"pro-door-sockets"},"(PRO) Door sockets"),(0,n.kt)("p",null,"By default, when the generator computes how can two room templates be connected, it looks for doors with the same length. If you want to have more control over this process, you can use ",(0,n.kt)("a",{parentName:"p",href:"/Edgar-Unity/docs/2.0.2/guides/door-sockets"},"Door sockets"),"."),(0,n.kt)("h3",{id:"pro-door-directions"},"(PRO) Door directions"),(0,n.kt)("p",null,"By default, all doors are undirected, meaning that they can be used both as an entrance or as an exit. With manual door mode, it is possible to configure doors as entrance-only or exit-only. When combined with directed level graphs, it gives you more control over generated levels. See the ",(0,n.kt)("a",{parentName:"p",href:"/Edgar-Unity/docs/2.0.2/guides/directed-level-graphs"},"Directed level graphs")," guide for more information and examples."),(0,n.kt)("h2",{id:"repeat-mode"},"Repeat mode"),(0,n.kt)("p",null,"Each ",(0,n.kt)("em",{parentName:"p"},"Room template")," script has a field called ",(0,n.kt)("em",{parentName:"p"},"Repeat Mode")," that is initially set to ",(0,n.kt)("em",{parentName:"p"},"Allow Repeat"),". Using this field, we can tell the algorithm whether the room template can be used more than once in generated levels. There are the following possibilities:"),(0,n.kt)("ul",null,(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("strong",{parentName:"li"},"Allow repeat")," - The room template may repeat in generated levels."),(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("strong",{parentName:"li"},"No immediate")," - Neighbors of the room template must be different."),(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("strong",{parentName:"li"},"No repeat")," - The room template can be used at most once.")),(0,n.kt)(h,{src:"2d/room_templates/repeat_mode.png",caption:"Specific positions mode",mdxType:"Image"}),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," Instead of setting the ",(0,n.kt)("em",{parentName:"p"},"Repeat mode")," on a per room template basis, you can use global override which is configured directly in the dungeon generator.")),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," If you provide too few room templates, they may repeat in generated levels even if you choose the ",(0,n.kt)("strong",{parentName:"p"},"No immediate")," or ",(0,n.kt)("strong",{parentName:"p"},"No repeat")," options. To make sure that the repeat mode is satisfied, please provide enough room templates to choose from.")),(0,n.kt)("h2",{id:"corridors"},"Corridors"),(0,n.kt)("p",null,"The algorithm distinguishes two types of room templates - basic room templates and room templates for corridor rooms. In theory, we can use any room template with at least two doors to act as a corridor room template. ",(0,n.kt)("strong",{parentName:"p"},"However, to make the algorithm fast, we should follow these recommendations"),":"),(0,n.kt)("ol",null,(0,n.kt)("li",{parentName:"ol"},"There should be exactly two door positions."),(0,n.kt)("li",{parentName:"ol"},"The two doors should be on the opposite sides of the room template."),(0,n.kt)("li",{parentName:"ol"},"The corridor should not be too long or too wide.")),(0,n.kt)(u,{cols:2,fixedHeight:!0,mdxType:"Gallery"},(0,n.kt)(h,{src:"2d/room_templates/corridor_ok1.png",caption:"Recommended - narrow straight corridor",mdxType:"Image"}),(0,n.kt)(h,{src:"2d/room_templates/corridor_ok2.png",caption:"OK - little too wide but should be ok",mdxType:"Image"}),(0,n.kt)(h,{src:"2d/room_templates/corridor_nok1.png",caption:"Not recommended - doors not on opposite sides",mdxType:"Image"}),(0,n.kt)(h,{src:"2d/room_templates/corridor_nok2.png",caption:"Not recommended - more than 2 door positions",mdxType:"Image"})),(0,n.kt)("h2",{id:"final-steps"},"Final steps"),(0,n.kt)("p",null,"After creating a room template game object, you can simply save it as a prefab, and it is ready to be used in a level graph."))}k.isMDXComponent=!0}}]);