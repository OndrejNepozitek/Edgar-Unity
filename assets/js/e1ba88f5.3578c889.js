"use strict";(self.webpackChunkmy_website=self.webpackChunkmy_website||[]).push([[7157],{603905:function(e,t,r){r.d(t,{Zo:function(){return m},kt:function(){return u}});var n=r(667294);function o(e,t,r){return t in e?Object.defineProperty(e,t,{value:r,enumerable:!0,configurable:!0,writable:!0}):e[t]=r,e}function a(e,t){var r=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),r.push.apply(r,n)}return r}function i(e){for(var t=1;t<arguments.length;t++){var r=null!=arguments[t]?arguments[t]:{};t%2?a(Object(r),!0).forEach((function(t){o(e,t,r[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(r)):a(Object(r)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(r,t))}))}return e}function l(e,t){if(null==e)return{};var r,n,o=function(e,t){if(null==e)return{};var r,n,o={},a=Object.keys(e);for(n=0;n<a.length;n++)r=a[n],t.indexOf(r)>=0||(o[r]=e[r]);return o}(e,t);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);for(n=0;n<a.length;n++)r=a[n],t.indexOf(r)>=0||Object.prototype.propertyIsEnumerable.call(e,r)&&(o[r]=e[r])}return o}var s=n.createContext({}),p=function(e){var t=n.useContext(s),r=t;return e&&(r="function"==typeof e?e(t):i(i({},t),e)),r},m=function(e){var t=p(e.components);return n.createElement(s.Provider,{value:t},e.children)},d={inlineCode:"code",wrapper:function(e){var t=e.children;return n.createElement(n.Fragment,{},t)}},c=n.forwardRef((function(e,t){var r=e.components,o=e.mdxType,a=e.originalType,s=e.parentName,m=l(e,["components","mdxType","originalType","parentName"]),c=p(r),u=o,g=c["".concat(s,".").concat(u)]||c[u]||d[u]||a;return r?n.createElement(g,i(i({ref:t},m),{},{components:r})):n.createElement(g,i({ref:t},m))}));function u(e,t){var r=arguments,o=t&&t.mdxType;if("string"==typeof e||o){var a=r.length,i=new Array(a);i[0]=c;var l={};for(var s in t)hasOwnProperty.call(t,s)&&(l[s]=t[s]);l.originalType=e,l.mdxType="string"==typeof e?e:o,i[1]=l;for(var p=2;p<a;p++)i[p]=r[p];return n.createElement.apply(null,i)}return n.createElement.apply(null,r)}c.displayName="MDXCreateElement"},614850:function(e,t,r){r.r(t),r.d(t,{frontMatter:function(){return l},contentTitle:function(){return s},metadata:function(){return p},toc:function(){return m},default:function(){return h}});var n=r(487462),o=r(263366),a=(r(667294),r(603905)),i=["components"],l={title:"Dungeon generator"},s=void 0,p={unversionedId:"3d/generators/dungeon-generator",id:"version-2.0.6/3d/generators/dungeon-generator",title:"Dungeon generator",description:"Minimal setup",source:"@site/versioned_docs/version-2.0.6/3d/generators/dungeon-generator.md",sourceDirName:"3d/generators",slug:"/3d/generators/dungeon-generator",permalink:"/Edgar-Unity/docs/3d/generators/dungeon-generator",editUrl:"https://github.com/OndrejNepozitek/Edgar-Unity/tree/docusaurus/versioned_docs/version-2.0.6/3d/generators/dungeon-generator.md",tags:[],version:"2.0.6",frontMatter:{title:"Dungeon generator"},sidebar:"3d",previous:{title:"Performance tips",permalink:"/Edgar-Unity/docs/3d/basics/performance-tips"},next:{title:"Post-processing",permalink:"/Edgar-Unity/docs/3d/generators/post-processing"}},m=[{value:"Minimal setup",id:"minimal-setup",children:[],level:2},{value:"Configuration",id:"configuration",children:[{value:"Input config (<code>FixedLevelGraphConfigGrid3D</code>)",id:"input-config",children:[],level:4},{value:"Generator config (<code>DungeonGeneratorConfigGrid3D</code>)",id:"generator-config",children:[],level:4},{value:"Post-processing config (<code>PostProcessingConfigGrid3D</code>)",id:"post-processing-config",children:[],level:4},{value:"Other config (available directly on the generator class)",id:"other-config",children:[],level:4},{value:"Change the configuration from a script",id:"change-the-configuration-from-a-script",children:[],level:3}],level:2},{value:"Call the generator from a script",id:"call-the-generator-from-a-script",children:[],level:2}],d=function(e){return function(t){return console.warn("Component "+e+" was not imported, exported, or provided by MDXProvider as global scope"),(0,a.kt)("div",t)}},c=d("Image"),u=d("Difference2D3D"),g={toc:m};function h(e){var t=e.components,r=(0,o.Z)(e,i);return(0,a.kt)("wrapper",(0,n.Z)({},g,r,{components:t,mdxType:"MDXLayout"}),(0,a.kt)("h2",{id:"minimal-setup"},"Minimal setup"),(0,a.kt)("ol",null,(0,a.kt)("li",{parentName:"ol"},"Create an empty game object in the scene (name it however you like, I usually use ",(0,a.kt)("em",{parentName:"li"},"Dungeon Generator"),")"),(0,a.kt)("li",{parentName:"ol"},"Add the ",(0,a.kt)("strong",{parentName:"li"},"Dungeon Generator (Gid3D)")," component to that game object"),(0,a.kt)("li",{parentName:"ol"},"Assign your ",(0,a.kt)("em",{parentName:"li"},"GeneratorSettings")," to the ",(0,a.kt)("strong",{parentName:"li"},"Generator Settings")," field"),(0,a.kt)("li",{parentName:"ol"},"Assign your level graph to the ",(0,a.kt)("strong",{parentName:"li"},"Level Graph")," field"),(0,a.kt)("li",{parentName:"ol"},"Hit the ",(0,a.kt)("strong",{parentName:"li"},"Generate dungeon")," button or enable ",(0,a.kt)("strong",{parentName:"li"},"Generate on start")," and enter play mode")),(0,a.kt)(c,{src:"3d/generator_setup/component.png",caption:"Dungeon generator component",width:"500px",mdxType:"Image"}),(0,a.kt)("h2",{id:"configuration"},"Configuration"),(0,a.kt)("h4",{id:"input-config"},"Input config (",(0,a.kt)("inlineCode",{parentName:"h4"},"FixedLevelGraphConfigGrid3D"),")"),(0,a.kt)("ul",null,(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Level Graph")," - Level graph that should be used. Must not be null."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Use Corridors")," - Whether corridors should be used between neighbouring rooms. If enabled, corridor room templates must be provided in the level graph."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Allow Rotation Override")," - Use this field to override whether room templates can be rotated in generated levels."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Fix Elevations Inside Cycles")," - How to handle level graph cycles and room templates with different door elevations. Find out more ",(0,a.kt)("a",{parentName:"li",href:"/Edgar-Unity/docs/3d/guides/different-elevations#option-1-avoid-elevation-changes-inside-cycles"},"here"),".")),(0,a.kt)("h4",{id:"generator-config"},"Generator config (",(0,a.kt)("inlineCode",{parentName:"h4"},"DungeonGeneratorConfigGrid3D"),")"),(0,a.kt)("ul",null,(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Root Game Object")," - Game Object to which the generated level will be attached. New Game Object will be created if null."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Timeout")," - How long (in milliseconds) should we wait for the algorithm to generate a level. We may sometimes create an input that is too hard for the algorithm, so it is good to stop after some amount of time with an error."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Repeat Mode Override")," - Whether to override the repeat mode of individual room templates.",(0,a.kt)("ul",{parentName:"li"},(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"No override")," - Nothing is overridden, keep repeat modes from room templates."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Allow repeat")," - All room templates may repeat in generated levels."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"No immediate")," - Neighbouring room must have different room templates."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"No repeat")," - All rooms must have different room templates.")))),(0,a.kt)("blockquote",null,(0,a.kt)("p",{parentName:"blockquote"},(0,a.kt)("strong",{parentName:"p"},"Note:")," If you provide too few room templates, they may repeat in generated levels even if you choose the ",(0,a.kt)("strong",{parentName:"p"},"No immediate")," or ",(0,a.kt)("strong",{parentName:"p"},"No repeat")," options. To make sure that the repeat mode is satisfied, please provide enough room templates to choose from.")),(0,a.kt)("ul",null,(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Minimum Room Distance")," - The minimum distance between non-neighbouring rooms.",(0,a.kt)("ul",{parentName:"li"},(0,a.kt)("li",{parentName:"ul"},"If equal to ",(0,a.kt)("strong",{parentName:"li"},"0")," - walls from one room can occupy the same tiles as walls from a different room."),(0,a.kt)("li",{parentName:"ul"},"If equal to ",(0,a.kt)("strong",{parentName:"li"},"1")," (default) - walls from different rooms can be next to each other but not on top of each other."),(0,a.kt)("li",{parentName:"ul"},"If equal to ",(0,a.kt)("strong",{parentName:"li"},"2")," - there must be at least one empty tile between walls of different rooms. (This is good for when using rule tiles and weird things are happening.)")))),(0,a.kt)("blockquote",null,(0,a.kt)("p",{parentName:"blockquote"},(0,a.kt)("strong",{parentName:"p"},"Note:")," Higher values of ",(0,a.kt)("em",{parentName:"p"},"Minimum Room Distance")," may negatively affect the performance of the generator. Moreover, with very short corridor, it might even be impossible to generate a level with a high value of this parameter.")),(0,a.kt)("ul",null,(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Generator Settings")," - Instance of the ",(0,a.kt)("em",{parentName:"li"},"GeneratorSettings")," scriptable object.")),(0,a.kt)("h4",{id:"post-processing-config"},"Post-processing config (",(0,a.kt)("inlineCode",{parentName:"h4"},"PostProcessingConfigGrid3D"),")"),(0,a.kt)("ul",null,(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Center Level")," - Whether to move the level so that its centre is approximately at (0,0). Useful for debugging in the Scene view inside the editor."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Process Connectors And Blockers")," - Whether door connectors and blockers should be added to the level"),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Add Connectors")," - Which door connectors should be added. For every single door in the level, there are room templates on both sides of the door, which means that there possible 2 different door connectors that can be added. This dropdown controls which of those connectors are added to the level:",(0,a.kt)("ul",{parentName:"li"},(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Never")," - No door connectors are added"),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"RoomsOnly")," - Only connectors belonging to non-corridor rooms are used"),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"CorridorsOnly")," - Only connectors belonging to corridor rooms are used"),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"RoomsAndCorridors")," - All connectors are added"),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"PreferCorridors")," - Corridor connectors are added unless there is no corridor between neighbouring rooms, in which case room connectors are used.")))),(0,a.kt)("h4",{id:"other-config"},"Other config (available directly on the generator class)"),(0,a.kt)("ul",null,(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Use Random Seed")," - Whether to use a random seed for each new generated level. "),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Random Generator Seed")," - Random generator seed that will be used when ",(0,a.kt)("strong",{parentName:"li"},"Use Random Seed")," is disabled. Useful for debugging."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Generate On Start")," - Whether to generate a new level when play mode is entered.")),(0,a.kt)("h3",{id:"change-the-configuration-from-a-script"},"Change the configuration from a script"),(0,a.kt)("p",null,"You can also easily change the configuration of the generator directly from a script as all the fields are exposed on the ",(0,a.kt)("em",{parentName:"p"},"DungeonGeneratorGrid3D")," components."),(0,a.kt)("h2",{id:"call-the-generator-from-a-script"},"Call the generator from a script"),(0,a.kt)("blockquote",null,(0,a.kt)("p",{parentName:"blockquote"},(0,a.kt)("strong",{parentName:"p"},"Note:")," This part of the guide lives in the 2D section of the documentation: ",(0,a.kt)("a",{parentName:"p",href:"/Edgar-Unity/docs/generators/dungeon-generator#call-the-generator-from-a-script"},"here")),(0,a.kt)("p",{parentName:"blockquote"},"The concepts are the same in the 2D version, you just have to replace the ",(0,a.kt)("inlineCode",{parentName:"p"},"Grid2D")," class name suffix with ",(0,a.kt)("inlineCode",{parentName:"p"},"Grid3D"),". For example, ",(0,a.kt)("inlineCode",{parentName:"p"},"DungeonGeneratorGrid2D")," becomes ",(0,a.kt)("inlineCode",{parentName:"p"},"DungeonGeneratorGrid3D"),".")),(0,a.kt)(u,{mdxType:"Difference2D3D"}))}h.isMDXComponent=!0}}]);