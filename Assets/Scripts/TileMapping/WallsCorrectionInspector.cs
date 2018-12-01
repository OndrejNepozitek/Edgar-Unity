namespace Assets.Scripts.TileMapping
{
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(WallsCorrection))]
	public class WallsCorrectionInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			var dungeonGenerator = (WallsCorrection) target;

			if (GUILayout.Button("Correct!"))
			{
				dungeonGenerator.Execute();
			}
		}
	}
}