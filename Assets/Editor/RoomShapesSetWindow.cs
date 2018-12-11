namespace Assets.Editor
{
	using System.Collections.Generic;
	using System.Linq;
	using Scripts;
	using Scripts.DataOld;
	using UnityEditor;
	using UnityEngine;

	public class RoomShapesSetWindow : EditorWindow
	{
		private List<RoomShapeSetItem> roomShapeSetItems = new List<RoomShapeSetItem>();

		private float columnRoomShapeWidth = 150;
		private float columnRotateWidth = 50;
		private float columnProbabilityWidth = 80;
		private float columnNormalizeProbabilitiesWidth = 80;

		private string setName;
		private int id;

		public RoomShapesSetWindow()
		{
			minSize = new Vector2(500, 500);
		}

		public void OnGUI()
		{
			setName = EditorGUILayout.TextField("Name:", setName, GUILayout.Width(300));
			GUILayout.Space(30);

			GUILayout.BeginVertical();

			GUILayout.BeginHorizontal();

			GUILayout.Label("Room shape", GUILayout.Width(columnRoomShapeWidth));
			GUILayout.Label("Rotate", GUILayout.Width(columnRotateWidth));
			GUILayout.Label("Probability", GUILayout.Width(columnProbabilityWidth));
			// GUILayout.Label("Normalize probs", GUILayout.Width(columnNormalizeProbabilitiesWidth));

			GUILayout.EndHorizontal();

			var roomShapes = MainWindow.Data.RoomShapes;

			var ids = roomShapes.Select(x => x.Key);
			var names = roomShapes.Select(x => x.Value.Name);

			var idsForDropdown = ids.Prepend(0).ToArray();
			var namesForDropdown = names.Prepend("Not selected").ToArray();

			var idsToIndex = new Dictionary<int, int>();

			for (var i = 0; i < idsForDropdown.Length; i++)
			{
				idsToIndex[idsForDropdown[i]] = i;
			}

			for (var i = 0; i < roomShapeSetItems.Count; i++)
			{
				var roomShapeSetItem = roomShapeSetItems[i];
				GUILayout.BeginHorizontal();

				var idIndex = EditorGUILayout.Popup(idsToIndex[roomShapeSetItem.RoomShapeId], namesForDropdown,
					GUILayout.Width(columnRoomShapeWidth));

				roomShapeSetItem.RoomShapeId = idsForDropdown[idIndex];
				roomShapeSetItem.Rotate = EditorGUILayout.Toggle(roomShapeSetItem.Rotate, GUILayout.Width(columnRotateWidth));
				roomShapeSetItem.Probability =
					EditorGUILayout.FloatField(roomShapeSetItem.Probability, GUILayout.Width(columnProbabilityWidth));
				// roomShapeSetItem.NormalizeProbabilities = EditorGUILayout.Toggle(roomShapeSetItem.NormalizeProbabilities, GUILayout.Width(columnNormalizeProbabilitiesWidth));

				if (GUILayout.Button("Delete", GUILayout.Width(60)))
				{
					roomShapeSetItems.RemoveAt(i);
				}

				GUILayout.EndHorizontal();
			}

			if (GUILayout.Button("Add item"))
			{
				roomShapeSetItems.Add(new RoomShapeSetItem() { RoomShapeId = 0, Probability = 1, Rotate = true, NormalizeProbabilities = true});
			}

			GUILayout.Space(30);

			var data = MainWindow.Data;

			if (GUILayout.Button("Save and close"))
			{
				if (id == 0)
				{
					id = data.RoomShapes.GetNextId();
				}

				data.RoomShapeSets[id] = new RoomShapeSet()
				{
					Name = setName,
					RoomShapeSetItems = roomShapeSetItems
				};

				Close();
			}

			if (id != 0 && GUILayout.Button("Delete"))
			{
				data.RoomShapeSets.Remove(id);
				Close();
			}

			GUILayout.EndVertical();
		}

		public void SetRoomShapeSet(int id, RoomShapeSet roomShapeSet)
		{
			this.id = id;
			this.setName = roomShapeSet.Name;

			if (id != 0)
			{
				name = roomShapeSet.Name;
				roomShapeSetItems = new List<RoomShapeSetItem>(roomShapeSet.RoomShapeSetItems);
			}
		}
	}
}