namespace Assets.Scripts
{
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(DungeonGenerator))]
	public class DungeonGeneratorInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			var dungeonGenerator = (DungeonGenerator) target;

			if (GUILayout.Button("Generate!"))
			{
				dungeonGenerator.Generate();
			}
		}
	}
}