using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Editor.NodeBasedEditor
{
    public interface IEditorNodeBase
	{
		bool ProcessEvents(Event e);

		void Draw();

		void Drag(Vector2 delta);
	}
}