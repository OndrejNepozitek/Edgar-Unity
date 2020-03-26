namespace Assets.ProceduralLevelGenerator.Editor.NodeBasedEditor
{
	using UnityEngine;

	public interface IEditorNodeBase
	{
		bool ProcessEvents(Event e);

		void Draw();

		void Drag(Vector2 delta);
	}
}