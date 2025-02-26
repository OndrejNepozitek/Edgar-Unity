"use strict";(self.webpackChunkmy_website=self.webpackChunkmy_website||[]).push([[8630],{603905:function(e,t,r){r.d(t,{Zo:function(){return d},kt:function(){return u}});var n=r(667294);function o(e,t,r){return t in e?Object.defineProperty(e,t,{value:r,enumerable:!0,configurable:!0,writable:!0}):e[t]=r,e}function a(e,t){var r=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),r.push.apply(r,n)}return r}function l(e){for(var t=1;t<arguments.length;t++){var r=null!=arguments[t]?arguments[t]:{};t%2?a(Object(r),!0).forEach((function(t){o(e,t,r[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(r)):a(Object(r)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(r,t))}))}return e}function i(e,t){if(null==e)return{};var r,n,o=function(e,t){if(null==e)return{};var r,n,o={},a=Object.keys(e);for(n=0;n<a.length;n++)r=a[n],t.indexOf(r)>=0||(o[r]=e[r]);return o}(e,t);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);for(n=0;n<a.length;n++)r=a[n],t.indexOf(r)>=0||Object.prototype.propertyIsEnumerable.call(e,r)&&(o[r]=e[r])}return o}var s=n.createContext({}),p=function(e){var t=n.useContext(s),r=t;return e&&(r="function"==typeof e?e(t):l(l({},t),e)),r},d=function(e){var t=p(e.components);return n.createElement(s.Provider,{value:t},e.children)},m={inlineCode:"code",wrapper:function(e){var t=e.children;return n.createElement(n.Fragment,{},t)}},c=n.forwardRef((function(e,t){var r=e.components,o=e.mdxType,a=e.originalType,s=e.parentName,d=i(e,["components","mdxType","originalType","parentName"]),c=p(r),u=o,g=c["".concat(s,".").concat(u)]||c[u]||m[u]||a;return r?n.createElement(g,l(l({ref:t},d),{},{components:r})):n.createElement(g,l({ref:t},d))}));function u(e,t){var r=arguments,o=t&&t.mdxType;if("string"==typeof e||o){var a=r.length,l=new Array(a);l[0]=c;var i={};for(var s in t)hasOwnProperty.call(t,s)&&(i[s]=t[s]);i.originalType=e,i.mdxType="string"==typeof e?e:o,l[1]=i;for(var p=2;p<a;p++)l[p]=r[p];return n.createElement.apply(null,l)}return n.createElement.apply(null,r)}c.displayName="MDXCreateElement"},337896:function(e,t,r){r.r(t),r.d(t,{frontMatter:function(){return i},contentTitle:function(){return s},metadata:function(){return p},toc:function(){return d},default:function(){return f}});var n=r(487462),o=r(263366),a=(r(667294),r(603905)),l=["components"],i={id:"introduction",title:"Introduction"},s=void 0,p={unversionedId:"introduction",id:"version-2.0.5/introduction",title:"Introduction",description:"This project is a Unity plugin for procedural generation of 2D dungeons and aims to give game designers a complete control over generated levels. It combines procedural generation and handmade room templates to generate levels with a feeling of consistency. Under the hood, the plugin uses my .NET procedural level generator.",source:"@site/versioned_docs/version-2.0.5/introduction.md",sourceDirName:".",slug:"/introduction",permalink:"/Edgar-Unity/docs/2.0.5/introduction",editUrl:"https://github.com/OndrejNepozitek/Edgar-Unity/tree/docusaurus/versioned_docs/version-2.0.5/introduction.md",tags:[],version:"2.0.5",frontMatter:{id:"introduction",title:"Introduction"},sidebar:"docs",next:{title:"Motivation",permalink:"/Edgar-Unity/docs/2.0.5/motivation"}},d=[{value:"Features",id:"features",children:[],level:2},{value:"Limitations",id:"limitations",children:[],level:2},{value:"Workflow",id:"workflow",children:[{value:"1. Draw rooms and corridors",id:"1-draw-rooms-and-corridors",children:[],level:3},{value:"2. Prepare the structure of the level",id:"2-prepare-the-structure-of-the-level",children:[],level:3},{value:"3. Generate levels",id:"3-generate-levels",children:[],level:3}],level:2},{value:"Examples",id:"examples",children:[],level:2},{value:"Terms of use",id:"terms-of-use",children:[],level:2}],m=function(e){return function(t){return console.warn("Component "+e+" was not imported, exported, or provided by MDXProvider as global scope"),(0,a.kt)("div",t)}},c=m("Gallery"),u=m("Image"),g={toc:d};function f(e){var t=e.components,r=(0,o.Z)(e,l);return(0,a.kt)("wrapper",(0,n.Z)({},g,r,{components:t,mdxType:"MDXLayout"}),(0,a.kt)("p",null,"This project is a Unity plugin for procedural generation of 2D dungeons and aims to give game designers a ",(0,a.kt)("strong",{parentName:"p"},"complete control")," over generated levels. It combines procedural generation and ",(0,a.kt)("strong",{parentName:"p"},"handmade room templates")," to generate levels with a ",(0,a.kt)("strong",{parentName:"p"},"feeling of consistency"),". Under the hood, the plugin uses my .NET ",(0,a.kt)("a",{parentName:"p",href:"https://github.com/OndrejNepozitek/ProceduralLevelGenerator"},"procedural level generator"),"."),(0,a.kt)("p",null,"Similar approaches are used in games like ",(0,a.kt)("a",{parentName:"p",href:"https://www.boristhebrave.com/2019/07/28/dungeon-generation-in-enter-the-gungeon/"},(0,a.kt)("strong",{parentName:"a"},"Enter the Gungeon"))," or ",(0,a.kt)("a",{parentName:"p",href:"https://www.indiedb.com/games/dead-cells/news/the-level-design-of-a-procedurally-generated-metroidvania"},(0,a.kt)("strong",{parentName:"a"},"Dead Cells")),"."),(0,a.kt)("h2",{id:"features"},"Features"),(0,a.kt)("ul",null,(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Complete control over the structure of generated level.")," Instead of generating completely random dungeons, you specify how many rooms you want and how they should be connected, and the algorithm generates levels that follow exactly that structure."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Complete control over the look of individual rooms.")," You can draw room templates using Unity built-in Tilemap feature. You can use all available tools (brushes, rule tiles, etc.) to design room templates."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Rooms either directly connected by doors or connected by corridors.")," You can choose to either connect rooms by corridors or directly via doors."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Easy to customize.")," The plugin is ready to be customized and extended."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Supports Unity 2019.4 and newer.")),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Multiple example scenes included."))),(0,a.kt)("h2",{id:"limitations"},"Limitations"),(0,a.kt)("ul",null,(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Some inputs are too hard for the generator.")," You need to follow some guidelines in order to achieve good performance."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Not suitable for large levels.")," The generator usually works best for levels with less than 30 rooms."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("strong",{parentName:"li"},"Not everything can be configured via editor.")," You need to have programming knowledge in order to generate anything non-trivial.")),(0,a.kt)("h2",{id:"workflow"},"Workflow"),(0,a.kt)("h3",{id:"1-draw-rooms-and-corridors"},"1. Draw rooms and corridors"),(0,a.kt)(c,{cols:4,mdxType:"Gallery"},(0,a.kt)(u,{src:"2d/examples/example1/room1.png",mdxType:"Image"}),(0,a.kt)(u,{src:"2d/examples/example1/room2.png",mdxType:"Image"}),(0,a.kt)(u,{src:"2d/examples/example1/intro_spawn.png",mdxType:"Image"}),(0,a.kt)(u,{src:"2d/examples/example1/intro_boss.png",mdxType:"Image"}),(0,a.kt)(u,{src:"2d/examples/example1/intro_corridor_horizontal.png",mdxType:"Image"}),(0,a.kt)(u,{src:"2d/examples/example1/intro_corridor_vertical.png",mdxType:"Image"}),(0,a.kt)(u,{src:"2d/examples/example1/corridor_horizontal2.png",mdxType:"Image"}),(0,a.kt)(u,{src:"2d/examples/example1/corridor_vertical2.png",mdxType:"Image"})),(0,a.kt)("h3",{id:"2-prepare-the-structure-of-the-level"},"2. Prepare the structure of the level"),(0,a.kt)(u,{src:"2d/examples/example1/level_graph2.png",height:500,mdxType:"Image"}),(0,a.kt)("h3",{id:"3-generate-levels"},"3. Generate levels"),(0,a.kt)(c,{cols:4,mdxType:"Gallery"},(0,a.kt)(u,{src:"2d/examples/example1/result_reallife2.png",mdxType:"Image"}),(0,a.kt)(u,{src:"2d/examples/example1/result_reallife3.png",mdxType:"Image"}),(0,a.kt)(u,{src:"2d/examples/example1/result_reallife4.png",mdxType:"Image"}),(0,a.kt)(u,{src:"2d/examples/example1/result_reallife5.png",mdxType:"Image"})),(0,a.kt)("h2",{id:"examples"},"Examples"),(0,a.kt)(c,{mdxType:"Gallery"},(0,a.kt)(u,{src:"2d/examples/example1/result_reallife2.png",caption:"Example 1",mdxType:"Image"}),(0,a.kt)(u,{src:"2d/examples/example1/result_reallife1.png",caption:"Example 1",mdxType:"Image"}),(0,a.kt)(u,{src:"2d/examples/example2/result1.png",caption:"Example 2",mdxType:"Image"}),(0,a.kt)(u,{src:"2d/examples/example2/result_reallife1.png",caption:"Example 2",mdxType:"Image"})),(0,a.kt)("h2",{id:"terms-of-use"},"Terms of use"),(0,a.kt)("p",null,"The plugin can be used in both commercial and non-commercial projects but ",(0,a.kt)("strong",{parentName:"p"},"cannot be redistributed or resold"),". If you want to include this plugin in your own asset, please contact me, and we will figure that out."))}f.isMDXComponent=!0}}]);