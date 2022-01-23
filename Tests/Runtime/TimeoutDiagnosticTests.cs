using System.Collections;
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
            var result = GetResult<NumberOfCycles.Result>(exception);

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

            Assert.That(result.NumberOfRooms, Is.EqualTo(25));
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
            var result = GetResult<MinimumRoomDistance.Result>(exception);

            Assert.That(result.MinimumRoomDistance, Is.EqualTo(3));
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