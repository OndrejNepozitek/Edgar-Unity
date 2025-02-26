"use strict";(self.webpackChunkmy_website=self.webpackChunkmy_website||[]).push([[3874],{603905:function(e,t,o){o.d(t,{Zo:function(){return d},kt:function(){return h}});var a=o(667294);function r(e,t,o){return t in e?Object.defineProperty(e,t,{value:o,enumerable:!0,configurable:!0,writable:!0}):e[t]=o,e}function n(e,t){var o=Object.keys(e);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);t&&(a=a.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),o.push.apply(o,a)}return o}function l(e){for(var t=1;t<arguments.length;t++){var o=null!=arguments[t]?arguments[t]:{};t%2?n(Object(o),!0).forEach((function(t){r(e,t,o[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(o)):n(Object(o)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(o,t))}))}return e}function i(e,t){if(null==e)return{};var o,a,r=function(e,t){if(null==e)return{};var o,a,r={},n=Object.keys(e);for(a=0;a<n.length;a++)o=n[a],t.indexOf(o)>=0||(r[o]=e[o]);return r}(e,t);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);for(a=0;a<n.length;a++)o=n[a],t.indexOf(o)>=0||Object.prototype.propertyIsEnumerable.call(e,o)&&(r[o]=e[o])}return r}var s=a.createContext({}),p=function(e){var t=a.useContext(s),o=t;return e&&(o="function"==typeof e?e(t):l(l({},t),e)),o},d=function(e){var t=p(e.components);return a.createElement(s.Provider,{value:t},e.children)},c={inlineCode:"code",wrapper:function(e){var t=e.children;return a.createElement(a.Fragment,{},t)}},m=a.forwardRef((function(e,t){var o=e.components,r=e.mdxType,n=e.originalType,s=e.parentName,d=i(e,["components","mdxType","originalType","parentName"]),m=p(o),h=r,u=m["".concat(s,".").concat(h)]||m[h]||c[h]||n;return o?a.createElement(u,l(l({ref:t},d),{},{components:o})):a.createElement(u,l({ref:t},d))}));function h(e,t){var o=arguments,r=t&&t.mdxType;if("string"==typeof e||r){var n=o.length,l=new Array(n);l[0]=m;var i={};for(var s in t)hasOwnProperty.call(t,s)&&(i[s]=t[s]);i.originalType=e,i.mdxType="string"==typeof e?e:r,l[1]=i;for(var p=2;p<n;p++)l[p]=o[p];return a.createElement.apply(null,l)}return a.createElement.apply(null,o)}m.displayName="MDXCreateElement"},516242:function(e,t,o){o.r(t),o.d(t,{frontMatter:function(){return i},contentTitle:function(){return s},metadata:function(){return p},toc:function(){return d},default:function(){return k}});var a=o(487462),r=o(263366),n=(o(667294),o(603905)),l=["components"],i={title:"Basics (step-by-step tutorial)"},s=void 0,p={unversionedId:"3d/examples/basics",id:"version-2.0.6/3d/examples/basics",title:"Basics (step-by-step tutorial)",description:"The goal of this tutorial is to show you how to generate your very first level using the 3D version of Edgar. I am going to use the great Prototype Textures asset pack by Kenney.",source:"@site/versioned_docs/version-2.0.6/3d/examples/basics.md",sourceDirName:"3d/examples",slug:"/3d/examples/basics",permalink:"/Edgar-Unity/docs/3d/examples/basics",editUrl:"https://github.com/OndrejNepozitek/Edgar-Unity/tree/docusaurus/versioned_docs/version-2.0.6/3d/examples/basics.md",tags:[],version:"2.0.6",frontMatter:{title:"Basics (step-by-step tutorial)"},sidebar:"3d",previous:{title:"Custom input",permalink:"/Edgar-Unity/docs/3d/generators/custom-input"},next:{title:"Dungeon 1",permalink:"/Edgar-Unity/docs/3d/examples/dungeon-1"}},d=[{value:"Simple example",id:"simple-example",children:[{value:"Blocks",id:"blocks",children:[],level:3},{value:"Generator settings",id:"generator-settings",children:[],level:3},{value:"Room template",id:"room-template",children:[],level:3},{value:"Door prefab",id:"door-prefab",children:[],level:3},{value:"Doors placement",id:"doors-placement",children:[],level:3},{value:"Level graph",id:"level-graph",children:[],level:3},{value:"Door blockers",id:"door-blockers",children:[],level:3},{value:"Results",id:"results",children:[],level:3}],level:2},{value:"Real-life example",id:"real-life-example",children:[{value:"Corridors",id:"corridors",children:[],level:3},{value:"Door connectors",id:"door-connectors",children:[],level:3},{value:"Additional room templates",id:"additional-room-templates",children:[],level:3},{value:"Level graph",id:"level-graph-1",children:[],level:3},{value:"Results",id:"results-1",children:[],level:3}],level:2}],c=function(e){return function(t){return console.warn("Component "+e+" was not imported, exported, or provided by MDXProvider as global scope"),(0,n.kt)("div",t)}},m=c("Gallery"),h=c("Image"),u=c("Path"),g={toc:d};function k(e){var t=e.components,o=(0,r.Z)(e,l);return(0,n.kt)("wrapper",(0,a.Z)({},g,o,{components:t,mdxType:"MDXLayout"}),(0,n.kt)("p",null,"The goal of this tutorial is to show you how to generate your very first level using the 3D version of Edgar. I am going to use the great ",(0,n.kt)("a",{parentName:"p",href:"https://www.kenney.nl/assets/prototype-textures"},"Prototype Textures")," asset pack by ",(0,n.kt)("a",{parentName:"p",href:"https://twitter.com/KenneyNL"},"Kenney"),"."),(0,n.kt)(m,{cols:2,fixedHeight:!0,mdxType:"Gallery"},(0,n.kt)(h,{src:"3d/examples/basics/result_3.png",caption:"Example result",mdxType:"Image"}),(0,n.kt)(h,{src:"3d/examples/basics/result_reallife_3.png",caption:"Example result",mdxType:"Image"})),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," All files from this example can be found at ",(0,n.kt)(u,{path:"3de:Basics",mdxType:"Path"}),".")),(0,n.kt)("h2",{id:"simple-example"},"Simple example"),(0,n.kt)("p",null,"The goal of the ",(0,n.kt)("em",{parentName:"p"},"simple example")," is to set up the bare minimum that is needed to generate a level."),(0,n.kt)("h3",{id:"blocks"},"Blocks"),(0,n.kt)("p",null,"The first step is to create some blocks that we will later use to create our room templates. We will use an orange block for walls and a black block for floors."),(0,n.kt)(h,{src:"3d/examples/basics/blocks.png",caption:"Basic blocks: floor (left), wall (middle), how they will interact when used together (right)",mdxType:"Image"}),(0,n.kt)("p",null,"Create an empty prefab game object. Then add a cube as a child object (3D Object \u2192 Cube). Assign the corresponding material to the cube together with a Box Collider."),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," Even if you have your own 3D models, it is a good idea to create prefabs for your main building blocks like floors or walls. With this approach, you can later decide to change the models or make other changes without needing to recreate all your room templates.")),(0,n.kt)(m,{cols:2,fixedHeight:!0,mdxType:"Gallery"},(0,n.kt)(h,{src:"3d/examples/basics/floor_not_aligned.png",caption:"Floor block (not aligned)",mdxType:"Image"}),(0,n.kt)(h,{src:"3d/examples/basics/floor_aligned.png",caption:"Floor block (aligned)",mdxType:"Image"})),(0,n.kt)("p",null,"The last step is to set the position of the cube so that it is aligned to the grid cells. As you can see in the image (left) above, the block is not aligned to the grid in the background, but we want it to be. In this case, I had to set the position to ",(0,n.kt)("inlineCode",{parentName:"p"},"(0.5, -0.25, 0.5)")," (right image). "),(0,n.kt)("h3",{id:"generator-settings"},"Generator settings"),(0,n.kt)("p",null,"The next step is to create an instance of the ",(0,n.kt)("inlineCode",{parentName:"p"},"GeneratorSettings")," scriptable object (",(0,n.kt)("em",{parentName:"p"},"SO"),") ",(0,n.kt)(u,{path:"3d:Generator settings",par:!0,mdxType:"Path"}),". This ",(0,n.kt)("em",{parentName:"p"},"SO")," is used to configure the size of the grid that we will be using for this tutorial. Open the settings and set the cell size to ",(0,n.kt)("em",{parentName:"p"},"(1, 0.5, 1)")," like in the image bellow. The ",(0,n.kt)("inlineCode",{parentName:"p"},"0.5")," value comes from the floor tile being only ",(0,n.kt)("inlineCode",{parentName:"p"},"0.5")," units high."),(0,n.kt)(h,{src:"3d/examples/basics/generator_settings.png",caption:"Generator settings for this tutorial",mdxType:"Image"}),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," The height (",(0,n.kt)("em",{parentName:"p"},"y axis"),") of the cell size is not important in this case. The walls are 1 unit high while the floors are only 0.5 units high. Therefore, we could use both heights of ",(0,n.kt)("em",{parentName:"p"},"1")," and ",(0,n.kt)("em",{parentName:"p"},"0.5"),", and it would work, just some gizmos would be shown in a slightly different way.")),(0,n.kt)("h3",{id:"room-template"},"Room template"),(0,n.kt)("p",null,"Create your first room template ",(0,n.kt)(u,{path:"3d:Dungeon room template",par:!0,mdxType:"Path"}),". Open the prefab and assign the ",(0,n.kt)("inlineCode",{parentName:"p"},"GeneratorSettings")," object created in the previous step to the ",(0,n.kt)("inlineCode",{parentName:"p"},"GeneratorSettings")," field of the ",(0,n.kt)("inlineCode",{parentName:"p"},"RoomTemplateSettings")," component."),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Tip:")," When creating room templates and doors, you are required to assign the generator settings object to them. If you want to automate this step, open your ",(0,n.kt)("inlineCode",{parentName:"p"},"GeneratorSettings")," object and click the ",(0,n.kt)("em",{parentName:"p"},"Mark as default generator settings")," button. When you create a new room template, the chosen default generator settings will be automatically assigned.")),(0,n.kt)("p",null,"Next, use the floor and wall blocks to design the room template. Make sure to add these blocks as children of the ",(0,n.kt)("inlineCode",{parentName:"p"},"Blocks")," game object so that they can be recognized by the outline computing algorithm. The room template I created looks like this:"),(0,n.kt)(h,{src:"3d/examples/basics/room_1_no_doors.png",caption:"Basic room template without doors, the yellow gizmo display the outline as seen by the generator",mdxType:"Image"}),(0,n.kt)("p",null,"Make sure that the outline of the room template is aligned to the underlying grid. In the image above, you can see that yellow outline gizmo precisely follows the intended outline of the room template."),(0,n.kt)("p",null,"You might be wondering why there is so much empty space on the outline of the room template where walls should be. The reason is that we will use this space to mark potential door positions in the next section."),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"WARNING:")," It is not possible to decrease the cell size in order to hide the fact that a room template is not aligned to the grid. See the two images below. The outline of the room template in the images should be a square, and changing the cell size of the grid does not fix the fact that one of the floor tiles is incorrectly positioned."),(0,n.kt)(m,{cols:2,fixedHeight:!0,mdxType:"Gallery"},(0,n.kt)(h,{src:"3d/examples/basics/outline_not_aligned_1.png",caption:"Wrong - the outline is incorrect because one floor tile is not aligned to the grid",mdxType:"Image"}),(0,n.kt)(h,{src:"3d/examples/basics/outline_not_aligned_2.png",caption:"Wrong - the cell size was decreased (0.1, 0.5, 0.1) so the outline now follows the room template, but it is still wrong",mdxType:"Image"})),(0,n.kt)("p",{parentName:"blockquote"},"The correct solutions are:"),(0,n.kt)("ul",{parentName:"blockquote"},(0,n.kt)("li",{parentName:"ul"},"Move the floor tile so that it does not cover the neighbouring grid cell"),(0,n.kt)("li",{parentName:"ul"},"Increase the ",(0,n.kt)("inlineCode",{parentName:"li"},"ColliderSizeTolerance")," field in the ",(0,n.kt)("inlineCode",{parentName:"li"},"GeneratorSettings")," object"))),(0,n.kt)("h3",{id:"door-prefab"},"Door prefab"),(0,n.kt)("p",null,"The room template is now ready for doors to be added. Create a door prefab by going to ",(0,n.kt)(u,{path:"3d:Door",mdxType:"Path"}),". Open the prefab and, again, assign the ",(0,n.kt)("inlineCode",{parentName:"p"},"GeneratorSettings")," field. Now, you should see something like in the image bellow on the left."),(0,n.kt)("p",null,"The darker red gizmos show the orientation of the door - the dark part is directed outwards from the room. The more transparent red gizmo shows the total volume of the door. By default, each door is ",(0,n.kt)("em",{parentName:"p"},"1 block")," long in all dimensions. That means that with the cell size in the generator settings, the volume is ",(0,n.kt)("inlineCode",{parentName:"p"},"(1, 0.5, 1)")," units. "),(0,n.kt)("p",null,"For this tutorial, I decided that the doors will be ",(0,n.kt)("em",{parentName:"p"},"2 blocks")," wide, so change the ",(0,n.kt)("inlineCode",{parentName:"p"},"Width")," field in the ",(0,n.kt)("inlineCode",{parentName:"p"},"DoorsHandler")," component to ",(0,n.kt)("inlineCode",{parentName:"p"},"2"),". You should now see something like in the image bellow on the right."),(0,n.kt)(m,{cols:2,fixedHeight:!0,mdxType:"Gallery"},(0,n.kt)(h,{src:"3d/examples/basics/empty_door_1_wide.png",caption:"1 block wide door",mdxType:"Image"}),(0,n.kt)(h,{src:"3d/examples/basics/empty_door_2_wide.png",caption:"2 blocks wide door",mdxType:"Image"})),(0,n.kt)("h3",{id:"doors-placement"},"Doors placement"),(0,n.kt)("p",null,"Now we are ready to use our door prefab and place it inside the room template. You can now open the room template prefab and put the door prefab inside the ",(0,n.kt)("em",{parentName:"p"},"Doors")," game object. Then, move the prefab to the outline of the room template, but make sure that the door is properly aligned to the grid. Also, make sure that the dark red gizmo points out of the room. If you need to rotate the door, use the ",(0,n.kt)("inlineCode",{parentName:"p"},"Rotate -90")," and ",(0,n.kt)("inlineCode",{parentName:"p"},"Rotate +90")," buttons in the inspector. The result should look like something like this:"),(0,n.kt)(h,{src:"3d/examples/basics/room_1_single_door.png",caption:"Basic room template with a single door",mdxType:"Image"}),(0,n.kt)("p",null,"The next step is ",(0,n.kt)("strong",{parentName:"p"},"VERY IMPORTANT"),". Instead of filling all the holes with individual door prefabs, you should select the current door and change its ",(0,n.kt)("inlineCode",{parentName:"p"},"Repeat")," property to ",(0,n.kt)("inlineCode",{parentName:"p"},"2")," because there are two additional blocks next to the door that should be covered. The result should look like this:"),(0,n.kt)(h,{src:"3d/examples/basics/room_1_single_door_repeat.png",caption:"Basic room template with a single door, using the Repeat property to expand the door by 2 blocks",mdxType:"Image"}),(0,n.kt)("p",null,"Next, repeat the process for the 3 remaining sides of the room template, each time changing the value of the ",(0,n.kt)("inlineCode",{parentName:"p"},"Repeat")," property. You should end up with ",(0,n.kt)("em",{parentName:"p"},"4")," instance of the ",(0,n.kt)("em",{parentName:"p"},"Door")," prefab under the ",(0,n.kt)("em",{parentName:"p"},"Doors")," object. Also, make sure to use the correct orientation for each door."),(0,n.kt)(h,{src:"3d/examples/basics/room_1.png",caption:"Basic room template with 4 door prefabs added to it, each expanded with the Repeat property",mdxType:"Image"}),(0,n.kt)("h3",{id:"level-graph"},"Level graph"),(0,n.kt)("p",null,"For the simple variant of this example, we will use a very simple acyclic level graph that will guarantee quick generation times. If this is your first time creating a level graph, plase consult the ",(0,n.kt)("a",{parentName:"p",href:"/Edgar-Unity/docs/3d/basics/level-graphs"},"Level graph")," page."),(0,n.kt)(h,{src:"3d/examples/basics/simple_level_graph.png",caption:"Simple level graph",mdxType:"Image"}),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," If you want to generate some levels at this stage of the tutorial, you have to uncheck the ",(0,n.kt)("inlineCode",{parentName:"p"},"Use Corridors")," field on the dungeon generator component.")),(0,n.kt)("h3",{id:"door-blockers"},"Door blockers"),(0,n.kt)("p",null,"If we now generate a level, we will get something similar to the image below. The rooms are somehow correctly positioned but there are no walls at all the places that we marked as potential doors but which were not used by the generator."),(0,n.kt)(h,{src:"3d/examples/basics/result_no_blockers.png",caption:"Generated level without door blockers",mdxType:"Image"}),(0,n.kt)("p",null,"To fix this problem, we have to use a so-called ",(0,n.kt)("strong",{parentName:"p"},"door blocker"),", which is an object that will block doors that were not used by the generator. Open the door prefab again and you should see a think green gizmo which shows where to put the door blocker. Create an empty game object inside the door prefab, call it ",(0,n.kt)("em",{parentName:"p"},"Blocker")," and add your wall tile inside of it. Make sure that the wall is correctly positioned inside the green gizmo. Lastly, drag and drop the ",(0,n.kt)("em",{parentName:"p"},"Blocker")," game object inside the ",(0,n.kt)("inlineCode",{parentName:"p"},"Blockers")," field of the ",(0,n.kt)("inlineCode",{parentName:"p"},"DoorHandler")," component. The result should look like this:"),(0,n.kt)(h,{src:"3d/examples/basics/door_blocker.png",caption:"Door prefab with a blocker",mdxType:"Image"}),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," After you are done with the door blocker, it is a good idea to make the root game object of the blocker inactive so that it does not get in the way. ")),(0,n.kt)("p",null,"If we now generate a level, it should have all the unused doors filled with walls."),(0,n.kt)(h,{src:"3d/examples/basics/result_only_blockers.png",caption:"Generated level with door blockers",mdxType:"Image"}),(0,n.kt)("h3",{id:"results"},"Results"),(0,n.kt)(m,{cols:2,fixedHeight:!0,mdxType:"Gallery"},(0,n.kt)(h,{src:"3d/examples/basics/result_1.png",caption:"Example result",mdxType:"Image"}),(0,n.kt)(h,{src:"3d/examples/basics/result_2.png",caption:"Example result",mdxType:"Image"})),(0,n.kt)("h2",{id:"real-life-example"},"Real-life example"),(0,n.kt)("p",null,"The goal of the ",(0,n.kt)("em",{parentName:"p"},"real-life example")," is to add some extra stuff to the simple version."),(0,n.kt)("h3",{id:"corridors"},"Corridors"),(0,n.kt)("p",null,"It is quite common for procedural dungeons to have short corridors between rooms. In Edgar, it can also improve the performance of the generator as there are more ways of connecting different rooms. Bellow, you can see two corridor room templates that I created."),(0,n.kt)(m,{cols:2,fixedHeight:!0,mdxType:"Gallery"},(0,n.kt)(h,{src:"3d/examples/basics/corridor_1.png",caption:"Shorter corridor",mdxType:"Image"}),(0,n.kt)(h,{src:"3d/examples/basics/corridor_2.png",caption:"Longer corridor",mdxType:"Image"})),(0,n.kt)("p",null,"The generated levels should now look like this:"),(0,n.kt)(h,{src:"3d/examples/basics/result_corridors.png",caption:"Generated level with corridors",mdxType:"Image"}),(0,n.kt)("h3",{id:"door-connectors"},"Door connectors"),(0,n.kt)("p",null,"In the previous sections, I showcased the use of door blockers to block potential door blocks that were not used. Another useful feature are so-called ",(0,n.kt)("strong",{parentName:"p"},"door connectors"),". There are game objects that are placed at locations that are actually used for doors. Open the door prefab and this time, create a game object called ",(0,n.kt)("em",{parentName:"p"},"Connector"),". I created a simple red door frame to be placed when the door is used (image bellow). Do not forget to drag and drop the connector to the ",(0,n.kt)("inlineCode",{parentName:"p"},"Connectors")," array in the ",(0,n.kt)("inlineCode",{parentName:"p"},"DoorHandler")," component."),(0,n.kt)(h,{src:"3d/examples/basics/door_connector.png",caption:"Door prefab with a blocker and a connector",mdxType:"Image"}),(0,n.kt)("p",null,"The generated levels should now look like this:"),(0,n.kt)(h,{src:"3d/examples/basics/result_blockers_and_connectors.png",caption:"Generated level with corridors, blockers and connectors",mdxType:"Image"}),(0,n.kt)("h3",{id:"additional-room-templates"},"Additional room templates"),(0,n.kt)("p",null,"I also created some additional room templates (shown in the images bellow). In the room template on the left, you can see that there might be walls that are not aligned to the grid. But the resulting outline (yellow lines) will always be aligned to the grid. Also, you have to make sure that all the doors are aligned to the grid. In the room template on the right, you can see that you do not have to add doors to all the sides of the room template."),(0,n.kt)(m,{cols:2,fixedHeight:!0,mdxType:"Gallery"},(0,n.kt)(h,{src:"3d/examples/basics/room_2.png",caption:"Additional room template",mdxType:"Image"}),(0,n.kt)(h,{src:"3d/examples/basics/room_3.png",caption:"Additional room template",mdxType:"Image"})),(0,n.kt)("h3",{id:"level-graph-1"},"Level graph"),(0,n.kt)(h,{src:"3d/examples/basics/level_graph.png",caption:"Level graph with more rooms",mdxType:"Image"}),(0,n.kt)("h3",{id:"results-1"},"Results"),(0,n.kt)(h,{src:"3d/examples/basics/result_reallife_1.png",caption:"Example result",mdxType:"Image"}),(0,n.kt)(h,{src:"3d/examples/basics/result_reallife_2.png",caption:"Example result",mdxType:"Image"}))}k.isMDXComponent=!0}}]);