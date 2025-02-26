"use strict";(self.webpackChunkmy_website=self.webpackChunkmy_website||[]).push([[1825],{603905:function(e,t,r){r.d(t,{Zo:function(){return c},kt:function(){return h}});var o=r(667294);function n(e,t,r){return t in e?Object.defineProperty(e,t,{value:r,enumerable:!0,configurable:!0,writable:!0}):e[t]=r,e}function a(e,t){var r=Object.keys(e);if(Object.getOwnPropertySymbols){var o=Object.getOwnPropertySymbols(e);t&&(o=o.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),r.push.apply(r,o)}return r}function i(e){for(var t=1;t<arguments.length;t++){var r=null!=arguments[t]?arguments[t]:{};t%2?a(Object(r),!0).forEach((function(t){n(e,t,r[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(r)):a(Object(r)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(r,t))}))}return e}function s(e,t){if(null==e)return{};var r,o,n=function(e,t){if(null==e)return{};var r,o,n={},a=Object.keys(e);for(o=0;o<a.length;o++)r=a[o],t.indexOf(r)>=0||(n[r]=e[r]);return n}(e,t);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);for(o=0;o<a.length;o++)r=a[o],t.indexOf(r)>=0||Object.prototype.propertyIsEnumerable.call(e,r)&&(n[r]=e[r])}return n}var l=o.createContext({}),p=function(e){var t=o.useContext(l),r=t;return e&&(r="function"==typeof e?e(t):i(i({},t),e)),r},c=function(e){var t=p(e.components);return o.createElement(l.Provider,{value:t},e.children)},u={inlineCode:"code",wrapper:function(e){var t=e.children;return o.createElement(o.Fragment,{},t)}},m=o.forwardRef((function(e,t){var r=e.components,n=e.mdxType,a=e.originalType,l=e.parentName,c=s(e,["components","mdxType","originalType","parentName"]),m=p(r),h=n,y=m["".concat(l,".").concat(h)]||m[h]||u[h]||a;return r?o.createElement(y,i(i({ref:t},c),{},{components:r})):o.createElement(y,i({ref:t},c))}));function h(e,t){var r=arguments,n=t&&t.mdxType;if("string"==typeof e||n){var a=r.length,i=new Array(a);i[0]=m;var s={};for(var l in t)hasOwnProperty.call(t,l)&&(s[l]=t[l]);s.originalType=e,s.mdxType="string"==typeof e?e:n,i[1]=s;for(var p=2;p<a;p++)i[p]=r[p];return o.createElement.apply(null,i)}return o.createElement.apply(null,r)}m.displayName="MDXCreateElement"},923473:function(e,t,r){r.r(t),r.d(t,{frontMatter:function(){return s},contentTitle:function(){return l},metadata:function(){return p},toc:function(){return c},default:function(){return m}});var o=r(487462),n=r(263366),a=(r(667294),r(603905)),i=["components"],s={title:"Performance tips"},l=void 0,p={unversionedId:"basics/performance-tips",id:"basics/performance-tips",title:"Performance tips",description:"When used correctly, Edgar can generate very complex levels. Unfortunately, it is also relatively simple to prepare an input that is just too difficult for the generator, and you will end up with a TimeoutException. The goal of this page is to provide some performance tips that you can follow to improve the performance of the generator.",source:"@site/docs/basics/performance-tips.md",sourceDirName:"basics",slug:"/basics/performance-tips",permalink:"/Edgar-Unity/docs/next/basics/performance-tips",editUrl:"https://github.com/OndrejNepozitek/Edgar-Unity/tree/docusaurus/docs/basics/performance-tips.md",tags:[],version:"current",frontMatter:{title:"Performance tips"},sidebar:"docs",previous:{title:"Level structure and rooms data",permalink:"/Edgar-Unity/docs/next/basics/generated-level-info"},next:{title:"Dungeon generator",permalink:"/Edgar-Unity/docs/next/generators/dungeon-generator"}},c=[{value:"Room templates",id:"room-templates",children:[],level:2},{value:"Level graphs",id:"level-graphs",children:[],level:2}],u={toc:c};function m(e){var t=e.components,r=(0,n.Z)(e,i);return(0,a.kt)("wrapper",(0,o.Z)({},u,r,{components:t,mdxType:"MDXLayout"}),(0,a.kt)("p",null,"When used correctly, Edgar can generate very complex levels. Unfortunately, it is also relatively simple to prepare an input that is just too difficult for the generator, and you will end up with a ",(0,a.kt)("inlineCode",{parentName:"p"},"TimeoutException"),". The goal of this page is to provide some performance tips that you can follow to improve the performance of the generator."),(0,a.kt)("p",null,"The general idea is that if you make it harder for the generator in one way (e.g. by having many rooms), you should compensate for that in some other way (e.g. by not having cycles in your level graph). Also, I recommend starting simple and only making things more complex when you get the hang of how the generator behaves."),(0,a.kt)("h2",{id:"room-templates"},"Room templates"),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"Try to provide as many door positions as possible.")," I cannot stress enough how important this is. You should aim to use the ",(0,a.kt)("em",{parentName:"p"},"Simple")," or ",(0,a.kt)("em",{parentName:"p"},"Hybrid")," door modes as much as possible, and only use the ",(0,a.kt)("em",{parentName:"p"},"Manual")," door mode when it is absolutely necessary. The only exception is when you are trying to generate levels without cycles, then you can get away with having a relatively small number of door positions. "),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"Make sure that default room templates make sense.")," The easiest way to configure room templates is to add them as ",(0,a.kt)("em",{parentName:"p"},"Default room templates"),", making them available to all rooms in the level graph. However, it is not recommended adding room templates that can be used only in very specific scenarios. For example, if you have a secret room that has exactly one door position, you should not add it to the default list. The reason is that the generator might try to use this room template for a room that has multiple neighbours, wasting precious time. Instead, assign these unique room templates only to the rooms where it makes sense."),(0,a.kt)("h2",{id:"level-graphs"},"Level graphs"),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"Limit the number of rooms.")," The number of rooms in a level graph greatly affects the performance. As a rule of thumb, you should aim to have ",(0,a.kt)("strong",{parentName:"p"},"less than 20 rooms"),". However, if you follow the other performance tips, you can generate levels with up to 40 rooms."),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"Limit the number of cycles.")," It is very hard to generate levels with cycles. Therefore, the number of cycles greatly affects the performance. Usually, you should start with 0-1 cycles and only increase the number when you are already familiar with the core concepts of Edgar. In the ",(0,a.kt)("a",{parentName:"p",href:"/Edgar-Unity/docs/next/examples/enter-the-gungeon#level-graphs"},"Enter the Gungeon")," example, you can see levels graphs with up to 3 cycles and the generator is still relatively fast."),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"Avoid interconnected cycles.")," Cycles are hard, but interconnected cycles are even harder. If you want to have multiple cycles in a level graph, make sure that the cycles do not have any rooms in common. Usually, it should not be too hard to design your level graphs without interconnected cycles. For example, all the level graphs in ",(0,a.kt)("em",{parentName:"p"},"Enter the Gungeon")," have this property, and it does not make the game any worse."))}m.isMDXComponent=!0}}]);