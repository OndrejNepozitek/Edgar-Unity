namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.TilemapLayers.Editor
{
	using RoomTemplateInitializers;
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(DefaultRoomTemplateInitializer))]
	public class DefaultRoomTemaplteInitializerInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			var roomTemplateInitializer = (DefaultRoomTemplateInitializer)target;

			DrawDefaultInspector();

			if (GUILayout.Button("Initialize room template"))
			{
				roomTemplateInitializer.Initialize();
			}
		}
	}
}