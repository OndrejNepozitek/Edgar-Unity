﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edgar.Unity.Examples.Example1;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

namespace Edgar.Unity.Tests.Runtime.Examples
{
    public class Example1Tests : ExampleTestsBase
    {
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            LoadScene("Example1");
            yield return null;
        }

        [UnityTest]
        public IEnumerator BasicTest()
        {
            var dungeonGeneratorGameObject = GameObject.Find("Dungeon Generator");
            var dungeonGenerator = dungeonGeneratorGameObject.GetComponent<DungeonGeneratorGrid2D>();
            Assert.IsNotNull(dungeonGenerator);

            var levelGraph = dungeonGenerator.FixedLevelGraphConfig.LevelGraph;

            var generatedLevelGameObject = GameObject.Find("Generated Level");
            var levelInfo = generatedLevelGameObject.GetComponent<LevelInfoGrid2D>();
            Assert.IsNotNull(levelInfo);
            Assert.IsTrue(levelInfo.RoomInstances.Count(x => !x.IsCorridor) == levelGraph.Rooms.Count);

            dungeonGenerator.Generate();
            yield return null;

            var levelInfoNew = generatedLevelGameObject.GetComponent<LevelInfoGrid2D>();
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
                "Assets/Edgar/Examples/Grid2D/Example1/SimpleLevelGraph.asset",
                "Assets/Edgar/Examples/Grid2D/Example1/RealLifeLevelGraph.asset",
            };

            var dungeonGeneratorGameObject = GameObject.Find("Dungeon Generator");
            var dungeonGenerator = dungeonGeneratorGameObject.GetComponent<DungeonGeneratorGrid2D>();
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
            Object.Destroy(Example1GameManager.Instance);
            yield return null;
        }
    }
}