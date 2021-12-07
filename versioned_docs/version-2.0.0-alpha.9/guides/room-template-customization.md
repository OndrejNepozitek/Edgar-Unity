---
title: Room template customization
---

In this guide, we will cover:

1. How to **override the default structure of tilemap layers**
2. How to **customize room templates** e.g. by automatically adding additional game objects/components

## Tilemap layers structure

Each room template comes with a set of tilemap layers that are all children of the *Tilemaps* game object in the room template. For dungeon room templates, we usually want a tilemap layer for wall tiles, floor tiles, etc. We tried to provide a reasonable default setup, but it is expected that users may want to use a different structure.

There is a *Tilemap Layers Structure* field in the *Post-processing config* section of the *Dungeon Generator* component that controls the structure of tilemaps. This field is backed by an enum which currently has 3 possible values:

### `Default`

The default tilemaps structure is what you get when you create a room template via the *Create* menu dialogue. This structure of tilemaps is *fixed*, meaning that if you customize the structure of some of your room templates, the resulting structure will always be the same. **Therefore, it is not recommended to make any changes to the structure of tilemaps when using this mode,** as it can lead to unexpected results.

### `FromExample`

This mode makes it possible to infer the desired structure of tilemaps from an example room template given to the generator. When enabled, there is a new field - *Tilemaps Layers Example*. To configure this mode, you have to take one of your room templates and assign it to that field. After a level is generated, the generator will take the example room template, look for the `Tilemaps` game object inside the room template and then instantiate this game object. It means that anything that is inside the `Tilemaps` game object will be included in the generated level, including the structure of tilemaps in the example. The last thing that the generator does is that it removes all the tiles in the example so that you can use a real room template as an example.

> **Note:** In the past, if you wanted to have a custom structure of tilemaps, you had to write a piece of code to do that. Even if you wanted to change something small, you still had to do that. The `FromExample` mode is here to change that.

Typical use case:
- Start with a room template with the default structure of tilemaps
- Decide that you want to change something in the tilemaps - modify layers/sorting layers, add/remove colliders, etc.
- Change the room template to reflect these changes
- Use the `FromExample` mode and assign this room template to the example field
- When creating a new room template, duplicate one of the room templates with the correct structure (instead of using the *Create* menu dialogue to create the empty default room template)

> **Note:** It is still very important to have the same structure of tilemaps across all room templates. So make sure that all the room templates have the same structure as the example room template.

### `Custom`

