using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    [CustomEditor(typeof(RoomTemplateInitializerBase), true)]
	public class RoomTemplateInitializerInspector : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var roomTemplateInitializer = (RoomTemplateInitializerBase) target;

			DrawDefaultInspector();

			if (GUILayout.Button("Initialize room template"))
			{
				roomTemplateInitializer.Initialize();
				DestroyImmediate(roomTemplateInitializer);
			}
		}
	}
}