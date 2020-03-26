using UnityEditor;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.Transformations.Editor
{
    [CustomEditor(typeof(RoomTransformationsScript))]
	public class RoomTransformationsInspector : UnityEditor.Editor
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