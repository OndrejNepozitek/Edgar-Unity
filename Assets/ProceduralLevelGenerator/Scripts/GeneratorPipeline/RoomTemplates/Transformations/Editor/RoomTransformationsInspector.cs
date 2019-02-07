namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.Transformations.Editor
{
	using Transformations;
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(RoomTransformationsScript))]
	public class RoomTransformationsInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			var roomTransformations = (RoomTransformationsScript)target;

			DrawDefaultInspector();

			if (GUILayout.Button("Transform"))
			{
				roomTransformations.Transform();
			}
		}
	}
}