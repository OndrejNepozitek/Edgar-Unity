using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edgar.Unity.Tests;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

namespace Edgar.Unity.Edgar.Tests.Runtime
{
    public class TilemapLayersStructureTests : TestBase
    {
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            LoadScene("TilemapLayersStructure");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TilemapLayersStructure_Default()
        {
            var dungeonGenerator = GetDungeonGenerator("Dungeon Generator Default");
            Assert.IsNotNull(dungeonGenerator);

            dungeonGenerator.Generate();
            yield return null;

            AssertTilemapLayerExists("Floor");
        }

        [UnityTest]
        public IEnumerator TilemapLayersStructure_FromExample()
        {
            var dungeonGenerator = GetDungeonGenerator("Dungeon Generator FromExample");
            Assert.IsNotNull(dungeonGenerator);

            dungeonGenerator.Generate();
            yield return null;

            AssertTilemapLayerExists("Floor FromExample");
        }

        [UnityTest]
        public IEnumerator TilemapLayersStructure_FromExampleMissing()
        {
            var dungeonGenerator = GetDungeonGenerator("Dungeon Generator FromExampleMissing");
            Assert.IsNotNull(dungeonGenerator);

            Assert.Throws<ConfigurationException>(() => dungeonGenerator.Generate());
            yield return null;
        }

        [UnityTest]
        public IEnumerator TilemapLayersStructure_FromExampleInvalid()
        {
            var dungeonGenerator = GetDungeonGenerator("Dungeon Generator FromExampleInvalid");
            Assert.IsNotNull(dungeonGenerator);

            Assert.Throws<ConfigurationException>(() => dungeonGenerator.Generate());
            yield return null;
        }

        [UnityTest]
        public IEnumerator TilemapLayersStructure_Custom()
        {
            var dungeonGenerator = GetDungeonGenerator("Dungeon Generator Custom");
            Assert.IsNotNull(dungeonGenerator);

            dungeonGenerator.Generate();
            yield return null;

            AssertTilemapLayerExists("Floor Custom");
        }

        [UnityTest]
        public IEnumerator TilemapLayersStructure_CustomMissing()
        {
            var dungeonGenerator = GetDungeonGenerator("Dungeon Generator CustomMissing");
            Assert.IsNotNull(dungeonGenerator);

            Assert.Throws<ConfigurationException>(() => dungeonGenerator.Generate());
            yield return null;
        }

        [UnityTest]
        public IEnumerator TilemapMaterial()
        {
            var dungeonGenerator = GetDungeonGenerator("Dungeon Generator Material");
            Assert.IsNotNull(dungeonGenerator);
            Assert.That(dungeonGenerator.PostProcessConfig.TilemapMaterial, Is.Not.Null);

            dungeonGenerator.Generate();
            yield return null;

            var level = GetGeneratedLevelRoot();

            foreach (var renderer in level.GetComponentsInChildren<TilemapRenderer>())
            {
                Assert.That(renderer.material.name.Contains(dungeonGenerator.PostProcessConfig.TilemapMaterial.name));
            }
        }

        private void AssertTilemapLayerExists(string layerName)
        {
            var levelRoot = GetGeneratedLevelRoot();
            var tilemaps = RoomTemplateUtilsGrid2D.GetTilemaps(levelRoot);

            var tilemapLayer = tilemaps.SingleOrDefault(x => x.name == layerName);
            Assert.IsNotNull(tilemapLayer);
        }
    }
}