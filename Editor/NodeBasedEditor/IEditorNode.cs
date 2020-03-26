namespace ProceduralLevelGenerator.Unity.Editor.NodeBasedEditor
{
	public interface IEditorNode<TData> : IEditorNodeBase
	{
		TData Data { get; set; }
	}
}