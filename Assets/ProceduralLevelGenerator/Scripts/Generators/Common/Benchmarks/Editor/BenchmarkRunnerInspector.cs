using UnityEditor;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Benchmarks.Editor
{
    [CustomEditor(typeof(BenchmarkRunner))]
    public class BenchmarkRunnerInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var benchmarkRunner = (BenchmarkRunner) target;

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(BenchmarkRunner.Runs)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(BenchmarkRunner.ScreenshotCamera)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(BenchmarkRunner.ScreenshotCameraSize)));

            EditorGUI.BeginDisabledGroup(benchmarkRunner.IsRunning);
            var buttonText = benchmarkRunner.IsRunning ? $"Running - {benchmarkRunner.Progress}/{benchmarkRunner.Runs}" : "Run benchmark";
            if (GUILayout.Button(buttonText))
            {
                benchmarkRunner.RunBenchmark();
            }
            EditorGUI.EndDisabledGroup();

            if (benchmarkRunner.IsRunning)
            {
                if (GUILayout.Button("Stop benchmark"))
                {
                    benchmarkRunner.StopBenchmark();
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}