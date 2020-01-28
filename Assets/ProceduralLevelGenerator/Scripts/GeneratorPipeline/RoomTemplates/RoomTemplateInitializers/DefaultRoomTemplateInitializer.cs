using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.TilemapLayers;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.RoomTemplateInitializers
{
#if UNITY_EDITOR
    using UnityEditor;

#endif

    /// <summary>
    ///     Default room template initializer that uses the default
    ///     tilemap layers handler.
    /// </summary>
    public class DefaultRoomTemplateInitializer : BaseRoomTemplateInitializer
    {
#if UNITY_EDITOR
        public void Initialize()
        {
            var tilemapLayersHandler = AssetDatabase
                .LoadAssetAtPath<TilemapLayersHandler>("Assets/ProceduralLevelGenerator/ScriptableObjects/DefaultTilemapLayersHandler.asset");

            gameObject.transform.position = Vector3.zero;

            InitializeTilemaps(tilemapLayersHandler);

            InitializeDoors();

            // Destroy the initializer
            DestroyImmediate(this);
        }
#endif
    }
}