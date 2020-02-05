using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.RoomTemplateInitializers;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.TilemapLayers.Editor
{
    using TilemapLayers;
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(ConfigurableRoomTemplateInitializer))]
	public class RoomTemaplteInitializerInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			var roomTemplateInitializer = (ConfigurableRoomTemplateInitializer) target;

			DrawDefaultInspector();

			if (GUILayout.Button("Initialize room template"))
			{
				roomTemplateInitializer.Initialize();
			}
		}
	}
}