The last way of customizing the structure of tilemap layers requires writing a piece of code. You have to create a class that inherits from the `TilemapLayersHandlerBase` class. Below is a commented example of how to do that:

    // TilemapLayersHandlerBase inherit from ScriptableObject so we need to create an asset
    // menu item that we will use to create the scriptable object instance.
    // The menu name can be changed to anything you want.
    // [CreateAssetMenu(menuName = "Dungeon generator/Custom tilemap layers handler", fileName = "CustomTilemapLayersHandler")]
    public class CustomTilemapLayersHandlerExample : TilemapLayersHandlerBase
    {
        public override void InitializeTilemaps(GameObject gameObject)
        {
            // First make sure that you add the grid component
            var grid = gameObject.AddComponent<Grid>();

            // If we want a different cell size, we can configure that here
            // grid.cellSize = new Vector3(1, 2, 1);

            // And now we create individual tilemap layers
            var floorTilemapObject = CreateTilemapGameObject("Floor", gameObject, 0);

            var wallsTilemapObject = CreateTilemapGameObject("Walls", gameObject, 1);
            AddCompositeCollider(wallsTilemapObject);

            CreateTilemapGameObject("Additional layer 1", gameObject, 2);
            CreateTilemapGameObject("Additional layer 2", gameObject, 3);
        }

        /// <summary>
        /// Helper to create a tilemap layer
        /// </summary>
        protected GameObject CreateTilemapGameObject(string name, GameObject parentObject, int sortingOrder)
        {
            // Create a new game object that will hold our tilemap layer
            var tilemapObject = new GameObject(name);
            // Make sure to correctly set the parent
            tilemapObject.transform.SetParent(parentObject.transform);
            var tilemap = tilemapObject.AddComponent<Tilemap>();
            var tilemapRenderer = tilemapObject.AddComponent<TilemapRenderer>();
            tilemapRenderer.sortingOrder = sortingOrder;

            return tilemapObject;
        }

        /// <summary>
        /// Helper to add a collider to a given tilemap game object.
        /// </summary>
        protected void AddCompositeCollider(GameObject tilemapGameObject, bool isTrigger = false)
        {
            var tilemapCollider2D = tilemapGameObject.AddComponent<TilemapCollider2D>();
            tilemapCollider2D.usedByComposite = true;

            var compositeCollider2d = tilemapGameObject.AddComponent<CompositeCollider2D>();
            compositeCollider2d.geometryType = CompositeCollider2D.GeometryType.Polygons;
            compositeCollider2d.isTrigger = isTrigger;

            tilemapGameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

When we have our custom tilemap layers handler prepared, there are 2 additional steps:

1. **Register the handler in the generator** so that each generated level has this new structure
2. (*Optional but highly recommended*) **Create a custom room template initializer** that will help you with creating room templates with the new structure

#### Register the  handler in the generator

We have to configure the generator to use our custom tilemap layers handler.

First, we have to create an instance of a ScriptableObject that will hold our custom handler. This is the reason why we added the *CreateAssetMenu* attribute to our handler. In the project view, we right-click in a folder and choose *Create -> Edgar -> Custom tilemap layers handler* (the path may be changed in the *CreateAssetMenu* attribute). That should create a file in the current folder.

And second, we have to drag and drop this file to the *Tilemap Layers Handler* field of our generator component. Make sure to choose the *Custom* tilemap layers structure mode to see the field. If we want to switch the tilemap layers handler in the future, we can either replace it with a different handler or remove it to use the default handler.

#### Create a custom room template initializer

Now we will add a *Create menu* shortcut to create a room template with a custom tilemap layers handler. This is better explained in the next section, below is the minimal working example. It will add a *Create -> Edgar -> Custom room template* menu item that will create a room template prefab with our custom tilemap layers.

    using UnityEngine;
    #if UNITY_EDITOR
    using UnityEditor;
    #endif

    public class CustomRoomTemplateInitializerExample : RoomTemplateInitializerBase
    {
        protected override void InitializeTilemaps(GameObject tilemapsRoot)
        {
            // Create an instance of our custom tilemap layers handler
            var tilemapLayersHandler = ScriptableObject.CreateInstance<CustomTilemapLayersHandlerExample>();

            // Initialize tilemaps
            tilemapLayersHandler.InitializeTilemaps(tilemapsRoot);
        }

        #if UNITY_EDITOR
        [MenuItem("Assets/Create/Edgar/Custom room template")]
        public static void CreateRoomTemplatePrefab()
        {
            // Make sure to use the correct generic parameter - it should be the type of this class
            RoomTemplateInitializerUtils.CreateRoomTemplatePrefab<CustomRoomTemplateInitializerExample>();
        }
        #endif
    }


## Room template initializers

The process of creating room templates would be very time-consuming and error-prone if done manually, i.e. by creating all the tilemap layers by hand. If you use the default tilemaps structure, you can use the *Create* menu dialogue. But if you somehow customize your room templates, e.g. by adding lights represented by multiple game objects, the default room template initializer is no longer useful. There are basically two ways of making it easier to create room templates:

1. Choose one of the already created room templates and create a copy of the prefab
2. Write a piece of code that will handle the initialization of new room templates

The first option is a pretty solid choice most of the time and requires no extra preparation steps. In this guide, we will see how to handle the second option. We will implement a so-called *room template initializer*. This technique is mostly only useful if you use the *Custom* tilemaps structure mode, because you will may want to use a custom tilemap layers handler.

There are two main ways of utilizing custom room template initializers:

1. To override the default structure of tilemap layers
2. To do some post-processing, e.g. add additional game objects or components to a room template

The steps to create a custom room template initializer are the following:

1. Create a class that inherits from `RoomTemplateInitializerBase`
2. (Optional) Override the `InitializeTilemaps()` method and use a custom tilemap layers handler
3. (Optional) Run additional logic after a room template is created
4. Add custom menu item so that we can use the initializer via a *Create asset menu*

An example can be seen below:

    using UnityEngine;
    #if UNITY_EDITOR
    using UnityEditor;
    #endif

    public class CustomRoomTemplateInitializerExample2 : RoomTemplateInitializerBase
    {
        public override void Initialize()
        {
            // Call the default initialization
            base.Initialize();

            // Place your custom logic after initialization here
            // This script is attached to the room template game object that is being created (and this component is later removed)
            // So you can access the gameObject field and add e.g. additional game object

            // For example, we can add a game object that will hold lights
            var lightsGameObject = new GameObject("Lights");
            lightsGameObject.transform.SetParent(gameObject.transform);
        }

        protected override void InitializeTilemaps(GameObject tilemapsRoot)
        {
            // Create an instance of our custom tilemap layers handler
            var tilemapLayersHandler = ScriptableObject.CreateInstance<CustomTilemapLayersHandlerExample>();

            // Initialize tilemaps
            tilemapLayersHandler.InitializeTilemaps(tilemapsRoot);
        }

        #if UNITY_EDITOR
        [MenuItem("Assets/Create/Edgar/Custom room template")]
        public static void CreateRoomTemplatePrefab()
        {
            // Make sure to use the correct generic parameter - it should be the type of this class
            RoomTemplateInitializerUtils.CreateRoomTemplatePrefab<CustomRoomTemplateInitializerExample2>();
        }
        #endif
    }