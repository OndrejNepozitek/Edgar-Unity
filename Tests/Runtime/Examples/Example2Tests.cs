using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProceduralLevelGenerator.Unity.Examples.Example2.Scripts;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using ProceduralLevelGenerator.Unity.Generators.Common.Utils;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

namespace ProceduralLevelGenerator.Unity.Tests.Runtime.Examples
{
    public class Example2Tests : ExampleTestsBase
    {
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            LoadScene("Example2");
            yield return null;
        }

        [UnityTest]
        public IEnumerator BasicTest()
        {
            var dungeonGeneratorGameObject = GameObject.Find("Dungeon Generator");
            var dungeonGenerator = dungeonGeneratorGameObject.GetComponent<DungeonGenerator>();
            Assert.IsNotNull(dungeonGenerator);

            var levelGraph = dungeonGenerator.FixedLevelGraphConfig.LevelGraph;

            var generatedLevelGameObject = GameObject.Find("Generated Level");
            var levelInfo = generatedLevelGameObject.GetComponent<LevelInfo>();
            Assert.IsNotNull(levelInfo);
            Assert.IsTrue(levelInfo.RoomInstances.Count(x => !x.IsCorridor) == levelGraph.Rooms.Count);

            dungeonGenerator.Generate();
            yield return null;

            var levelInfoNew = generatedLevelGameObject.GetComponent<LevelInfo>();
            Assert.IsTrue(levelInfo != levelInfoNew);
            Assert.IsNotNull(levelInfoNew);
            Assert.IsTrue(levelInfoNew.RoomInstances.Count(x => !x.IsCorridor) == levelGraph.Rooms.Count);

            yield return null;
        }

        [UnityTest]
        public IEnumerator DifferentLevelGraphs()
        {
            var levelGraphNames = new List<string>()
            {
                "Assets/ProceduralLevelGenerator/Examples/Example2/SimpleLevelGraph.asset",
                "Assets/ProceduralLevelGenerator/Examples/Example2/RealLifeLevelGraph.asset",
            };

            var dungeonGeneratorGameObject = GameObject.Find("Dungeon Generator");
            var dungeonGenerator = dungeonGeneratorGameObject.GetComponent<DungeonGenerator>();
            Assert.IsNotNull(dungeonGenerator);

            foreach (var levelGraphName in levelGraphNames)
            {
                var levelGraph = AssetDatabase.LoadAssetAtPath<LevelGraph>(levelGraphName);
                Assert.IsNotNull(levelGraph, $"Could not load {levelGraphName}");

                dungeonGenerator.FixedLevelGraphConfig.LevelGraph = levelGraph;
                dungeonGenerator.Generate();
                yield return null;
            }

            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            Object.Destroy(Example2GameManager.Instance);
            yield return null;
        }
    }
}