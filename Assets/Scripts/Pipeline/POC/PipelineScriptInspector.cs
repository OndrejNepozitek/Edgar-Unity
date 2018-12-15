namespace Assets.Scripts.Pipeline.POC
{
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(PipelineScript))]
	public class PipelineScriptInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			var pipelineScript = (PipelineScript) target;

			if (GUILayout.Button("Execute"))
			{
				pipelineScript.Execute();
			}

			if (GUILayout.Button("Execute new"))
			{
				pipelineScript.Execute2();
			}
		}
	}
}