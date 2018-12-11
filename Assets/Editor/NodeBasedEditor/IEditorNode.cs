namespace Assets.Editor.NodeBasedEditor
{
	public interface IEditorNode<TData> : IEditorNodeBase
	{
		TData Data { get; set; }
	}
}