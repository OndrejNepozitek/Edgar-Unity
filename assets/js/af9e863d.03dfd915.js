"use strict";(self.webpackChunkmy_website=self.webpackChunkmy_website||[]).push([[4076],{603905:function(e,t,o){o.d(t,{Zo:function(){return m},kt:function(){return h}});var r=o(667294);function a(e,t,o){return t in e?Object.defineProperty(e,t,{value:o,enumerable:!0,configurable:!0,writable:!0}):e[t]=o,e}function l(e,t){var o=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),o.push.apply(o,r)}return o}function n(e){for(var t=1;t<arguments.length;t++){var o=null!=arguments[t]?arguments[t]:{};t%2?l(Object(o),!0).forEach((function(t){a(e,t,o[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(o)):l(Object(o)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(o,t))}))}return e}function i(e,t){if(null==e)return{};var o,r,a=function(e,t){if(null==e)return{};var o,r,a={},l=Object.keys(e);for(r=0;r<l.length;r++)o=l[r],t.indexOf(o)>=0||(a[o]=e[o]);return a}(e,t);if(Object.getOwnPropertySymbols){var l=Object.getOwnPropertySymbols(e);for(r=0;r<l.length;r++)o=l[r],t.indexOf(o)>=0||Object.prototype.propertyIsEnumerable.call(e,o)&&(a[o]=e[o])}return a}var s=r.createContext({}),p=function(e){var t=r.useContext(s),o=t;return e&&(o="function"==typeof e?e(t):n(n({},t),e)),o},m=function(e){var t=p(e.components);return r.createElement(s.Provider,{value:t},e.children)},c={inlineCode:"code",wrapper:function(e){var t=e.children;return r.createElement(r.Fragment,{},t)}},d=r.forwardRef((function(e,t){var o=e.components,a=e.mdxType,l=e.originalType,s=e.parentName,m=i(e,["components","mdxType","originalType","parentName"]),d=p(o),h=a,u=d["".concat(s,".").concat(h)]||d[h]||c[h]||l;return o?r.createElement(u,n(n({ref:t},m),{},{components:o})):r.createElement(u,n({ref:t},m))}));function h(e,t){var o=arguments,a=t&&t.mdxType;if("string"==typeof e||a){var l=o.length,n=new Array(l);n[0]=d;var i={};for(var s in t)hasOwnProperty.call(t,s)&&(i[s]=t[s]);i.originalType=e,i.mdxType="string"==typeof e?e:a,n[1]=i;for(var p=2;p<l;p++)n[p]=o[p];return r.createElement.apply(null,n)}return r.createElement.apply(null,o)}d.displayName="MDXCreateElement"},363746:function(e,t,o){o.r(t),o.d(t,{frontMatter:function(){return i},contentTitle:function(){return s},metadata:function(){return p},toc:function(){return m},default:function(){return w}});var r=o(487462),a=o(263366),l=(o(667294),o(603905)),n=["components"],i={title:"Example 1"},s=void 0,p={unversionedId:"examples/example-1",id:"examples/example-1",title:"Example 1",description:"In this tutorial, we will use this tileset by @pixelpoem. Be sure to check out their work if you like the tileset. We will not care about room decorations - we will use just basic walls, floor and door tiles.",source:"@site/docs/examples/example-1.md",sourceDirName:"examples",slug:"/examples/example-1",permalink:"/Edgar-Unity/docs/next/examples/example-1",editUrl:"https://github.com/OndrejNepozitek/Edgar-Unity/tree/docusaurus/docs/examples/example-1.md",tags:[],version:"current",frontMatter:{title:"Example 1"},sidebar:"docs",previous:{title:"(PRO) Custom input",permalink:"/Edgar-Unity/docs/next/generators/custom-input"},next:{title:"Example 2",permalink:"/Edgar-Unity/docs/next/examples/example-2"}},m=[{value:"Simple example",id:"simple-example",children:[{value:"Basic rooms templates",id:"basic-rooms-templates",children:[],level:3},{value:"Corridors",id:"corridors",children:[],level:3},{value:"Level graph",id:"level-graph",children:[],level:3},{value:"Results",id:"results",children:[],level:3}],level:2},{value:"Real-life example",id:"real-life-example",children:[{value:"Spawn room",id:"spawn-room",children:[],level:3},{value:"Boss room",id:"boss-room",children:[],level:3},{value:"Additional room template",id:"additional-room-template",children:[],level:3},{value:"Doors",id:"doors",children:[],level:3},{value:"Longer corridors",id:"longer-corridors",children:[],level:3},{value:"Enemies",id:"enemies",children:[],level:3},{value:"Level graph",id:"level-graph-1",children:[],level:3},{value:"Results",id:"results-1",children:[],level:3}],level:2}],c=function(e){return function(t){return console.warn("Component "+e+" was not imported, exported, or provided by MDXProvider as global scope"),(0,l.kt)("div",t)}},d=c("Gallery"),h=c("Image"),u=c("Path"),g=c("ExampleFeatures"),x=c("ExternalCode"),k={toc:m};function w(e){var t=e.components,o=(0,a.Z)(e,n);return(0,l.kt)("wrapper",(0,r.Z)({},k,o,{components:t,mdxType:"MDXLayout"}),(0,l.kt)("p",null,"In this tutorial, we will use ",(0,l.kt)("a",{parentName:"p",href:"https://pixel-poem.itch.io/dungeon-assetpuck"},"this tileset")," by ",(0,l.kt)("a",{parentName:"p",href:"https://twitter.com/pixel_poem"},"@pixel_poem"),". Be sure to check out their work if you like the tileset. We will not care about room decorations - we will use just basic walls, floor and door tiles. "),(0,l.kt)(d,{mdxType:"Gallery"},(0,l.kt)(h,{src:"2d/examples/example1/result1.png",caption:"Simple example",mdxType:"Image"}),(0,l.kt)(h,{src:"2d/examples/example1/result_reallife1.png",caption:"Real-life example",mdxType:"Image"})),(0,l.kt)("blockquote",null,(0,l.kt)("p",{parentName:"blockquote"},(0,l.kt)("strong",{parentName:"p"},"Note:")," All files from this example can be found at ",(0,l.kt)(u,{path:"2de:Example1",mdxType:"Path"}),".")),(0,l.kt)(g,{id:"example-1",mdxType:"ExampleFeatures"}),(0,l.kt)("h2",{id:"simple-example"},"Simple example"),(0,l.kt)("p",null,"The goal is to create two basic rectangular room templates of different sizes and a room template for both horizontal and vertical corridors. We will use the smaller room template for our dead-end rooms and the bigger room template for other rooms."),(0,l.kt)("h3",{id:"basic-rooms-templates"},"Basic rooms templates"),(0,l.kt)("p",null,"There should be nothing hard about the design of the two rectangular room templates. We use the ",(0,l.kt)("em",{parentName:"p"},"simple")," ",(0,l.kt)("em",{parentName:"p"},"door mode")," configured to door length 1 and corner distance 2. We need corner distance 2 in order to easily connect corridors."),(0,l.kt)(d,{mdxType:"Gallery"},(0,l.kt)(h,{src:"2d/examples/example1/room1.png",caption:"Bigger room",mdxType:"Image"}),(0,l.kt)(h,{src:"2d/examples/example1/room2.png",caption:"Smaller room",mdxType:"Image"})),(0,l.kt)("h3",{id:"corridors"},"Corridors"),(0,l.kt)("p",null,"Corridors are very simple with this tileset. We use the ",(0,l.kt)("em",{parentName:"p"},"specific positions")," doors mode to choose the two possible door positions. And because corridors are by default placed after non-corridor rooms, these room templates just work without the need of any scripting."),(0,l.kt)(d,{mdxType:"Gallery"},(0,l.kt)(h,{src:"2d/examples/example1/corridor_horizontal.png",caption:"Horizontal corridor",mdxType:"Image"}),(0,l.kt)(h,{src:"2d/examples/example1/corridor_vertical.png",caption:"Vertical corridor",mdxType:"Image"})),(0,l.kt)("p",null,"We just need to make sure that we do not allow door positions of non-corridor rooms that are closer than 2 tiles from corners. Below you can see what would happen otherwise. It is possible to allow that, but we would have to implement some post-processing logic that would fix such cases."),(0,l.kt)(h,{src:"2d/examples/example1/wrong_corridor.png",caption:"Incorrect corridor connection",mdxType:"Image"}),(0,l.kt)("h3",{id:"level-graph"},"Level graph"),(0,l.kt)("p",null,"With only two room templates for non-corridor rooms, we must think about which level graphs are possible to lay out and which are not. For example, using only the bigger room template, the algorithm is not able to lay out cycles of lengths 3 and 5 because there simply is not any way to form these cycles with such room templates. But cycles of length 4 are possible, so that is what we do here."),(0,l.kt)(h,{src:"2d/examples/example1/level_graph1.png",caption:"Level graph",mdxType:"Image"}),(0,l.kt)("h3",{id:"results"},"Results"),(0,l.kt)(d,{mdxType:"Gallery"},(0,l.kt)(h,{src:"2d/examples/example1/result2.png",caption:"Example result",mdxType:"Image"}),(0,l.kt)(h,{src:"2d/examples/example1/result3.png",caption:"Example result",mdxType:"Image"})),(0,l.kt)("h2",{id:"real-life-example"},"Real-life example"),(0,l.kt)("p",null,"To create something that is closer to a real-life example, we will:"),(0,l.kt)("ul",null,(0,l.kt)("li",{parentName:"ul"},"add spawn room template that includes a player"),(0,l.kt)("li",{parentName:"ul"},"add boss room that contains a ladder to the next level"),(0,l.kt)("li",{parentName:"ul"},"add doors to corridors"),(0,l.kt)("li",{parentName:"ul"},"add two more corridor room templates"),(0,l.kt)("li",{parentName:"ul"},"add enemies"),(0,l.kt)("li",{parentName:"ul"},"add more rooms to the level graph")),(0,l.kt)("h3",{id:"spawn-room"},"Spawn room"),(0,l.kt)("p",null,"Our spawn room will look different from our basic rooms. We will also want the generator to spawn our player prefab inside the room. This can be simply achieved by placing our prefab inside the room template, next to the ",(0,l.kt)("em",{parentName:"p"},"GameObject")," that holds our tilemaps."),(0,l.kt)(h,{src:"2d/examples/example1/spawn.png",caption:"Spawn room with player prefab",mdxType:"Image"}),(0,l.kt)("blockquote",null,(0,l.kt)("p",{parentName:"blockquote"},(0,l.kt)("strong",{parentName:"p"},"Note:")," A basic script for player movement is included in the example scene.")),(0,l.kt)("h3",{id:"boss-room"},"Boss room"),(0,l.kt)("p",null,"Our boss room will also have a special look. We also created a simple Exit prefab that looks like a ladder a generates a new level when interacted with. And similarly to placing our player prefab, we can also let the generator spawn a ",(0,l.kt)("em",{parentName:"p"},"mighty ogre")," that will guard the exit."),(0,l.kt)(h,{src:"2d/examples/example1/boss.png",caption:"Boss room template with exit prefab",mdxType:"Image"}),(0,l.kt)("blockquote",null,(0,l.kt)("p",{parentName:"blockquote"},(0,l.kt)("strong",{parentName:"p"},"Note:")," There is no enemy AI, so the ogre is really not that mighty.")),(0,l.kt)("h3",{id:"additional-room-template"},"Additional room template"),(0,l.kt)("p",null,"Even for ordinary rooms, we can have non-rectangular room templates."),(0,l.kt)(d,{cols:2,mdxType:"Gallery"},(0,l.kt)(h,{src:"2d/examples/example1/room3.png",caption:"Additional room tempalte",mdxType:"Image"})),(0,l.kt)("h3",{id:"doors"},"Doors"),(0,l.kt)("p",null,"We can easily add doors to our corridors. We created a simple door prefab that has a collider and also a trigger that lets the player open the door."),(0,l.kt)(h,{src:"2d/examples/example1/corridor_doors.png",caption:"Corridor with doors",mdxType:"Image"}),(0,l.kt)("h3",{id:"longer-corridors"},"Longer corridors"),(0,l.kt)(d,{cols:2,mdxType:"Gallery"},(0,l.kt)(h,{src:"2d/examples/example1/corridor_horizontal2.png",caption:"Longer horizontal corridor",mdxType:"Image"}),(0,l.kt)(h,{src:"2d/examples/example1/corridor_vertical2.png",caption:"Longer vertical corridor",mdxType:"Image"})),(0,l.kt)("h3",{id:"enemies"},"Enemies"),(0,l.kt)("p",null,"We can easily add enemies to our levels. In this tutorial, we will add enemies directly to room templates and then implement a post-processing task that spawns each enemy with a configurable chance. "),(0,l.kt)("p",null,'We will start by creating a GameObject called "Enemies" in all the room templates that will contain enemies a make all the enemies children of this GameObject.'),(0,l.kt)(h,{src:"2d/examples/example1/room_with_monsters.png",caption:"Enemies added to the room template",mdxType:"Image"}),(0,l.kt)("blockquote",null,(0,l.kt)("p",{parentName:"blockquote"},(0,l.kt)("strong",{parentName:"p"},"Note:"),' We must make sure to always name the root GameObject "Enemies" as we will use that name to work with the enemies.')),(0,l.kt)("p",null,"If we now generate the dungeon, we will see that it contains all the enemies that we added to individual room templates."),(0,l.kt)(h,{src:"2d/examples/example1/dungeon_with_monsters.png",caption:"Dungeon with monsters",mdxType:"Image"}),(0,l.kt)("p",null,"If we are happy with the results, we can stop here. However, to showcase how we can add some post-processing logic to the generator, we will try to spawn each monster with some predefined probability so that different monsters spawn every time. The result can be found below."),(0,l.kt)("blockquote",null,(0,l.kt)("p",{parentName:"blockquote"},(0,l.kt)("strong",{parentName:"p"},"Note:")," Since version ",(0,l.kt)("inlineCode",{parentName:"p"},"2.0.0-beta.0"),", the easiest way to implement a post-processing logic is with a ",(0,l.kt)("inlineCode",{parentName:"p"},"MonoBehaviour")," rather than a ",(0,l.kt)("inlineCode",{parentName:"p"},"ScriptableObject"),". So we will showcase that here.")),(0,l.kt)("p",null,"We have to create a class that inherits from ",(0,l.kt)("inlineCode",{parentName:"p"},"DungeonGeneratorPostProcessingComponentGrid2D"),". After a level is generated, the ",(0,l.kt)("inlineCode",{parentName:"p"},"Run")," method is called and that is the place where we call our post-processing logic."),(0,l.kt)(x,{name:"2d_example1_postProcessingComponent",mdxType:"ExternalCode"}),(0,l.kt)("p",null,"With the implementation ready, we now have to attach this component to the game object where we have our generator component attached."),(0,l.kt)(h,{src:"2d/examples/example1/custom_post_processing_component.png",caption:"Attach the component to the game object with the generator",height:"700",mdxType:"Image"}),(0,l.kt)("h3",{id:"level-graph-1"},"Level graph"),(0,l.kt)("p",null,"So the goal is to have more rooms than in the simple example and also a spawn room and a boss room. You can see one such level graph below."),(0,l.kt)(h,{src:"2d/examples/example1/level_graph2.png",caption:"Level graph",mdxType:"Image"}),(0,l.kt)("p",null,"You can also see that with corridors of different lengths and more room templates to choose from, we can now have cycles of various sizes. The boss and spawn rooms have assigned only a single room template."),(0,l.kt)("h3",{id:"results-1"},"Results"),(0,l.kt)(d,{cols:2,fixedHeight:!0,mdxType:"Gallery"},(0,l.kt)(h,{src:"2d/examples/example1/result_reallife2.png",caption:"Example result",mdxType:"Image"}),(0,l.kt)(h,{src:"2d/examples/example1/result_reallife3.png",caption:"Example result",mdxType:"Image"}),(0,l.kt)(h,{src:"2d/examples/example1/result_reallife4.png",caption:"Example result with enemies",mdxType:"Image"}),(0,l.kt)(h,{src:"2d/examples/example1/result_reallife5.png",caption:"Example result with enemies",mdxType:"Image"})))}w.isMDXComponent=!0}}]);