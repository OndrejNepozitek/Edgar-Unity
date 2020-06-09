using System.Collections;
using System.Diagnostics;
using ProceduralLevelGenerator.Unity.Examples.Common;
using ProceduralLevelGenerator.Unity.Generators.Common.Rooms;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator;
using UnityEngine;
using UnityEngine.UI;

namespace ProceduralLevelGenerator.Unity.Examples.CurrentRoomDetection.Scripts
{
    public class CurrentRoomDetectionGameManager : GameManagerBase<CurrentRoomDetectionGameManager>
    {
        // Current active room
        private RoomInstance currentRoom;

        // The room that will be active after the player leaves the current room
        private RoomInstance nextCurrentRoom;

        public void Update()
        {
            if (Input.GetKey(KeyCode.G))
            {
                LoadNextLevel();
            }
        }

        public override void LoadNextLevel()
        {
            currentRoom = null;
            nextCurrentRoom = null;

            // Show loading screen
            ShowLoadingScreen("Example 1", "loading..");

            // Find the generator runner
            var generator = GameObject.Find("Dungeon Generator").GetComponent<DungeonGenerator>();

            // Start the generator coroutine
            StartCoroutine(GeneratorCoroutine(generator));
        }

        /// <summary>
        /// Coroutine that generates the level.
        /// We need to yield return before the generator starts because we want to show the loading screen
        /// and it cannot happen in the same frame.
        /// It is also sometimes useful to yield return before we hide the loading screen to make sure that
        /// all the scripts that were possibly created during the process are properly initialized.
        /// </summary>
        private IEnumerator GeneratorCoroutine(DungeonGenerator generator)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            yield return null;

            generator.Generate();

            yield return null;

            stopwatch.Stop();

            SetLevelInfo($"Generated in {stopwatch.ElapsedMilliseconds/1000d:F}s");
            HideLoadingScreen();
        }

        public void OnRoomEnter(RoomInstance roomInstance)
        {
            nextCurrentRoom = roomInstance;

            if (currentRoom == null)
            {
                currentRoom = nextCurrentRoom;
                nextCurrentRoom = null;
                UpdateCurrentRoomInfo();
            }
        }

        public void OnRoomLeave(RoomInstance roomInstance)
        {
            currentRoom = nextCurrentRoom;
            nextCurrentRoom = null;
            UpdateCurrentRoomInfo();
        }

        private void UpdateCurrentRoomInfo()
        {
            var canvas = GetCanvas();
            var currentRoomInfo = canvas.transform.Find("CurrentRoomInfo").GetComponent<Text>();
            currentRoomInfo.text = $"Room name: {currentRoom?.Room.GetDisplayName()}, Room template: {currentRoom?.RoomTemplatePrefab.name}";
        }
    }
}
