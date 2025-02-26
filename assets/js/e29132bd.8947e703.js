"use strict";(self.webpackChunkmy_website=self.webpackChunkmy_website||[]).push([[78],{603905:function(e,t,o){o.d(t,{Zo:function(){return c},kt:function(){return u}});var r=o(667294);function n(e,t,o){return t in e?Object.defineProperty(e,t,{value:o,enumerable:!0,configurable:!0,writable:!0}):e[t]=o,e}function i(e,t){var o=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),o.push.apply(o,r)}return o}function a(e){for(var t=1;t<arguments.length;t++){var o=null!=arguments[t]?arguments[t]:{};t%2?i(Object(o),!0).forEach((function(t){n(e,t,o[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(o)):i(Object(o)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(o,t))}))}return e}function s(e,t){if(null==e)return{};var o,r,n=function(e,t){if(null==e)return{};var o,r,n={},i=Object.keys(e);for(r=0;r<i.length;r++)o=i[r],t.indexOf(o)>=0||(n[o]=e[o]);return n}(e,t);if(Object.getOwnPropertySymbols){var i=Object.getOwnPropertySymbols(e);for(r=0;r<i.length;r++)o=i[r],t.indexOf(o)>=0||Object.prototype.propertyIsEnumerable.call(e,o)&&(n[o]=e[o])}return n}var l=r.createContext({}),p=function(e){var t=r.useContext(l),o=t;return e&&(o="function"==typeof e?e(t):a(a({},t),e)),o},c=function(e){var t=p(e.components);return r.createElement(l.Provider,{value:t},e.children)},d={inlineCode:"code",wrapper:function(e){var t=e.children;return r.createElement(r.Fragment,{},t)}},m=r.forwardRef((function(e,t){var o=e.components,n=e.mdxType,i=e.originalType,l=e.parentName,c=s(e,["components","mdxType","originalType","parentName"]),m=p(o),u=n,h=m["".concat(l,".").concat(u)]||m[u]||d[u]||i;return o?r.createElement(h,a(a({ref:t},c),{},{components:o})):r.createElement(h,a({ref:t},c))}));function u(e,t){var o=arguments,n=t&&t.mdxType;if("string"==typeof e||n){var i=o.length,a=new Array(i);a[0]=m;var s={};for(var l in t)hasOwnProperty.call(t,l)&&(s[l]=t[l]);s.originalType=e,s.mdxType="string"==typeof e?e:n,a[1]=s;for(var p=2;p<i;p++)a[p]=o[p];return r.createElement.apply(null,a)}return r.createElement.apply(null,o)}m.displayName="MDXCreateElement"},351387:function(e,t,o){o.r(t),o.d(t,{frontMatter:function(){return s},contentTitle:function(){return l},metadata:function(){return p},toc:function(){return c},default:function(){return v}});var r=o(487462),n=o(263366),i=(o(667294),o(603905)),a=["components"],s={title:"Post-processing"},l=void 0,p={unversionedId:"generators/post-process",id:"version-2.0.6/generators/post-process",title:"Post-processing",description:"After a level is generated, we may often want to run some additional logic like spawning enemies, etc. This can be achieved by providing your own post-processing logic that will be called after the level is generated and provided with information about the level.",source:"@site/versioned_docs/version-2.0.6/generators/post-process.md",sourceDirName:"generators",slug:"/generators/post-process",permalink:"/Edgar-Unity/docs/generators/post-process",editUrl:"https://github.com/OndrejNepozitek/Edgar-Unity/tree/docusaurus/versioned_docs/version-2.0.6/generators/post-process.md",tags:[],version:"2.0.6",frontMatter:{title:"Post-processing"},sidebar:"docs",previous:{title:"Dungeon generator",permalink:"/Edgar-Unity/docs/generators/dungeon-generator"},next:{title:"(PRO) Platformer generator",permalink:"/Edgar-Unity/docs/generators/platformer-generator"}},c=[{value:"Built-in post-processing steps",id:"built-in-post-processing-steps",children:[{value:"0. Instantiate room template with correct positions",id:"0-instantiate-room-template-with-correct-positions",children:[],level:4},{value:"1. Initialize shared tilemaps",id:"1-initialize-shared-tilemaps",children:[],level:4},{value:"2. Copy rooms to shared tilemaps",id:"2-copy-rooms-to-shared-tilemaps",children:[],level:4},{value:"3. Center grid",id:"3-center-grid",children:[],level:4},{value:"4. Disable room template renderers",id:"4-disable-room-template-renderers",children:[],level:4},{value:"5. Disable room template colliders",id:"5-disable-room-template-colliders",children:[],level:4}],level:2},{value:"Custom post-processing",id:"custom-post-processing",children:[{value:"Using a <code>MonoBehaviour</code> component",id:"using-a-monobehaviour-component",children:[],level:3},{value:"Using a <code>ScriptableObject</code>",id:"using-a-scriptableobject",children:[],level:3}],level:2}],d=function(e){return function(t){return console.warn("Component "+e+" was not imported, exported, or provided by MDXProvider as global scope"),(0,i.kt)("div",t)}},m=d("ExternalCode"),u=d("Path"),h=d("Image"),g=d("FeatureUsage"),b={toc:c};function v(e){var t=e.components,o=(0,n.Z)(e,a);return(0,i.kt)("wrapper",(0,r.Z)({},b,o,{components:t,mdxType:"MDXLayout"}),(0,i.kt)("p",null,"After a level is generated, we may often want to run some additional logic like spawning enemies, etc. This can be achieved by providing your own post-processing logic that will be called after the level is generated and provided with information about the level. "),(0,i.kt)("p",null,"To better understand how the generator works, we will first describe which post-processing is done by the generator itself and then provide ways to extend this behaviour and provide your own logic. You can skip right to ",(0,i.kt)("a",{parentName:"p",href:"/Edgar-Unity/docs/generators/post-process#custom-post-processing"},"Custom post-processing")," if that is what you are looking for."),(0,i.kt)("h2",{id:"built-in-post-processing-steps"},"Built-in post-processing steps"),(0,i.kt)("h4",{id:"0-instantiate-room-template-with-correct-positions"},"0. Instantiate room template with correct positions"),(0,i.kt)("p",null,"This is not actually a post-processing as it happens in the generator stage of the whole generator and cannot be disabled. At this point, the generator knows the final position and room template for each room in the level. The generator goes through the rooms and instantiates the corresponding room template game object and moves it to its correct position. If we disabled all the other post-processing steps, we would get a bunch of correctly positioned rooms, but there would often be weird overlap between the rooms."),(0,i.kt)("h4",{id:"1-initialize-shared-tilemaps"},"1. Initialize shared tilemaps"),(0,i.kt)("p",null,"In this step, the generator initializes the structure of shared tilemaps to which we will copy individual rooms in the next step. These tilemaps will contain all the tiles in the level. If you provided your own ",(0,i.kt)("em",{parentName:"p"},"Tilemap Layers Handler"),", this is the time it gets called."),(0,i.kt)("h4",{id:"2-copy-rooms-to-shared-tilemaps"},"2. Copy rooms to shared tilemaps"),(0,i.kt)("p",null,"In this step, the generator copies individual rooms to shared tilemaps. If we use corridors, it is important to first copy other rooms and only then corridors. By doing so, corridors will make holes into the walls of other rooms, so we can go from one room to another."),(0,i.kt)("h4",{id:"3-center-grid"},"3. Center grid"),(0,i.kt)("p",null,"In this step, the whole level is moved in a way that its centre ends up at (0,0). This step is only used to make it easier to go through multiple generated levels without having to move the camera around."),(0,i.kt)("h4",{id:"4-disable-room-template-renderers"},"4. Disable room template renderers"),(0,i.kt)("p",null,"At this point, we display both tiles from shared tilemaps and tiles from individual room template game objects that we instantiated in the step 0. Therefore, we have to disable all tilemap renderers from individual room templates. "),(0,i.kt)("p",null,"You may think why do not we just disable the whole room template. The reason for that is that there may be some additional game objects like lights, enemies, etc. and we do not want to lose that."),(0,i.kt)("h4",{id:"5-disable-room-template-colliders"},"5. Disable room template colliders"),(0,i.kt)("p",null,"The last step is very similar to the previous step. At this point, there are colliders from individual room templates that would prevent us from going from one room to another. We keep only the colliders that are set to trigger because these may be useful for example for ",(0,i.kt)("a",{parentName:"p",href:"/Edgar-Unity/docs/guides/current-room-detection"},"Current room detection"),"."),(0,i.kt)("h2",{id:"custom-post-processing"},"Custom post-processing"),(0,i.kt)("p",null,"There are currently 2 ways of implementing custom processing-logic: you either implement a custom component or use a ",(0,i.kt)("em",{parentName:"p"},"ScriptableObject"),". I would recommend starting with a custom component as it is a bit easier, any only use ",(0,i.kt)("em",{parentName:"p"},"ScriptableObjects")," if you need some of their benefits."),(0,i.kt)("blockquote",null,(0,i.kt)("p",{parentName:"blockquote"},(0,i.kt)("strong",{parentName:"p"},"Note:")," Previously (before ",(0,i.kt)("em",{parentName:"p"},"v2.0.0-beta.0"),"), it was only possible to create custom post-processing logic using ",(0,i.kt)("em",{parentName:"p"},"ScriptableObjects"),". But that process is relatively tedious: you have to add the ",(0,i.kt)("inlineCode",{parentName:"p"},"CreateAssetMenu")," attribute (which I never remember) and then create an instance of that ",(0,i.kt)("em",{parentName:"p"},"ScriptableObject"),". Therefore, I also made it possible to use a ",(0,i.kt)("inlineCode",{parentName:"p"},"MonoBehaviour")," component and just attach it to the generator game object.")),(0,i.kt)("h3",{id:"using-a-monobehaviour-component"},"Using a ",(0,i.kt)("inlineCode",{parentName:"h3"},"MonoBehaviour")," component"),(0,i.kt)("p",null,"The first approach is to create a class that inherits from the ",(0,i.kt)("inlineCode",{parentName:"p"},"DungeonGeneratorPostProcessingComponentGrid2D")," class (which in turn inherits from ",(0,i.kt)("inlineCode",{parentName:"p"},"MonoBehaviour"),"). This class expects you to override the ",(0,i.kt)("inlineCode",{parentName:"p"},"void Run(DungeonGeneratorLevelGrid2D level)")," method where you should place your post-processing logic."),(0,i.kt)(m,{name:"2d_customPostProcessingComponent",mdxType:"ExternalCode"}),(0,i.kt)("p",null,"When you have your implementation ready, go to the scene where you have the generator and attach this component to the game object with the generator. If you now run the generator, your post-processing code should be called."),(0,i.kt)("h3",{id:"using-a-scriptableobject"},"Using a ",(0,i.kt)("inlineCode",{parentName:"h3"},"ScriptableObject")),(0,i.kt)("p",null,"The second approach is to create a class that inherits from ",(0,i.kt)("inlineCode",{parentName:"p"},"DungeonGeneratorPostProcessingGrid2D")," (which in turn inherits from ",(0,i.kt)("inlineCode",{parentName:"p"},"ScriptableObject"),"). And because the base class is a ",(0,i.kt)("em",{parentName:"p"},"ScriptableObject"),", you need to add the ",(0,i.kt)("inlineCode",{parentName:"p"},"CreateAssetMenu")," attribute, so you are able to create an instance of that ",(0,i.kt)("em",{parentName:"p"},"ScriptableObject"),". This class expects you to override the ",(0,i.kt)("inlineCode",{parentName:"p"},"void Run(DungeonGeneratorLevelGrid2D level)")," method where you should place your post-processing logic."),(0,i.kt)(m,{name:"2d_customPostProcessing",mdxType:"ExternalCode"}),(0,i.kt)("p",null,"When you have your implementation ready, you now have to create an instance of that ScriptableObject by right-clicking in the project view and then ",(0,i.kt)(u,{path:"2d:Examples/Docs/My custom post-processing",mdxType:"Path"}),". And the last step is to drag and drop this ScriptableObject in the ",(0,i.kt)("em",{parentName:"p"},"Custom post process tasks")," section of the dungeon generator."),(0,i.kt)(h,{src:"2d/examples/example1/custom_post_process.png",caption:"Add the ScriptableObject to the Custom post process tasks array",mdxType:"Image"}),(0,i.kt)(g,{id:"custom-post-processing",mdxType:"FeatureUsage"}))}v.isMDXComponent=!0}}]);