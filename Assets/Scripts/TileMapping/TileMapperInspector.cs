namespace Assets.Scripts.TileMapping
{
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(TileMapper))]
	public class TileMapperInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			var tileMapper = (TileMapper)target;

			if (GUILayout.Button("Execute!"))
			{
				tileMapper.Execute();
			}
		}
	}
}