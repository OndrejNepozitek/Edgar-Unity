using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.RoomTemplateInitializers;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.TilemapLayers.Editor
{
    [CustomEditor(typeof(BaseRoomTemplateInitializer), true)]
	public class RoomTemplateInitializerInspector : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var roomTemplateInitializer = (BaseRoomTemplateInitializer) target;

			DrawDefaultInspector();

			if (GUILayout.Button("Initialize room template"))
			{
				roomTemplateInitializer.Initialize();
				DestroyImmediate(roomTemplateInitializer);
			}
		}
	}
}