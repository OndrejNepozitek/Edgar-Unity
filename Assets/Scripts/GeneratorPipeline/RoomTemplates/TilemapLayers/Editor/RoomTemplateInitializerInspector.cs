namespace Assets.Scripts.GeneratorPipeline.RoomTemplates.TilemapLayers.Editor
{
	using Transformations;
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(RoomTemplateInitializer))]
	public class RoomTemaplteInitializerInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			var roomTemplateInitializer = (RoomTemplateInitializer) target;

			DrawDefaultInspector();

			if (GUILayout.Button("Initialize room template"))
			{
				roomTemplateInitializer.Initialize();
			}
		}
	}
}