"use strict";(self.webpackChunkmy_website=self.webpackChunkmy_website||[]).push([[5204],{603905:function(e,t,a){a.d(t,{Zo:function(){return p},kt:function(){return u}});var o=a(667294);function r(e,t,a){return t in e?Object.defineProperty(e,t,{value:a,enumerable:!0,configurable:!0,writable:!0}):e[t]=a,e}function i(e,t){var a=Object.keys(e);if(Object.getOwnPropertySymbols){var o=Object.getOwnPropertySymbols(e);t&&(o=o.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),a.push.apply(a,o)}return a}function n(e){for(var t=1;t<arguments.length;t++){var a=null!=arguments[t]?arguments[t]:{};t%2?i(Object(a),!0).forEach((function(t){r(e,t,a[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(a)):i(Object(a)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(a,t))}))}return e}function l(e,t){if(null==e)return{};var a,o,r=function(e,t){if(null==e)return{};var a,o,r={},i=Object.keys(e);for(o=0;o<i.length;o++)a=i[o],t.indexOf(a)>=0||(r[a]=e[a]);return r}(e,t);if(Object.getOwnPropertySymbols){var i=Object.getOwnPropertySymbols(e);for(o=0;o<i.length;o++)a=i[o],t.indexOf(a)>=0||Object.prototype.propertyIsEnumerable.call(e,a)&&(r[a]=e[a])}return r}var s=o.createContext({}),d=function(e){var t=o.useContext(s),a=t;return e&&(a="function"==typeof e?e(t):n(n({},t),e)),a},p=function(e){var t=d(e.components);return o.createElement(s.Provider,{value:t},e.children)},h={inlineCode:"code",wrapper:function(e){var t=e.children;return o.createElement(o.Fragment,{},t)}},m=o.forwardRef((function(e,t){var a=e.components,r=e.mdxType,i=e.originalType,s=e.parentName,p=l(e,["components","mdxType","originalType","parentName"]),m=d(a),u=r,c=m["".concat(s,".").concat(u)]||m[u]||h[u]||i;return a?o.createElement(c,n(n({ref:t},p),{},{components:a})):o.createElement(c,n({ref:t},p))}));function u(e,t){var a=arguments,r=t&&t.mdxType;if("string"==typeof e||r){var i=a.length,n=new Array(i);n[0]=m;var l={};for(var s in t)hasOwnProperty.call(t,s)&&(l[s]=t[s]);l.originalType=e,l.mdxType="string"==typeof e?e:r,n[1]=l;for(var d=2;d<i;d++)n[d]=a[d];return o.createElement.apply(null,n)}return o.createElement.apply(null,a)}m.displayName="MDXCreateElement"},77045:function(e,t,a){a.r(t),a.d(t,{frontMatter:function(){return l},contentTitle:function(){return s},metadata:function(){return d},toc:function(){return p},default:function(){return k}});var o=a(487462),r=a(263366),i=(a(667294),a(603905)),n=["components"],l={title:"(PRO) Fog of war"},s=void 0,d={unversionedId:"guides/fog-of-war",id:"version-2.0.6/guides/fog-of-war",title:"(PRO) Fog of war",description:"In this guide, we will learn how to enable a simple fog of war functionality.",source:"@site/versioned_docs/version-2.0.6/guides/fog-of-war.md",sourceDirName:"guides",slug:"/guides/fog-of-war",permalink:"/Edgar-Unity/docs/guides/fog-of-war",editUrl:"https://github.com/OndrejNepozitek/Edgar-Unity/tree/docusaurus/versioned_docs/version-2.0.6/guides/fog-of-war.md",tags:[],version:"2.0.6",frontMatter:{title:"(PRO) Fog of war"},sidebar:"docs",previous:{title:"Current room detection",permalink:"/Edgar-Unity/docs/guides/current-room-detection"},next:{title:"(PRO) Minimap",permalink:"/Edgar-Unity/docs/guides/minimap"}},p=[{value:"Limitations",id:"limitations",children:[],level:2},{value:"Prerequisites",id:"prerequisites",children:[],level:2},{value:"Example scene",id:"example-scene",children:[],level:2},{value:"Setup",id:"setup",children:[{value:"Add Fog of War component",id:"add-fog-of-war-component",children:[],level:3},{value:"Include shader in build",id:"include-shader-in-build",children:[],level:3},{value:"Add custom renderer feature",id:"add-custom-renderer-feature",children:[],level:3},{value:"Add custom post-processing logic",id:"add-custom-post-processing-logic",children:[],level:3},{value:"Add trigger colliders",id:"add-trigger-colliders",children:[],level:3}],level:2},{value:"API",id:"api",children:[],level:2},{value:"Configuration and examples",id:"configuration-and-examples",children:[{value:"Fog colour",id:"fog-colour",children:[],level:3},{value:"Transition mode",id:"transition-mode",children:[{value:"Fog value",id:"fog-value",children:[],level:4},{value:"<code>TileGranularity</code>",id:"tilegranularity",children:[],level:4},{value:"<code>FogSmoothness</code>",id:"fogsmoothness",children:[],level:4}],level:3},{value:"Wave mode",id:"wave-mode",children:[{value:"<code>WaveSpeed</code>",id:"wavespeed",children:[],level:4},{value:"<code>WaveRevealThreshold</code>",id:"waverevealthreshold",children:[],level:4}],level:3},{value:"Fade In mode",id:"fade-in-mode",children:[{value:"<code>FadeInDuration</code>",id:"fadeinduration",children:[],level:4}],level:3},{value:"Reveal Corridors",id:"reveal-corridors",children:[{value:"<code>RevealCorridorsTiles</code>",id:"revealcorridorstiles",children:[],level:4},{value:"<code>RevealCorridorsGradually</code>",id:"revealcorridorsgradually",children:[],level:4}],level:3},{value:"Initial fog transparency",id:"initial-fog-transparency",children:[],level:3}],level:2},{value:"Implementation and performance",id:"implementation-and-performance",children:[],level:2},{value:"Advanced",id:"advanced",children:[{value:"Serialization",id:"serialization",children:[],level:3}],level:2}],h=function(e){return function(t){return console.warn("Component "+e+" was not imported, exported, or provided by MDXProvider as global scope"),(0,i.kt)("div",t)}},m=h("Image"),u=h("Path"),c=h("ExternalCode"),g=h("Gallery"),f={toc:p};function k(e){var t=e.components,a=(0,r.Z)(e,n);return(0,i.kt)("wrapper",(0,o.Z)({},f,a,{components:t,mdxType:"MDXLayout"}),(0,i.kt)("p",null,"In this guide, we will learn how to enable a simple fog of war functionality."),(0,i.kt)(m,{src:"2d/guides/fog_of_war/example.gif",caption:"Fog of War example (exported to a GIF with low FPS, everything is smooth ingame)",mdxType:"Image"}),(0,i.kt)("h2",{id:"limitations"},"Limitations"),(0,i.kt)("ul",null,(0,i.kt)("li",{parentName:"ul"},(0,i.kt)("strong",{parentName:"li"},"Performance on large levels")," - Not suitable for very large levels (e.g. area larger than 500x500 tiles) - see ",(0,i.kt)("a",{parentName:"li",href:"/Edgar-Unity/docs/guides/fog-of-war#implementation-and-performance"},"Implementation and performance")),(0,i.kt)("li",{parentName:"ul"},(0,i.kt)("strong",{parentName:"li"},"Isometric not supported")," - Does not support isometric games/levels"),(0,i.kt)("li",{parentName:"ul"},"Does not work in levels where the camera is not perpendicular to the level (e.g. if the camera is positioned at an angle)")),(0,i.kt)("h2",{id:"prerequisites"},"Prerequisites"),(0,i.kt)("ul",null,(0,i.kt)("li",{parentName:"ul"},(0,i.kt)("strong",{parentName:"li"},"Built-in Render Pipeline")," - works in Unity 2018.4+"),(0,i.kt)("li",{parentName:"ul"},(0,i.kt)("strong",{parentName:"li"},"Lightweight Render Pipeline (LWRP)")," - version >= 6.5 of LWRP is needed (and therefore Unity 2019.2+)"),(0,i.kt)("li",{parentName:"ul"},(0,i.kt)("strong",{parentName:"li"},"Universal Render Pipeline (URP)")," - version >= 7.0 of URP is needed (and therefore Unity 2019.3+)",(0,i.kt)("ul",{parentName:"li"},(0,i.kt)("li",{parentName:"ul"},(0,i.kt)("strong",{parentName:"li"},"IMPORTANT!")," - this feature ",(0,i.kt)("strong",{parentName:"li"},"now works")," with the ",(0,i.kt)("strong",{parentName:"li"},"2D Renderer (Experimental)")," since Unity 2021.3+"))),(0,i.kt)("li",{parentName:"ul"},(0,i.kt)("strong",{parentName:"li"},"High Definition Render Pipeline (HDRP)")," - currently not supported")),(0,i.kt)("blockquote",null,(0,i.kt)("p",{parentName:"blockquote"},(0,i.kt)("strong",{parentName:"p"},"Note:")," Additional setup steps are needed for LWRP and URP, see below.")),(0,i.kt)("h2",{id:"example-scene"},"Example scene"),(0,i.kt)("p",null,"An example scene can be found at ",(0,i.kt)(u,{path:"2de:FogOfWarExample",mdxType:"Path"}),". "),(0,i.kt)("blockquote",null,(0,i.kt)("p",{parentName:"blockquote"},(0,i.kt)("strong",{parentName:"p"},"Note:")," Please see the ",(0,i.kt)("a",{parentName:"p",href:"/Edgar-Unity/docs/guides/fog-of-war#add-custom-renderer-feature"},"Add custom renderer feature")," setup step below to make the example scene work in URP/LWRP.")),(0,i.kt)("h2",{id:"setup"},"Setup"),(0,i.kt)("p",null,"There are several steps that need to be done to enable the ",(0,i.kt)("em",{parentName:"p"},"Fog of War")," feature. I tried to make the setup as simple as possible, but it still requires some manual work."),(0,i.kt)("h3",{id:"add-fog-of-war-component"},"Add Fog of War component"),(0,i.kt)("p",null,"The first step is to add the ",(0,i.kt)("strong",{parentName:"p"},"Fog of War")," component to the game object that holds the main camera of your game. The component has several settings, see the ",(0,i.kt)("a",{parentName:"p",href:"/Edgar-Unity/docs/guides/fog-of-war#configuration-and-examples"},"Configuration")," section."),(0,i.kt)("h3",{id:"include-shader-in-build"},"Include shader in build"),(0,i.kt)("p",null,"This feature uses a custom shader that you have to manually include in the build of your game. Navigate to ",(0,i.kt)(u,{path:"Edit/Project Settings/Graphics",mdxType:"Path"})," and add the ",(0,i.kt)("em",{parentName:"p"},"Edgar/FogOfWar")," (and ",(0,i.kt)("em",{parentName:"p"},"Edgar/FogOfWarURP"),") shader to the ",(0,i.kt)("em",{parentName:"p"},"Always Included Shaders")," list. If you do not do that, the game will work in the editor, but it won't work in a standalone build."),(0,i.kt)("h3",{id:"add-custom-renderer-feature"},"Add custom renderer feature"),(0,i.kt)("p",null,(0,i.kt)("strong",{parentName:"p"},"(URP and LWRP only)")," - This step is only required if you use URP or LWRP as they do not support the ",(0,i.kt)("a",{parentName:"p",href:"https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnRenderImage.html"},"OnRenderImage()")," hook. To work around that, you have to manually add a custom renderer feature."),(0,i.kt)("p",null,(0,i.kt)("strong",{parentName:"p"},"LWRP"),": The first step is to create a custom ",(0,i.kt)("strong",{parentName:"p"},"Forward Renderer")," asset if you do not have one already. This can be done in ",(0,i.kt)(u,{path:"Create/Rendering/LWRP/Forward Renderer",mdxType:"Path"}),". You have to make sure that this renderer is used in the render pipeline instead of the default one."),(0,i.kt)("p",null,(0,i.kt)("strong",{parentName:"p"},"URP"),": With URP, there should already exist a ",(0,i.kt)("strong",{parentName:"p"},"Forward Renderer")," asset that was created with the render pipeline asset. Locate the renderer."),(0,i.kt)("p",null,"The next step is to open the ",(0,i.kt)("strong",{parentName:"p"},"Forward Renderer")," asset. Click the ",(0,i.kt)("em",{parentName:"p"},"Add Renderer Feature")," and choose the ",(0,i.kt)("em",{parentName:"p"},"Fog Of War Feature"),". Based on the render pipeline that you are using, the feature should be called ",(0,i.kt)("em",{parentName:"p"},"Fog Of War LWRP Feature")," or ",(0,i.kt)("em",{parentName:"p"},"Fog Of War URP Feature"),"."),(0,i.kt)("blockquote",null,(0,i.kt)("p",{parentName:"blockquote"},(0,i.kt)("strong",{parentName:"p"},"Note:")," If you do not see the ",(0,i.kt)("em",{parentName:"p"},"Fog Of War Feature"),", make sure you meet the requirements as described ",(0,i.kt)("a",{parentName:"p",href:"/Edgar-Unity/docs/guides/fog-of-war#prerequisites"},"above"),". The feature is conditionally compiled only if there are all the required packages installed.")),(0,i.kt)("h3",{id:"add-custom-post-processing-logic"},"Add custom post-processing logic"),(0,i.kt)("p",null,"After a level is generated, we have to set up the Fog of War component. The best place to do that is in a ",(0,i.kt)("a",{parentName:"p",href:"/Edgar-Unity/docs/generators/post-process#custom-post-processing"},"custom post-processing task"),". Sample code with comments can be seen below:"),(0,i.kt)(c,{name:"2d_fogOfWar_postProcessing",mdxType:"ExternalCode"}),(0,i.kt)("blockquote",null,(0,i.kt)("p",{parentName:"blockquote"},(0,i.kt)("strong",{parentName:"p"},"Note:")," More information about the API of the ",(0,i.kt)("em",{parentName:"p"},"FogOfWar")," component can be found in the ",(0,i.kt)("a",{parentName:"p",href:"/Edgar-Unity/docs/guides/fog-of-war#api"},"API")," section.")),(0,i.kt)("p",null,"After enabling this post-processing task and starting the game, we should see that all but the spawn room are hidden in the fog. The last thing that we have to handle is to reveal rooms when we enter them."),(0,i.kt)("h3",{id:"add-trigger-colliders"},"Add trigger colliders"),(0,i.kt)("p",null,"The last step is to add trigger colliders that will reveal a room when the player enters that room. There are at least two possible ways of doing that:"),(0,i.kt)("ol",null,(0,i.kt)("li",{parentName:"ol"},"Add colliders to all room templates and always reveal only the room for which the collider was triggered"),(0,i.kt)("li",{parentName:"ol"},"Add colliders only to corridor room templates and when you trigger the collider reveal both the collider and its neighbours")),(0,i.kt)("p",null,"In this tutorial, I decided to use the second option as it, in my opinion, looks better. This is the final result that we want to achieve:"),(0,i.kt)(m,{src:"2d/guides/fog_of_war/colliders_goal.gif",caption:"Rooms are revealed after a corridor is visited",mdxType:"Image"}),(0,i.kt)("p",null,"To achieve this, we have to slightly modify our corridor room templates. In each corridor room template, we create a child game object with a collider that has ",(0,i.kt)("em",{parentName:"p"},"Is Trigger")," set to true. The collider should mark the area where the player has to stand to trigger the fog of war script. The easiest thing to do is to add a collider that covers the whole floor of the corridor. However, it is also possible to make the collider slightly smaller and trigger the script only when the player is closer to the centre of the corridor. Our example setup can be seen below:"),(0,i.kt)(m,{src:"2d/guides/fog_of_war/colliders_setup.png",caption:"Setup of the corridor Fog of War trigger",mdxType:"Image"}),(0,i.kt)("p",null,"The last step is to add a simple component (to the game object with the collider) that will call the Fog of War script when the collider is triggered:"),(0,i.kt)(c,{name:"2d_fogOfWar_triggerHandler",mdxType:"ExternalCode"}),(0,i.kt)("p",null,"After you modify all your corridor room templates, the Fog of War should work as expected."),(0,i.kt)("h2",{id:"api"},"API"),(0,i.kt)("p",null,"Up-to-date API documentation can be found ","[here][FogOfWar]","."),(0,i.kt)("h2",{id:"configuration-and-examples"},"Configuration and examples"),(0,i.kt)("h3",{id:"fog-colour"},"Fog colour"),(0,i.kt)("p",null,"The ",(0,i.kt)("em",{parentName:"p"},"FogColor")," property specifies the colour of tiles that are hidden in the fog. It defaults to black but in our example scene we use purple as that is the colour which works best with the tileset."),(0,i.kt)(g,{cols:2,mdxType:"Gallery"},(0,i.kt)(m,{src:"2d/guides/fog_of_war/fog_color_purple.png",caption:"FogColor set to purple",mdxType:"Image"}),(0,i.kt)(m,{src:"2d/guides/fog_of_war/fog_color_black.png",caption:"FogColor set to black",mdxType:"Image"})),(0,i.kt)("h3",{id:"transition-mode"},"Transition mode"),(0,i.kt)("p",null,"The ",(0,i.kt)("em",{parentName:"p"},"Transition mode")," property specifies what to do when there are two neighbouring tiles with different fog values (i.e. one tile is less revealed than the other tile). If we use the ",(0,i.kt)("em",{parentName:"p"},"Smooth")," mode, the colours of pixels on the two tiles will smoothly interpolate from one fog value to the other. On the other hand, if we use the ",(0,i.kt)("em",{parentName:"p"},"Tile Based")," mode, no interpolation will be used and there will be a strict division between the fog values of the two tiles. There is also a third mode now - the ",(0,i.kt)("em",{parentName:"p"},"Custom")," mode that is somewhere between the two previous modes and adds two additional parameters - ",(0,i.kt)("em",{parentName:"p"},"Tile granularity")," and ",(0,i.kt)("em",{parentName:"p"},"Fog smoothness"),"."),(0,i.kt)(g,{cols:2,mdxType:"Gallery"},(0,i.kt)(m,{src:"2d/guides/fog_of_war/transition_mode_smooth.gif",caption:"TransitionMode set to Smooth (gif)",mdxType:"Image"}),(0,i.kt)(m,{src:"2d/guides/fog_of_war/transition_mode_tile_based.gif",caption:"TransitionMode set to Tile Based (gif)",mdxType:"Image"})),(0,i.kt)("h4",{id:"fog-value"},"Fog value"),(0,i.kt)("p",null,"Before we dive into the next two parameters, it is good to know what is the ",(0,i.kt)("em",{parentName:"p"},"fog (transparency) value")," of a pixel. When we want to show that a pixel is affected by the fog of war effect, we interpolate between the colour of that pixel and the fog colour. The value that controls the interpolation is called the ",(0,i.kt)("em",{parentName:"p"},"fog value"),". This ",(0,i.kt)("em",{parentName:"p"},"fog value")," is always in the ",(0,i.kt)("em",{parentName:"p"},"[0,1]")," range. To completely hide a pixel in the fog, we set its ",(0,i.kt)("em",{parentName:"p"},"fog value")," to 0. To completely show a pixel, we set its ",(0,i.kt)("em",{parentName:"p"},"fog value")," to 1. If we want to animate the transition between pixels being hidden and then revealed, we can gradually increase the ",(0,i.kt)("em",{parentName:"p"},"fog value"),", starting with the initial ",(0,i.kt)("em",{parentName:"p"},"fog value")," and going all the way up to 1."),(0,i.kt)("h4",{id:"tilegranularity"},(0,i.kt)("inlineCode",{parentName:"h4"},"TileGranularity")),(0,i.kt)("p",null,"This parameter controls into how many pixel chunks is each tile divided. The actual relation between the value of this parameter and the number of chunks is that if the value is equal to ",(0,i.kt)("em",{parentName:"p"},"X"),", each tile is divided into ",(0,i.kt)("em",{parentName:"p"},"X","*","X")," same-sized chunks. That means that if we set the value to 2, we will get ",(0,i.kt)("em",{parentName:"p"},"2x2=4")," chunks in total - each tile is divided into quarters."),(0,i.kt)("p",null,"The main property of individual chunks is that each pixel in a chunk has the same ",(0,i.kt)("em",{parentName:"p"},"fog value")," as all the other pixels in the chunk. Both the ",(0,i.kt)("em",{parentName:"p"},"Tile-based")," and ",(0,i.kt)("em",{parentName:"p"},"Smooth")," transition modes are special cases of this division to chunks. The ",(0,i.kt)("em",{parentName:"p"},"Tile-based")," mode equals to setting ",(0,i.kt)("em",{parentName:"p"},"TileGranularity")," to 1 - each tile is a single chunk. The ",(0,i.kt)("em",{parentName:"p"},"Smooth")," mode equals to setting ",(0,i.kt)("em",{parentName:"p"},"TileGranularity")," to infinity - each pixel is independent of other pixels. "),(0,i.kt)(g,{cols:2,mdxType:"Gallery"},(0,i.kt)(m,{src:"2d/guides/fog_of_war/granularity_1.gif",caption:"TileGranularity set to 1 (gif)",mdxType:"Image"}),(0,i.kt)(m,{src:"2d/guides/fog_of_war/granularity_2.gif",caption:"TileGranularity set to 2 (gif)",mdxType:"Image"})),(0,i.kt)("h4",{id:"fogsmoothness"},(0,i.kt)("inlineCode",{parentName:"h4"},"FogSmoothness")),(0,i.kt)("p",null,"This parameter controls how many possible ",(0,i.kt)("em",{parentName:"p"},"fog values")," there are for every pixel. In other words, the parameter controls how many possible steps there are between the ",(0,i.kt)("em",{parentName:"p"},"fog values")," 0 and 1. The exact formula is ",(0,i.kt)("em",{parentName:"p"},"stepSize = 1 / fogSmoothness"),". For example, by setting the ",(0,i.kt)("em",{parentName:"p"},"FogSmoothness")," to ",(0,i.kt)("em",{parentName:"p"},"2"),", the step size is equal to ",(0,i.kt)("em",{parentName:"p"},"1/4 = 0.25")," and there are 5 possible fog values - ",(0,i.kt)("em",{parentName:"p"},"0"),", ",(0,i.kt)("em",{parentName:"p"},"0.25"),", ",(0,i.kt)("em",{parentName:"p"},"0.5"),", ",(0,i.kt)("em",{parentName:"p"},"0.75")," and ",(0,i.kt)("em",{parentName:"p"},"1"),"."),(0,i.kt)("p",null,"If you want to emphasize the transitions between individual ",(0,i.kt)("em",{parentName:"p"},"fog values")," set the parameter to a lower value, for example ",(0,i.kt)("em",{parentName:"p"},"10"),". If you want to have a very smooth transition between individual ",(0,i.kt)("em",{parentName:"p"},"fog values"),", use a high value, for example ",(0,i.kt)("em",{parentName:"p"},"100"),"."),(0,i.kt)(g,{cols:2,mdxType:"Gallery"},(0,i.kt)(m,{src:"2d/guides/fog_of_war/smoothness_2.gif",caption:"FogSmoothness set to 2 (gif)",mdxType:"Image"}),(0,i.kt)(m,{src:"2d/guides/fog_of_war/smoothness_10.gif",caption:"FogSmoothness set to 10 (gif)",mdxType:"Image"})),(0,i.kt)("h3",{id:"wave-mode"},"Wave mode"),(0,i.kt)("p",null,"The first available mode is the ",(0,i.kt)("strong",{parentName:"p"},"Wave mode"),". With the ",(0,i.kt)("em",{parentName:"p"},"Wave mode"),", the fog reveals based on the distance of individual tiles from the player. The tiles that are close to the player are revealed sooner than tiles that are far from the player. There are two properties that alter the behaviour when in the ",(0,i.kt)("em",{parentName:"p"},"Wave mode"),": "),(0,i.kt)("h4",{id:"wavespeed"},(0,i.kt)("inlineCode",{parentName:"h4"},"WaveSpeed")),(0,i.kt)("p",null,"The ",(0,i.kt)("strong",{parentName:"p"},"WaveSpeed")," property specifies how fast the wave moves and its unit is ",(0,i.kt)("em",{parentName:"p"},"tiles per second"),". If we have a tile that is 15 tiles away from the player and the wave speed is set to 5, then the tile will be fully revealed 15/5 = 3 seconds after triggering the fog of war script."),(0,i.kt)(g,{cols:2,mdxType:"Gallery"},(0,i.kt)(m,{src:"2d/guides/fog_of_war/wave_speed_10.gif",caption:"WaveSpeed set to 10 tiles per second (gif)",mdxType:"Image"}),(0,i.kt)(m,{src:"2d/guides/fog_of_war/wave_speed_1.gif",caption:"WaveSpeed set to 1 tile per second (gif)",mdxType:"Image"})),(0,i.kt)("h4",{id:"waverevealthreshold"},(0,i.kt)("inlineCode",{parentName:"h4"},"WaveRevealThreshold")),(0,i.kt)("p",null,"The ",(0,i.kt)("strong",{parentName:"p"},"WaveRevealThreshold")," property specifies for how long is a tile completely hidden before it starts to be revealed. If we set the value to 0, all tiles to be revealed will immediately have non-zero opacity. The tiles that are close to the player will quickly have their full opacity while it will take longer for tiles that are far away. If we set the value to 0.5 and have a tile that should be fully revealed after 3 seconds, it will make the tile completely hidden for 0.5 * 3 = 1.5 seconds and only after that the opacity will be increased, and the tile will be fully revealed after additional 1.5 seconds. As a result, it will look like the wave actually moves."),(0,i.kt)(g,{cols:2,mdxType:"Gallery"},(0,i.kt)(m,{src:"2d/guides/fog_of_war/wave_reveal_threshold_0.gif",caption:"WaveRevealThreshold set to 0 (gif)",mdxType:"Image"}),(0,i.kt)(m,{src:"2d/guides/fog_of_war/wave_reveal_threshold_0_5.gif",caption:"WaveRevealThreshold set to 0.5 (gif)",mdxType:"Image"})),(0,i.kt)("blockquote",null,(0,i.kt)("p",{parentName:"blockquote"},(0,i.kt)("strong",{parentName:"p"},"Note:")," It is not recommended to use values higher than 0.5 in the combination with the Smooth transition mode.")),(0,i.kt)("h3",{id:"fade-in-mode"},"Fade In mode"),(0,i.kt)("p",null,"The second available mode is the ",(0,i.kt)("strong",{parentName:"p"},"Fade In mode"),". With the ",(0,i.kt)("em",{parentName:"p"},"Fade In mode"),", all the tiles are revealed at the same time no matter the distance from the player. There is a single property that alters the behaviour of this mode:"),(0,i.kt)("h4",{id:"fadeinduration"},(0,i.kt)("inlineCode",{parentName:"h4"},"FadeInDuration")),(0,i.kt)("p",null,"The ",(0,i.kt)("strong",{parentName:"p"},"FadeInDuration")," property specifies after how many seconds should the tiles be completely revealed. Value ",(0,i.kt)("em",{parentName:"p"},"0")," means that all the tiles will be revealed immediately after triggering the fog of war script."),(0,i.kt)(g,{cols:2,mdxType:"Gallery"},(0,i.kt)(m,{src:"2d/guides/fog_of_war/fade_in_duration_0.gif",caption:"FadeInDuration set to 0 seconds (gif)",mdxType:"Image"}),(0,i.kt)(m,{src:"2d/guides/fog_of_war/fade_in_duration_5.gif",caption:"FadeInDuration set to 5 seconds (gif)",mdxType:"Image"})),(0,i.kt)("h3",{id:"reveal-corridors"},"Reveal Corridors"),(0,i.kt)("p",null,"The ",(0,i.kt)("strong",{parentName:"p"},"RevealCorridors")," checkbox specifies whether you want to reveal some tiles from neighbouring corridor rooms even though the rooms themselves are not yet fully revealed. The main purpose of this feature is aesthetics - in my opinion, the game looks better when the corridor tiles gradually fade away. There are two properties that alter the behaviour of this feature:"),(0,i.kt)(g,{cols:2,mdxType:"Gallery"},(0,i.kt)(m,{src:"2d/guides/fog_of_war/reveal_corridors_enabled.png",caption:"RevealCorridors enabled",mdxType:"Image"}),(0,i.kt)(m,{src:"2d/guides/fog_of_war/reveal_corridors_disabled.png",caption:"RevealCorridors disabled",mdxType:"Image"})),(0,i.kt)("h4",{id:"revealcorridorstiles"},(0,i.kt)("inlineCode",{parentName:"h4"},"RevealCorridorsTiles")),(0,i.kt)("p",null,"The ",(0,i.kt)("strong",{parentName:"p"},"RevealCorridorsTiles")," property specifies how many tiles of each corridor should be revealed. To be more precise, it specifies the maximum distance of tiles (counted from the doors of the corridor) that should be revealed."),(0,i.kt)(g,{cols:2,mdxType:"Gallery"},(0,i.kt)(m,{src:"2d/guides/fog_of_war/reveal_corridors_tiles_1.png",caption:"RevealCorridorsTiles set to 1",mdxType:"Image"}),(0,i.kt)(m,{src:"2d/guides/fog_of_war/reveal_corridors_tiles_3.png",caption:"RevealCorridorsTiles set to 3",mdxType:"Image"})),(0,i.kt)("h4",{id:"revealcorridorsgradually"},(0,i.kt)("inlineCode",{parentName:"h4"},"RevealCorridorsGradually")),(0,i.kt)("p",null,"The ",(0,i.kt)("strong",{parentName:"p"},"RevealCorridorsGradually")," property specifies whether corridor tiles should gradually fade out until they are barely visible."),(0,i.kt)(g,{cols:2,mdxType:"Gallery"},(0,i.kt)(m,{src:"2d/guides/fog_of_war/reveal_corridors_gradually_enabled.png",caption:"RevealCorridorsGradually enabled",mdxType:"Image"}),(0,i.kt)(m,{src:"2d/guides/fog_of_war/reveal_corridors_gradually_disabled.png",caption:"RevealCorridorsGradually disabled",mdxType:"Image"})),(0,i.kt)("h3",{id:"initial-fog-transparency"},"Initial fog transparency"),(0,i.kt)("p",null,"The ",(0,i.kt)("strong",{parentName:"p"},"InitialFogTransparency")," property controls the initial transparency of the fog. The valid range for this parameter is ",(0,i.kt)("em",{parentName:"p"},"[0,1]"),". By default, this value is set to ",(0,i.kt)("em",{parentName:"p"},"0"),", which means that tiles that are covered in fog are completely hidden. "),(0,i.kt)(g,{cols:2,mdxType:"Gallery"},(0,i.kt)(m,{src:"2d/guides/fog_of_war/transparency_0.png",caption:"InitialFogTransparency set to 0",mdxType:"Image"}),(0,i.kt)(m,{src:"2d/guides/fog_of_war/transparency_0_15.png",caption:"InitialFogTransparency set to 0.15",mdxType:"Image"})),(0,i.kt)("h2",{id:"implementation-and-performance"},"Implementation and performance"),(0,i.kt)("p",null,"The Fog of War is implemented as an Image Effect (built-in render pipeline) or a Render Feature (URP). The script keeps track of which tiles should be revealed and passes this information to a shader via a texture where each pixel represents a single tile in the level. Based on the colour of individual pixels, the shader lerps between the actual tile graphics and the fog colour. All transitions are done on a CPU - when the fog changes, an updated texture is sent to the GPU."),(0,i.kt)("p",null,"The main performance bottleneck is when some revealed tiles do not fit in the fog texture. The script starts with a 1x1 texture and when the first room is revealed, it creates a new texture that is large enough to fit all the revealed tiles (plus a bit extra to make sure that we do not have to do this too often). This process repeats every time the texture is too small. The problem is that as the texture gets larger, more CPU time is needed to create the texture and upload it to the GPU. I did a simple benchmark, and it looks like the performance is good enough for levels with up to 250k tiles (i.e. the bounding box of the level is approx. 500x500 tiles). For larger levels, the game freezes for a while when the texture is recreated. However, the average level generated by the plugin should be much smaller than that, so the performance of the game should not be affected by the Fog of War script."),(0,i.kt)("blockquote",null,(0,i.kt)("p",{parentName:"blockquote"},(0,i.kt)("strong",{parentName:"p"},"Note:")," If you have problems with the performance of the Fog of War script, please create a Github issue.")),(0,i.kt)("p",null,"If you want to have the minimum possible performance footprint, do not use any animated transitions. Use the ",(0,i.kt)("em",{parentName:"p"},"Fade In")," mode and set the ",(0,i.kt)("em",{parentName:"p"},"Fade In Duration")," to ",(0,i.kt)("em",{parentName:"p"},"0 seconds"),". With this configuration, the script will compute something only in the frame when a room is revealed."),(0,i.kt)("h2",{id:"advanced"},"Advanced"),(0,i.kt)("h3",{id:"serialization"},"Serialization"),(0,i.kt)("p",null,"TBD."))}k.isMDXComponent=!0}}]);