namespace Assets.Scripts.TileMapping
{
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(WallsCorrectionScript))]
	public class WallsCorrectionInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			var dungeonGenerator = (WallsCorrectionScript) target;

			if (GUILayout.Button("Correct!"))
			{
				dungeonGenerator.Execute();
			}
		}
	}
}