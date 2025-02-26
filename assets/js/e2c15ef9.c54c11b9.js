"use strict";(self.webpackChunkmy_website=self.webpackChunkmy_website||[]).push([[5151],{603905:function(e,t,o){o.d(t,{Zo:function(){return d},kt:function(){return p}});var n=o(667294);function r(e,t,o){return t in e?Object.defineProperty(e,t,{value:o,enumerable:!0,configurable:!0,writable:!0}):e[t]=o,e}function i(e,t){var o=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),o.push.apply(o,n)}return o}function c(e){for(var t=1;t<arguments.length;t++){var o=null!=arguments[t]?arguments[t]:{};t%2?i(Object(o),!0).forEach((function(t){r(e,t,o[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(o)):i(Object(o)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(o,t))}))}return e}function a(e,t){if(null==e)return{};var o,n,r=function(e,t){if(null==e)return{};var o,n,r={},i=Object.keys(e);for(n=0;n<i.length;n++)o=i[n],t.indexOf(o)>=0||(r[o]=e[o]);return r}(e,t);if(Object.getOwnPropertySymbols){var i=Object.getOwnPropertySymbols(e);for(n=0;n<i.length;n++)o=i[n],t.indexOf(o)>=0||Object.prototype.propertyIsEnumerable.call(e,o)&&(r[o]=e[o])}return r}var s=n.createContext({}),l=function(e){var t=n.useContext(s),o=t;return e&&(o="function"==typeof e?e(t):c(c({},t),e)),o},d=function(e){var t=l(e.components);return n.createElement(s.Provider,{value:t},e.children)},u={inlineCode:"code",wrapper:function(e){var t=e.children;return n.createElement(n.Fragment,{},t)}},m=n.forwardRef((function(e,t){var o=e.components,r=e.mdxType,i=e.originalType,s=e.parentName,d=a(e,["components","mdxType","originalType","parentName"]),m=l(o),p=r,f=m["".concat(s,".").concat(p)]||m[p]||u[p]||i;return o?n.createElement(f,c(c({ref:t},d),{},{components:o})):n.createElement(f,c({ref:t},d))}));function p(e,t){var o=arguments,r=t&&t.mdxType;if("string"==typeof e||r){var i=o.length,c=new Array(i);c[0]=m;var a={};for(var s in t)hasOwnProperty.call(t,s)&&(a[s]=t[s]);a.originalType=e,a.mdxType="string"==typeof e?e:r,c[1]=a;for(var l=2;l<i;l++)c[l]=o[l];return n.createElement.apply(null,c)}return n.createElement.apply(null,o)}m.displayName="MDXCreateElement"},889578:function(e,t,o){o.r(t),o.d(t,{frontMatter:function(){return a},contentTitle:function(){return s},metadata:function(){return l},toc:function(){return d},default:function(){return y}});var n=o(487462),r=o(263366),i=(o(667294),o(603905)),c=["components"],a={title:"(PRO) Custom editor controls"},s=void 0,l={unversionedId:"guides/custom-editor-controls",id:"version-2.0.4/guides/custom-editor-controls",title:"(PRO) Custom editor controls",description:"In this guide, we will see how to override the default look of level graph editor rooms and connections in order to add custom icons.",source:"@site/versioned_docs/version-2.0.4/guides/custom-editor-controls.md",sourceDirName:"guides",slug:"/guides/custom-editor-controls",permalink:"/Edgar-Unity/docs/2.0.4/guides/custom-editor-controls",editUrl:"https://github.com/OndrejNepozitek/Edgar-Unity/tree/docusaurus/versioned_docs/version-2.0.4/guides/custom-editor-controls.md",tags:[],version:"2.0.4",frontMatter:{title:"(PRO) Custom editor controls"},sidebar:"docs",previous:{title:"(PRO) Directed level graphs",permalink:"/Edgar-Unity/docs/2.0.4/guides/directed-level-graphs"},next:{title:"FAQ",permalink:"/Edgar-Unity/docs/2.0.4/other/faq"}},d=[{value:"Create custom room type",id:"create-custom-room-type",children:[],level:2},{value:"Create custom room control",id:"create-custom-room-control",children:[],level:2},{value:"Custom connections and connection editors",id:"custom-connections-and-connection-editors",children:[],level:2}],u=function(e){return function(t){return console.warn("Component "+e+" was not imported, exported, or provided by MDXProvider as global scope"),(0,i.kt)("div",t)}},m=u("Image"),p=u("Path"),f=u("ExternalCode"),h={toc:d};function y(e){var t=e.components,o=(0,r.Z)(e,c);return(0,i.kt)("wrapper",(0,n.Z)({},h,o,{components:t,mdxType:"MDXLayout"}),(0,i.kt)("p",null,"In this guide, we will see how to override the default look of level graph editor rooms and connections in order to add custom icons."),(0,i.kt)("blockquote",null,(0,i.kt)("p",{parentName:"blockquote"},(0,i.kt)("strong",{parentName:"p"},"Note:")," This pages is not meant to be an in-depth guide. Please look at the source code of this example for more information.")),(0,i.kt)(m,{src:"2d/guides/custom_editor_controls/result.png",caption:"Result of this guide. Custom icons for rooms and connection.",height:"500",mdxType:"Image"}),(0,i.kt)("blockquote",null,(0,i.kt)("p",{parentName:"blockquote"},(0,i.kt)("strong",{parentName:"p"},"Note:")," All files from this example can be found at ",(0,i.kt)(p,{path:"2de:CustomEditorControls",mdxType:"Path"}),".")),(0,i.kt)("h2",{id:"create-custom-room-type"},"Create custom room type"),(0,i.kt)("p",null,"Create a custom room type as documented ",(0,i.kt)("a",{parentName:"p",href:"/Edgar-Unity/docs/2.0.4/basics/level-graphs#pro-custom-rooms-and-connections"},"here"),". I also created a ",(0,i.kt)("inlineCode",{parentName:"p"},"RoomType")," enum which will control if a room has an icon next to it."),(0,i.kt)(f,{name:"2d_customEditorControls_customRoom",mdxType:"ExternalCode"}),(0,i.kt)("h2",{id:"create-custom-room-control"},"Create custom room control"),(0,i.kt)("p",null,"Create a class that will override the default look of rooms in the level graph editor. First, create a class that inherits from ",(0,i.kt)("inlineCode",{parentName:"p"},"RoomControl"),". Then, add the ",(0,i.kt)("inlineCode",{parentName:"p"},"CustomRoomControl")," attribute and specify for which room should the custom control be used."),(0,i.kt)("p",null,"After that, you can override the default implementation of the ",(0,i.kt)("inlineCode",{parentName:"p"},"RoomControl")," class. In this example, I extended the default implementation with a logic that checks the type of the room and displays an icon if there is any assigned to that room type. You can find more details in the source code."),(0,i.kt)(f,{name:"2d_customEditorControls_roomControl",mdxType:"ExternalCode"}),(0,i.kt)("h2",{id:"custom-connections-and-connection-editors"},"Custom connections and connection editors"),(0,i.kt)("p",null,"The process of creating custom connection editors is exactly the same as for rooms. Just replace the word ",(0,i.kt)("inlineCode",{parentName:"p"},"room")," with ",(0,i.kt)("inlineCode",{parentName:"p"},"connection")," in class names."))}y.isMDXComponent=!0}}]);