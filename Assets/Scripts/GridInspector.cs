namespace Assets.Scripts
{
	using System.Linq;
	using TileMapping;
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(Grid))]
	public class GridInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			var scriptHolder = FindObjectsOfType<GameObject>().Single(x => x.name == "DungeonGenerator");

			if (GUILayout.Button("Fix walls!"))
			{
				var wallsCorrection = scriptHolder.GetComponent<WallsCorrection>();
				wallsCorrection.GoToCorrect = ((Grid) target).gameObject;
				wallsCorrection.Execute();
			}
		}
	}
}