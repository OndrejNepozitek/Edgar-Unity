using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edgar.Unity.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Edgar.Unity.Tests.Runtime
{
    public class TimeoutDiagnosticTests : TestBase
    {
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            LoadScene("TimeoutDiagnostic");
            yield return null;
        }

        [Test]
        public void DifferentDoorLengths()
        {
            var dungeonGeneratorGameObject = GameObject.Find("DifferentDoorLengths");
            var dungeonGenerator = dungeonGeneratorGameObject.GetComponent<DungeonGeneratorGrid2D>();

            var exception = Assert.Throws<TimeoutException>(() => dungeonGenerator.Generate());
            var result = GetResult<DifferentLengthsOfDoors.Result>(exception);

            Assert.That(result.DoorLengths.Count, Is.EqualTo(2));
            Assert.That(result.IsPotentialProblem, Is.True);
        }

        [Test]
        public void DifferentDoorLengths2()
        {
            var dungeonGeneratorGameObject = GameObject.Find("DifferentDoorLengths 2");
            var dungeonGenerator = dungeonGeneratorGameObject.GetComponent<DungeonGeneratorGrid2D>();

            var exception = Assert.Throws<TimeoutException>(() => dungeonGenerator.Generate());
            var result = GetResult<DifferentLengthsOfDoors.Result>(exception);

            Assert.That(result.DoorLengths.Count, Is.EqualTo(3));
            Assert.That(result.IsPotentialProblem, Is.True);
        }

        [Test]
        public void TooShortTimeout()
        {
            var dungeonGeneratorGameObject = GameObject.Find("TooShortTimeout");
            var dungeonGenerator = dungeonGeneratorGameObject.GetComponent<DungeonGeneratorGrid2D>();

            var exception = Assert.Throws<TimeoutException>(() => dungeonGenerator.Generate());
            var result = GetResult<TimeoutLength.Result>(exception);

            Assert.That(result.Timeout, Is.EqualTo(1));
            Assert.That(result.IsPotentialProblem, Is.True);
        }

        [Test]
        public void NumberOfCycles()
        {
            var dungeonGeneratorGameObject = GameObject.Find("NumberOfCycles");
            var dungeonGenerator = dungeonGeneratorGameObject.GetComponent<DungeonGeneratorGrid2D>();

            var exception = Assert.Throws<TimeoutException>(() => dungeonGenerator.Generate());
            var result = GetResult<NumberOfCycles.Result>(exception, false);

            Assert.That(result.NumberOfCycles, Is.EqualTo(4));
            Assert.That(result.IsPotentialProblem, Is.True);
        }

        [Test]
        public void NumberOfRooms()
        {
            var dungeonGeneratorGameObject = GameObject.Find("NumberOfRooms");
            var dungeonGenerator = dungeonGeneratorGameObject.GetComponent<DungeonGeneratorGrid2D>();

            var exception = Assert.Throws<TimeoutException>(() => dungeonGenerator.Generate());
            var result = GetResult<NumberOfRooms.Result>(exception);

            Assert.That(result.NumberOfRooms, Is.EqualTo(24));
            Assert.That(result.IsPotentialProblem, Is.True);
        }

        [Test]
        public void WrongManualDoors()
        {
            var dungeonGeneratorGameObject = GameObject.Find("WrongManualDoors");
            var dungeonGenerator = dungeonGeneratorGameObject.GetComponent<DungeonGeneratorGrid2D>();

            var exception = Assert.Throws<TimeoutException>(() => dungeonGenerator.Generate());
            var result = GetResult<WrongManualDoors.Result>(exception, false);

            Assert.That(result.ProblematicRoomTemplates.Count, Is.EqualTo(1));
            Assert.That(result.IsPotentialProblem, Is.True);
        }

        [Test]
        public void MinimumRoomDistance()
        {
            var dungeonGeneratorGameObject = GameObject.Find("MinimumRoomDistance");
            var dungeonGenerator = dungeonGeneratorGameObject.GetComponent<DungeonGeneratorGrid2D>();

            var exception = Assert.Throws<TimeoutException>(() => dungeonGenerator.Generate());
            var result = GetResult<MinimumRoomDistance.Result>(exception, false);

            Assert.That(result.MinimumRoomDistance, Is.EqualTo(3));
            Assert.That(result.IsPotentialProblem, Is.True);
        }
        
        [Test]
        public void OddCycles()
        {
            var dungeonGeneratorGameObject = GameObject.Find("OddCycles");
            var dungeonGenerator = dungeonGeneratorGameObject.GetComponent<DungeonGeneratorGrid2D>();

            var exception = Assert.Throws<TimeoutException>(() => dungeonGenerator.Generate());
            var result = GetResult<OddCycles.Result>(exception, false);

            Assert.That(result.CycleLengths, Is.EquivalentTo(new List<int>() {3, 3, 5}));
            Assert.That(result.IsPotentialProblem, Is.True);
        }
        
        [Test]
        public void CorridorTypesUndefined()
        {
            var dungeonGeneratorGameObject = GameObject.Find("CorridorTypesUndefined");
            var dungeonGenerator = dungeonGeneratorGameObject.GetComponent<DungeonGeneratorGrid2D>();

            var exception = Assert.Throws<TimeoutException>(() => dungeonGenerator.Generate());
            var result = GetResult<CorridorTypes.Result>(exception);

            Assert.That(result.CorridorTypes, Is.EquivalentTo(new List<CorridorTypes.CorridorType>() { CorridorTypes.CorridorType.Undefined }));
            Assert.That(result.IsPotentialProblem, Is.True);
        }
        
        [Test]
        public void CorridorTypesHorizontal()
        {
            var dungeonGeneratorGameObject = GameObject.Find("CorridorTypesHorizontal");
            var dungeonGenerator = dungeonGeneratorGameObject.GetComponent<DungeonGeneratorGrid2D>();

            var exception = Assert.Throws<TimeoutException>(() => dungeonGenerator.Generate());
            var result = GetResult<CorridorTypes.Result>(exception);

            Assert.That(result.CorridorTypes, Is.EquivalentTo(new List<CorridorTypes.CorridorType>() { CorridorTypes.CorridorType.Horizontal }));
            Assert.That(result.IsPotentialProblem, Is.True);
        }
        
        [Test]
        public void NotEnoughDoorsNoFourSides()
        {
            var dungeonGeneratorGameObject = GameObject.Find("NotEnoughDoorsNoFourSides");
            var dungeonGenerator = dungeonGeneratorGameObject.GetComponent<DungeonGeneratorGrid2D>();

            var exception = Assert.Throws<TimeoutException>(() => dungeonGenerator.Generate());
            var result = GetResult<NotEnoughDoors.Result>(exception);

            Assert.That(result.MissingDoorsOnAllSides, Is.True);
            Assert.That(result.IsPotentialProblem, Is.True);
        }

        private TResult GetResult<TResult>(TimeoutException exception, bool allowOnlySingle = true) where TResult : class
        {
            if (allowOnlySingle && typeof(TResult) != typeof(TimeoutLength.Result))
            {
                var notTimeoutResults = exception
                    .DiagnosticResults
                    .Where(x => x.IsPotentialProblem)
                    .Where(x => !(x is TimeoutLength.Result))
                    .ToList();
                Assert.That(notTimeoutResults.Count, Is.EqualTo(1));
                Assert.That(notTimeoutResults[0], Is.TypeOf<TResult>());

                return notTimeoutResults[0] as TResult;
            }

            return exception.DiagnosticResults.Single(x => x is TResult) as TResult;
        }
    }
}