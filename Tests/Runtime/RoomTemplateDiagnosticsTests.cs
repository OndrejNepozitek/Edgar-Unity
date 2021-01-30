using System.Collections;
using Edgar.Unity.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Edgar.Unity.Tests.Runtime
{
    public class RoomTemplateDiagnosticsTests : TestBase
    {
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            LoadScene("RoomTemplateDiagnostics");
            yield return null;
        }

        [Test]
        public void NoRoomTemplateSettings()
        {
            var roomTemplate = GameObject.Find("NoRoomTemplateSettings");
            Assert.That(roomTemplate, Is.Not.Null);

            var resultAll = RoomTemplateDiagnostics.CheckAll(roomTemplate);
            Assert.That(resultAll.HasErrors, Is.True);

            var resultComponents = RoomTemplateDiagnostics.CheckComponents(roomTemplate);
            Assert.That(resultComponents.HasErrors, Is.True);
        }

        [Test]
        public void NoManualDoors()
        {
            var roomTemplate = GameObject.Find("NoManualDoors");
            Assert.That(roomTemplate, Is.Not.Null);

            var resultAll = RoomTemplateDiagnostics.CheckAll(roomTemplate);
            Assert.That(resultAll.HasErrors, Is.True);

            var resultComponents = RoomTemplateDiagnostics.CheckComponents(roomTemplate);
            Assert.That(resultComponents.HasErrors, Is.False);

            var resultDoors = RoomTemplateDiagnostics.CheckDoors(roomTemplate);
            Assert.That(resultDoors.HasErrors, Is.True);
        }

        [Test]
        public void NoSimpleDoors()
        {
            var roomTemplate = GameObject.Find("NoSimpleDoors");
            Assert.That(roomTemplate, Is.Not.Null);

            var resultAll = RoomTemplateDiagnostics.CheckAll(roomTemplate);
            Assert.That(resultAll.HasErrors, Is.True);

            var resultComponents = RoomTemplateDiagnostics.CheckComponents(roomTemplate);
            Assert.That(resultComponents.HasErrors, Is.False);

            var resultDoors = RoomTemplateDiagnostics.CheckDoors(roomTemplate);
            Assert.That(resultDoors.HasErrors, Is.True);
        }

        [Test]
        public void NoDoors()
        {
            var roomTemplate = GameObject.Find("NoDoors");
            Assert.That(roomTemplate, Is.Not.Null);

            var resultAll = RoomTemplateDiagnostics.CheckAll(roomTemplate);
            Assert.That(resultAll.HasErrors, Is.True);

            var resultComponents = RoomTemplateDiagnostics.CheckComponents(roomTemplate);
            Assert.That(resultComponents.HasErrors, Is.True);
        }

        [Test]
        public void ManualDoorsNotOnOutline()
        {
            var roomTemplate = GameObject.Find("ManualDoorsNotOnOutline");
            Assert.That(roomTemplate, Is.Not.Null);

            var resultAll = RoomTemplateDiagnostics.CheckAll(roomTemplate);
            Assert.That(resultAll.HasErrors, Is.True);

            var resultComponents = RoomTemplateDiagnostics.CheckComponents(roomTemplate);
            Assert.That(resultComponents.HasErrors, Is.False);

            var resultDoors = RoomTemplateDiagnostics.CheckDoors(roomTemplate);
            Assert.That(resultDoors.HasErrors, Is.True);
        }

        [Test]
        public void InvalidOutline()
        {
            var roomTemplate = GameObject.Find("InvalidOutline");
            Assert.That(roomTemplate, Is.Not.Null);

            var resultAll = RoomTemplateDiagnostics.CheckAll(roomTemplate);
            Assert.That(resultAll.HasErrors, Is.True);

            var resultComponents = RoomTemplateDiagnostics.CheckComponents(roomTemplate);
            Assert.That(resultComponents.HasErrors, Is.False);
        }
    }
}