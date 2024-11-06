using System.Collections;
using Edgar.Unity.Diagnostics;
using Edgar.Unity.Tests;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Edgar.Unity.Edgar.Tests.Runtime
{
    public class LevelStructureTests : TestBase
    {
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            LoadScene("LevelStructure");
            yield return null;
        }
        
        [Test]
        public void LevelStructure()
        {
            var generator = GetDungeonGenerator();
            var payload = (DungeonGeneratorPayloadGrid2D) generator.Generate();
            var level = payload.GeneratedLevel;
            var result = LevelStructureDiagnostics.Analyze(level);
            
            Assert.That(result.IsPotentialProblem, Is.True);
            Assert.That(result.Summary.Replace("\r\n", "\n"), Is.EqualTo("!! Generated level structure warning !!\nIt seems like the structure of the generated level game object does not match the structure of (some) room templates.\nFor example, if you change a Tag or a Component on a tilemap layer, you must configure the generator to reflect that.\n\nSOLUTION: The easiest solution is to go to the Generator component and set the Tilemap Layers Structure field to 'From Example'.\nThen, assign one of your room templates to the `Tilemap Layers Example` field to act as a template for the generated level.\nMore information: https://ondrejnepozitek.github.io/Edgar-Unity/docs/other/faq/#changes-to-a-room-template-are-lost-after-a-level-is-generated\n\nDETECTED PROBLEMS:\n\nTilemap layer \"Walls\" missing:\n - missing in room templates: \"Missing Walls\"\n - present in room templates: \"Generated level\", \"Different Tag\" and more\nTilemap layer \"Collideable\" - difference in Tag detected:\n - \"Untagged\" in room templates: \"Generated level\", \"Baseline\" and more\n - \"Respawn\" in room templates: \"Different Tag\"\n - \"EditorOnly\" in room templates: \"Different Tag 2\"\nTilemap layer \"Floor\" - difference in Layer detected:\n - \"Default\" in room templates: \"Generated level\", \"Baseline\" and more\n - \"Water\" in room templates: \"Different Layer\"\nTilemap layer \"Other 3\" missing component \"Animator\":\n - missing in room templates: \"Generated level\", \"Baseline\" and more\n - present in room templates: \"Extra Component\"\n\nNOTE: You can disable this warning if you uncheck the 'Analyze Level Structure' field inside the generator component.\n"));
        }
    }
}