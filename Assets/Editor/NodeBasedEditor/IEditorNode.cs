namespace Assets.Editor.NodeBasedEditor
{
	using UnityEngine;

	public interface IEditorNode
	{
		bool ProcessEvents(Event e);

		void Draw();

		void Drag(Vector2 delta);
	}
}