"use strict";(self.webpackChunkmy_website=self.webpackChunkmy_website||[]).push([[4735],{603905:function(e,t,o){o.d(t,{Zo:function(){return m},kt:function(){return d}});var r=o(667294);function a(e,t,o){return t in e?Object.defineProperty(e,t,{value:o,enumerable:!0,configurable:!0,writable:!0}):e[t]=o,e}function n(e,t){var o=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),o.push.apply(o,r)}return o}function i(e){for(var t=1;t<arguments.length;t++){var o=null!=arguments[t]?arguments[t]:{};t%2?n(Object(o),!0).forEach((function(t){a(e,t,o[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(o)):n(Object(o)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(o,t))}))}return e}function l(e,t){if(null==e)return{};var o,r,a=function(e,t){if(null==e)return{};var o,r,a={},n=Object.keys(e);for(r=0;r<n.length;r++)o=n[r],t.indexOf(o)>=0||(a[o]=e[o]);return a}(e,t);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);for(r=0;r<n.length;r++)o=n[r],t.indexOf(o)>=0||Object.prototype.propertyIsEnumerable.call(e,o)&&(a[o]=e[o])}return a}var s=r.createContext({}),p=function(e){var t=r.useContext(s),o=t;return e&&(o="function"==typeof e?e(t):i(i({},t),e)),o},m=function(e){var t=p(e.components);return r.createElement(s.Provider,{value:t},e.children)},c={inlineCode:"code",wrapper:function(e){var t=e.children;return r.createElement(r.Fragment,{},t)}},h=r.forwardRef((function(e,t){var o=e.components,a=e.mdxType,n=e.originalType,s=e.parentName,m=l(e,["components","mdxType","originalType","parentName"]),h=p(o),d=a,u=h["".concat(s,".").concat(d)]||h[d]||c[d]||n;return o?r.createElement(u,i(i({ref:t},m),{},{components:o})):r.createElement(u,i({ref:t},m))}));function d(e,t){var o=arguments,a=t&&t.mdxType;if("string"==typeof e||a){var n=o.length,i=new Array(n);i[0]=h;var l={};for(var s in t)hasOwnProperty.call(t,s)&&(l[s]=t[s]);l.originalType=e,l.mdxType="string"==typeof e?e:a,i[1]=l;for(var p=2;p<n;p++)i[p]=o[p];return r.createElement.apply(null,i)}return r.createElement.apply(null,o)}h.displayName="MDXCreateElement"},874037:function(e,t,o){o.r(t),o.d(t,{frontMatter:function(){return l},contentTitle:function(){return s},metadata:function(){return p},toc:function(){return m},default:function(){return v}});var r=o(487462),a=o(263366),n=(o(667294),o(603905)),i=["components"],l={title:"Level graphs"},s=void 0,p={unversionedId:"basics/level-graphs",id:"version-2.0.4/basics/level-graphs",title:"Level graphs",description:"Level graph is an abstraction that lets us control the structure of generated levels.",source:"@site/versioned_docs/version-2.0.4/basics/level-graphs.md",sourceDirName:"basics",slug:"/basics/level-graphs",permalink:"/Edgar-Unity/docs/2.0.4/basics/level-graphs",editUrl:"https://github.com/OndrejNepozitek/Edgar-Unity/tree/docusaurus/versioned_docs/version-2.0.4/basics/level-graphs.md",tags:[],version:"2.0.4",frontMatter:{title:"Level graphs"},sidebar:"docs",previous:{title:"Room templates",permalink:"/Edgar-Unity/docs/2.0.4/basics/room-templates"},next:{title:"Generator setup",permalink:"/Edgar-Unity/docs/2.0.4/basics/generator-setup"}},m=[{value:"Basics",id:"basics",children:[],level:2},{value:"Limitations",id:"limitations",children:[{value:"Planar graphs",id:"planar-graphs",children:[],level:3},{value:"Connected graphs",id:"connected-graphs",children:[],level:3}],level:2},{value:"Creating level graphs",id:"creating-level-graphs",children:[{value:"Graph editor",id:"graph-editor",children:[],level:3}],level:2},{value:"Room templates",id:"room-templates",children:[{value:"Room templates sets",id:"room-templates-sets",children:[],level:3},{value:"Default room templates",id:"default-room-templates",children:[{value:"Room templates",id:"room-templates-1",children:[],level:4},{value:"Room templates sets",id:"room-templates-sets-1",children:[],level:4}],level:3},{value:"Corridor room templates",id:"corridor-room-templates",children:[{value:"Room templates",id:"room-templates-2",children:[],level:4},{value:"Room templates sets",id:"room-templates-sets-2",children:[],level:4}],level:3},{value:"Configuring individual rooms",id:"configuring-individual-rooms",children:[],level:3}],level:2},{value:"(PRO) Custom rooms and connections",id:"pro-custom-rooms-and-connections",children:[{value:"Inherit from Room",id:"inherit-from-room",children:[],level:3},{value:"Inherit from RoomBase",id:"inherit-from-roombase",children:[],level:3},{value:"Configure level graph",id:"configure-level-graph",children:[],level:3},{value:"Accessing room information",id:"accessing-room-information",children:[],level:3},{value:"Custom colours in the level graph editor",id:"custom-colours-in-the-level-graph-editor",children:[],level:3}],level:2},{value:"(PRO) Directed level graphs",id:"pro-directed-level-graphs",children:[],level:2}],c=function(e){return function(t){return console.warn("Component "+e+" was not imported, exported, or provided by MDXProvider as global scope"),(0,n.kt)("div",t)}},h=c("Image"),d=c("Path"),u=c("FeatureUsage"),g={toc:m};function v(e){var t=e.components,o=(0,a.Z)(e,i);return(0,n.kt)("wrapper",(0,r.Z)({},g,o,{components:t,mdxType:"MDXLayout"}),(0,n.kt)("p",null,"Level graph is an abstraction that lets us control the structure of generated levels. "),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," In the context of this plugin, the term ",(0,n.kt)("em",{parentName:"p"},"graph")," is used to refer to a mathematical structure consisting of nodes and edges, not a way to visualize functions.")),(0,n.kt)("h2",{id:"basics"},"Basics"),(0,n.kt)("p",null,"Level graph consists of rooms and room connections. Each room corresponds to a room in a generated level and each connection tells the algorithm that the two rooms must be connected either directly to each other or via a corridor."),(0,n.kt)("p",null,"Below you can see a simple level graph with 5 rooms and 4 connections. If we use this level graph as an input for the algorithm, each generated dungeon will have exactly 5 rooms and ",(0,n.kt)("em",{parentName:"p"},"room 1")," will be connected to every other room in the dungeon."),(0,n.kt)(h,{src:"2d/level_graphs/basic_level_graph.png",caption:"Simple level graph with 5 rooms and 4 room connections",mdxType:"Image"}),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," It is not important how we draw the graph. It is only important how many rooms there are and which rooms are connected to each other.")),(0,n.kt)("h2",{id:"limitations"},"Limitations"),(0,n.kt)("h3",{id:"planar-graphs"},"Planar graphs"),(0,n.kt)("p",null,"Level graphs must be ",(0,n.kt)("strong",{parentName:"p"},"planar"),". We say that a graph is planar if it can be drawn on the plane in such a way that no two edges intersect. In our case that means that no two connection lines may intersect. If the input graph was not planar, we would not be able to find a dungeon without rooms or corridors overlapping one another."),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:"),' A level graph may be planar even if we draw it in a way that some edges intersect. It is because even if one drawing of the graph is "incorrect", that does not mean that there are intersecting edges in all the drawings.')),(0,n.kt)("h3",{id:"connected-graphs"},"Connected graphs"),(0,n.kt)("p",null,"Level graphs must be ",(0,n.kt)("strong",{parentName:"p"},"connected"),". We say that a graph is connected if there is a path between every pair of vertices. Below you can see a level graph that is not connected because there is no path between vertices on the left side and vertices on the right side."),(0,n.kt)(h,{src:"2d/level_graphs/not_connected_level_graph.png",caption:"Example of a level graph that is not connected",mdxType:"Image"}),(0,n.kt)("h2",{id:"creating-level-graphs"},"Creating level graphs"),(0,n.kt)("p",null,(0,n.kt)("em",{parentName:"p"},"LevelGraph")," is a ",(0,n.kt)("em",{parentName:"p"},"ScriptableObject")," that can be created by navigating to ",(0,n.kt)(d,{path:"2d:Level graph",mdxType:"Path"}),". Below you can see how are level graphs displayed in the Inspector window."),(0,n.kt)(h,{src:"2d/level_graphs/level_graph_inspector.png",caption:"Level graph in the Inspector window",mdxType:"Image"}),(0,n.kt)("h3",{id:"graph-editor"},"Graph editor"),(0,n.kt)("p",null,"The Graph editor window can be opened double-clicking the level graph ",(0,n.kt)("em",{parentName:"p"},"ScriptableObject"),"."),(0,n.kt)(h,{src:"2d/level_graphs/level_graph_window.png",caption:"Graph editor window",mdxType:"Image"}),(0,n.kt)("p",null,"Window controls:"),(0,n.kt)("ul",null,(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("em",{parentName:"li"},"Selected graph"),": the name of the currently selected level graph"),(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("em",{parentName:"li"},"Select in inspector"),": selects the current graph in the inspector window"),(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("em",{parentName:"li"},"Select level graph"),": selects a different level graph")),(0,n.kt)("p",null,"Working with level graphs:"),(0,n.kt)("ul",null,(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("em",{parentName:"li"},"Create room"),": double-click on an empty space in the grid"),(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("em",{parentName:"li"},"Configure room"),": double-click on an existing room"),(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("em",{parentName:"li"},"Delete room"),": press ",(0,n.kt)("em",{parentName:"li"},"Ctrl + Del"),", or right-click on a room and select ",(0,n.kt)("em",{parentName:"li"},"Delete room")),(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("em",{parentName:"li"},"Move room"),": left click and then drag around"),(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("em",{parentName:"li"},"Add connection"),": hold ",(0,n.kt)("em",{parentName:"li"},"Ctrl")," while left-clicking a room and then move the cursor to a different room"),(0,n.kt)("li",{parentName:"ul"},(0,n.kt)("em",{parentName:"li"},"Delete connection"),": right-click on a connection handle and select ",(0,n.kt)("em",{parentName:"li"},"Delete connection"))),(0,n.kt)(h,{src:"2d/level_graphs/level_graph_controls.gif",caption:"Level graph controls",mdxType:"Image"}),(0,n.kt)("h2",{id:"room-templates"},"Room templates"),(0,n.kt)("p",null,"When we have our rooms and connections, it is time to set up room templates. In the ",(0,n.kt)("em",{parentName:"p"},"Level graph")," inspector window above, we can see 2 sections - ",(0,n.kt)("em",{parentName:"p"},"Default room templates")," and ",(0,n.kt)("em",{parentName:"p"},"Corridor room templates"),". These sections are used to specify which room templates are available for which room. Below you can see the setup from ",(0,n.kt)("a",{parentName:"p",href:"/Edgar-Unity/docs/2.0.4/examples/example-1"},"Example 1"),"."),(0,n.kt)(h,{src:"2d/level_graphs/level_graph_inspector2.png",caption:"Example of assigned room templates",mdxType:"Image"}),(0,n.kt)("h3",{id:"room-templates-sets"},"Room templates sets"),(0,n.kt)("p",null,"It may sometimes be useful to group our room templates into groups like ",(0,n.kt)("em",{parentName:"p"},"Shop rooms"),", ",(0,n.kt)("em",{parentName:"p"},"Boss rooms"),", etc. We can create a so-called ",(0,n.kt)("strong",{parentName:"p"},"Room templates set")," by navigating to ",(0,n.kt)(d,{path:"2d:Room templates set",mdxType:"Path"}),". It is a simple ScriptableObject that holds an array of room templates, and we can use it instead of assigning individual room templates one by one. The main advantage is that if we later decide to add a new shop room template, we do not have to change all the shop rooms to include this new template - we simply add it to the room templates set."),(0,n.kt)(h,{src:"2d/level_graphs/room_templates_set.png",caption:"Example of a room templates set that holds all our basic rooms. If we add another room template later, the change gets propagated to all the rooms in the level graph that are using this room templates set.",mdxType:"Image"}),(0,n.kt)("h3",{id:"default-room-templates"},"Default room templates"),(0,n.kt)("h4",{id:"room-templates-1"},"Room templates"),(0,n.kt)("p",null,"Array of room templates that will be used for rooms that have no room shapes assigned. We can use this for our basic rooms and then configure our special rooms (spawn room, boss room, etc.) to use a different set of room templates."),(0,n.kt)("h4",{id:"room-templates-sets-1"},"Room templates sets"),(0,n.kt)("p",null,"Array of room templates sets that will be used for rooms that have no room shapes assigned. Room templates from these sets are used together with individual room templates."),(0,n.kt)("h3",{id:"corridor-room-templates"},"Corridor room templates"),(0,n.kt)("h4",{id:"room-templates-2"},"Room templates"),(0,n.kt)("p",null,"Array of room templates that will be used for corridor rooms. These room templates will be used if we set up the algorithm to use corridors instead of connecting rooms directly by doors. Can be left empty if we do not want to use corridors."),(0,n.kt)("h4",{id:"room-templates-sets-2"},"Room templates sets"),(0,n.kt)("p",null,"Array of room templates sets that will be used for corridor rooms. Room templates from these sets are used together with individual room templates."),(0,n.kt)("h3",{id:"configuring-individual-rooms"},"Configuring individual rooms"),(0,n.kt)("p",null,"If we double-click on a room in the Graph editor, it gets selected, and we can configure it in the inspector. We can set the name of the room which will be displayed in the Graph editor. We can also assign room templates and room templates sets that will be used only for this room. By assigning any room template or room template set, we override the default room templates that are set in the level graph itself."),(0,n.kt)(h,{src:"2d/level_graphs/room_inspector1.png",caption:"Configuration of a spawn room",mdxType:"Image"}),(0,n.kt)("h2",{id:"pro-custom-rooms-and-connections"},"(PRO) Custom rooms and connections"),(0,n.kt)("p",null,"It may be often useful to add additional information to individual rooms (or connections). For example, we may want to add a type to each of the rooms and then do something based on the type. To achieve that, we can provide our own implementation of the ",(0,n.kt)("a",{parentName:"p",href:"https://ondrejnepozitek.github.io/ProceduralLevelGenerator-UnityApiDocs/master/api/Edgar.Unity.RoomBase.html"},"RoomBase")," and ",(0,n.kt)("a",{parentName:"p",href:"https://ondrejnepozitek.github.io/ProceduralLevelGenerator-UnityApiDocs/master/api/Edgar.Unity.ConnectionBase.html"},"ConnectionBase")," classes. There are at least two possible approaches."),(0,n.kt)(u,{id:"custom-rooms-and-connections",mdxType:"FeatureUsage"}),(0,n.kt)("h3",{id:"inherit-from-room"},"Inherit from ",(0,n.kt)("a",{parentName:"h3",href:"https://ondrejnepozitek.github.io/ProceduralLevelGenerator-UnityApiDocs/master/api/Edgar.Unity.Room.html"},"Room")),(0,n.kt)("p",null,"The first approach is that we create a class that inherits from the ",(0,n.kt)("a",{parentName:"p",href:"https://ondrejnepozitek.github.io/ProceduralLevelGenerator-UnityApiDocs/master/api/Edgar.Unity.Room.html"},"Room")," class which is the default implementation that is used in level graphs. This approach is good if we want to just add something and do not want to change how the room works. We can also override the ",(0,n.kt)("inlineCode",{parentName:"p"},"GetDisplayName()")," method to change how is the room displayed in the level graph editor."),(0,n.kt)("p",null,"This is the recommended approach for the majority of users."),(0,n.kt)("h3",{id:"inherit-from-roombase"},"Inherit from ",(0,n.kt)("a",{parentName:"h3",href:"https://ondrejnepozitek.github.io/ProceduralLevelGenerator-UnityApiDocs/master/api/Edgar.Unity.RoomBase.html"},"RoomBase")),(0,n.kt)("p",null,"The second approach is that we inherit directly from the ",(0,n.kt)("a",{parentName:"p",href:"https://ondrejnepozitek.github.io/ProceduralLevelGenerator-UnityApiDocs/master/api/Edgar.Unity.RoomBase.html"},"RoomBase")," class. If we do that, we have to implement all the abstract methods (currently ",(0,n.kt)("inlineCode",{parentName:"p"},"GetDisplayName()")," and ",(0,n.kt)("inlineCode",{parentName:"p"},"GetRoomTemplates()"),"). An advantage of this approach is that in some situations, we may not need any logic related to room templates, so we just return null from the method, and we will not see anything related to room templates in the inspector of the room. This may be useful in a situation where we resolve room templates manually based on the type of the room."),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," The same logic applies to inheriting from ",(0,n.kt)("a",{parentName:"p",href:"https://ondrejnepozitek.github.io/ProceduralLevelGenerator-UnityApiDocs/master/api/Edgar.Unity.Connection.html"},"Connection")," or ",(0,n.kt)("a",{parentName:"p",href:"https://ondrejnepozitek.github.io/ProceduralLevelGenerator-UnityApiDocs/master/api/Edgar.Unity.ConnectionBase.html"},"ConnectionBase"),".")),(0,n.kt)("h3",{id:"configure-level-graph"},"Configure level graph"),(0,n.kt)("p",null,"When we have our custom room or connection type ready, we have to configure the level graph to use them. If we open the level graph in the inspector, we should be able to choose the custom types from the dropdown."),(0,n.kt)(h,{src:"2d/level_graphs/custom_rooms.png",caption:"Custom room and connection types (PRO version)",mdxType:"Image"}),(0,n.kt)("blockquote",null,(0,n.kt)("p",{parentName:"blockquote"},(0,n.kt)("strong",{parentName:"p"},"Note:")," It is not possible to easily convert a level graph from using one room/connection type to another. Therefore, it is important to decide if you want to use a custom room/connection before you create your level graphs. Otherwise, you will have to recreate them later with the correct types.")),(0,n.kt)("h3",{id:"accessing-room-information"},"Accessing room information"),(0,n.kt)("p",null,"If we add some additional information to a room or connection, we probably expect to somehow use this information later. The first step is to get access to the ","[RoomInstance][RoomInstance#properties]"," class which is described ",(0,n.kt)("a",{parentName:"p",href:"/Edgar-Unity/docs/2.0.4/basics/generated-level-info"},"here"),". When we have an instance of this class, we can use the ",(0,n.kt)("inlineCode",{parentName:"p"},"RoomInstance.Room")," property. This property is of the ",(0,n.kt)("inlineCode",{parentName:"p"},"RoomBase")," type, so we have to cast it to our custom room type."),(0,n.kt)("h3",{id:"custom-colours-in-the-level-graph-editor"},"Custom colours in the level graph editor"),(0,n.kt)("p",null,"It is also possible to change how custom rooms and connections look in the level graph editor. We just have to override the ",(0,n.kt)("inlineCode",{parentName:"p"},"GetEditorStyle()")," method and return an instance of ",(0,n.kt)("a",{parentName:"p",href:"https://ondrejnepozitek.github.io/ProceduralLevelGenerator-UnityApiDocs/master/api/Edgar.Unity.RoomEditorStyle.html#properties"},"RoomEditorStyle")," or ",(0,n.kt)("a",{parentName:"p",href:"https://ondrejnepozitek.github.io/ProceduralLevelGenerator-UnityApiDocs/master/api/Edgar.Unity.ConnectionEditorStyle.html#properties"},"ConnectionEditorStyle"),"."),(0,n.kt)("pre",null,(0,n.kt)("code",{parentName:"pre"},"public class GungeonRoom : RoomBase\n{\n    public GungeonRoomType Type;\n\n    /* ... */\n\n    public override RoomEditorStyle GetEditorStyle(bool isFocused)\n    {\n        var style = base.GetEditorStyle(isFocused);\n\n        var backgroundColor = style.BackgroundColor;\n\n        // Use different colors for different types of rooms\n        switch (Type)\n        {\n            case GungeonRoomType.Entrance:\n                backgroundColor = new Color(38/256f, 115/256f, 38/256f);\n                break;\n\n            /* ... */\n        }\n\n        style.BackgroundColor = backgroundColor;\n\n        // Darken the color when focused\n        if (isFocused)\n        {\n            style.BackgroundColor = Color.Lerp(style.BackgroundColor, Color.black, 0.7f);\n        }\n\n        return style;\n    }\n}\n")),(0,n.kt)(h,{src:"2d/examples/gungeon/level_graph_1.png",caption:"Different colours for special types of rooms",mdxType:"Image"}),(0,n.kt)("h2",{id:"pro-directed-level-graphs"},"(PRO) Directed level graphs"),(0,n.kt)("p",null,"By default, all level graphs are undirected, meaning that it does not matter whether you create a connection from ",(0,n.kt)("em",{parentName:"p"},"Room 1")," to ",(0,n.kt)("em",{parentName:"p"},"Room 2")," or the other way around. If you want to have more control over generated levels, you can make level graphs directed and combine that with entrance-only and exit-only doors. See the ",(0,n.kt)("a",{parentName:"p",href:"/Edgar-Unity/docs/2.0.4/guides/directed-level-graphs"},"Directed level graphs")," guide for more information and examples."))}v.isMDXComponent=!0}}]);