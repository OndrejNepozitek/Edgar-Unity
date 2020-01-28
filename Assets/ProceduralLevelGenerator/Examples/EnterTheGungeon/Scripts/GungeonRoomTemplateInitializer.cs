using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.RoomTemplateInitializers;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
#if UNITY_EDITOR
    using UnityEditor;

#endif

    public class GungeonRoomTemplateInitializer : BaseRoomTemplateInitializer
    {
#if UNITY_EDITOR
        public void Initialize()
        {
            var tilemapLayersHandler = AssetDatabase
                .LoadAssetAtPath<GungeonTilemapLayersHandler>(
                    "Assets/ProceduralLevelGenerator/Examples/EnterTheGungeon/Pipeline tasks/GungeonTilemapLayersHandler.asset");

            gameObject.transform.position = Vector3.zero;

            InitializeTilemaps(tilemapLayersHandler);

            InitializeDoors();

            // Custom behaviour
            gameObject.AddComponent<RoomManager>();
            gameObject.transform.Find("Floor").gameObject.AddComponent<RoomEnterHandler>();

            // Destroy the initializer
            DestroyImmediate(this);
        }
#endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(GungeonRoomTemplateInitializer))]
    public class GungeonRoomTemplateInitializerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            var roomTemplateInitializer = (GungeonRoomTemplateInitializer) target;

            DrawDefaultInspector();

            if (GUILayout.Button("Initialize room template"))
            {
                roomTemplateInitializer.Initialize();
            }
        }
    }
#endif
}