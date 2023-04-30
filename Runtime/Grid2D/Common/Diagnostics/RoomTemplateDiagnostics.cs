using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edgar.Geometry;
using Edgar.GraphBasedGenerator.Grid2D;
using Edgar.GraphBasedGenerator.Grid2D.Exceptions;
using UnityEngine;

namespace Edgar.Unity.Diagnostics
{
    public static class RoomTemplateDiagnostics
    {
        /// <summary>
        /// Tries to compute a room template from a given game object and returns the result.
        /// </summary>
        /// <param name="roomTemplate"></param>
        /// <returns></returns>
        public static ActionResult CheckAll(GameObject roomTemplate)
        {
            RoomTemplateLoaderGrid2D.TryGetRoomTemplate(roomTemplate, out var _, out var result);
            return result;
        }

        /// <summary>
        /// Checks that the room template has all the necessary components.
        /// </summary>
        /// <param name="roomTemplate"></param>
        /// <returns></returns>
        public static ActionResult CheckComponents(GameObject roomTemplate)
        {
            var result = new ActionResult();

            if (roomTemplate.GetComponent<RoomTemplateSettingsGrid2D>() == null)
            {
                result.AddError($"The {nameof(RoomTemplateSettingsGrid2D)} component is missing on the room template game object.");
            }

            if (roomTemplate.GetComponent<DoorsGrid2D>() == null)
            {
                result.AddError($"The {nameof(DoorsGrid2D)} component is missing on the room template game object.");
            }

            return result;
        }

        /// <summary>
        /// Checks the doors of the room template.
        /// </summary>
        /// <param name="outline"></param>
        /// <param name="doorMode"></param>
        /// <param name="selectedDoorMode"></param>
        /// <returns></returns>
        public static ActionResult CheckDoors(PolygonGrid2D outline, IDoorModeGrid2D doorMode, DoorsGrid2D.DoorMode selectedDoorMode)
        {
            var result = new ActionResult();

            try
            {
                var doors = doorMode.GetDoors(outline);

                if (doors.Count == 0)
                {
                    if (selectedDoorMode == DoorsGrid2D.DoorMode.Simple)
                    {
                        result.AddError(
                            $"The simple door mode is used but there are no valid door positions. Try to decrease door length and/or margin.");
                    }
                    else
                    {
                        result.AddError($"The {selectedDoorMode} door mode is used but no doors were added.");
                    }
                }
            }
            catch (DoorLineOutsideOfOutlineException e)
            {
                if (selectedDoorMode == DoorsGrid2D.DoorMode.Manual)
                {
                    result.AddError(
                        $"It seems like some of the manual doors are not located on the outline of the room template.");
                }
                else if (selectedDoorMode == DoorsGrid2D.DoorMode.Hybrid)
                {
                    result.AddError(
                        $"It seems like some of the hybrid door lines are not located on the outline of the room template.");
                }
                else
                {
                    result.AddError(e.Message);
                }
            }
            catch (DuplicateDoorPositionException e)
            {
                if (selectedDoorMode == DoorsGrid2D.DoorMode.Hybrid)
                {
                    result.AddError("There are duplicate/overlapping door lines with the same door length and socket.");
                }
                else
                {
                    result.AddError(e.Message);
                }
            }

            return result;
        }

        /// <summary>
        /// Checks the doors of the room template.
        /// </summary>
        /// <param name="roomTemplate"></param>
        /// <returns></returns>
        public static ActionResult CheckDoors(GameObject roomTemplate)
        {
            var roomTemplateSettings = roomTemplate.GetComponent<RoomTemplateSettingsGrid2D>();
            var outline = roomTemplateSettings.GetOutline();
            var doors = roomTemplate.GetComponent<DoorsGrid2D>();

            if (doors == null)
            {
                var result = new ActionResult();
                result.AddError($"The {nameof(DoorsGrid2D)} component is missing on the room template game object.");
                return result;
            }

            var doorMode = doors.GetDoorMode();

            return CheckDoors(outline, doorMode, doors.SelectedMode);
        }

        public static ActionResult CheckWrongManualDoors(PolygonGrid2D outline, IDoorModeGrid2D doorMode, out int differentLengthsCount)
        {
            var result = new ActionResult();
            differentLengthsCount = -1;

            if (doorMode is ManualDoorModeGrid2D)
            {
                var doors = doorMode.GetDoors(outline);
                var differentLengths = doors.Select(x => x.Length).Distinct().ToList();
                differentLengthsCount = differentLengths.Count;

                if (differentLengthsCount >= 3)
                {
                    result.AddError($"There are {differentLengthsCount} different lengths of manual doors. Please make sure that this is intentional. This is often caused by an incorrect use of the manual door (see the warning in the Doors component).");
                }
            }

            return result;
        }

        public static ActionResult CheckWrongManualDoors(GameObject roomTemplate, out int differentLengthsCount)
        {
            var roomTemplateSettings = roomTemplate.GetComponent<RoomTemplateSettingsGrid2D>();
            var outline = roomTemplateSettings.GetOutline();
            var doors = roomTemplate.GetComponent<DoorsGrid2D>();
            var doorMode = doors.GetDoorMode();

            return CheckWrongManualDoors(outline, doorMode, out differentLengthsCount);
        }

        private static bool IsAtOrigin(GameObject gameObject)
        {
            return gameObject.transform.localPosition == Vector3.zero;
        }

        public static List<GameObject> GetWrongPositionGameObjects(GameObject roomTemplate)
        {
            var result = new List<GameObject>();

            if (!IsAtOrigin(roomTemplate))
            {
                result.Add(roomTemplate);
            }

            var tilemapsRoot = RoomTemplateUtilsGrid2D.GetTilemapsRoot(roomTemplate);

            if (tilemapsRoot != roomTemplate && !IsAtOrigin(tilemapsRoot))
            {
                result.Add(tilemapsRoot);
            }

            foreach (var tilemap in RoomTemplateUtilsGrid2D.GetTilemaps(roomTemplate))
            {
                if (!IsAtOrigin(tilemap.gameObject))
                {
                    result.Add(tilemap.gameObject);
                }
            }

            return result;
        }
        
        public static ActionResult CheckWrongPositionGameObjects(GameObject roomTemplate)
        {
            var result = new ActionResult();
            var wrongPositionGameObjects = GetWrongPositionGameObjects(roomTemplate);

            if (wrongPositionGameObjects.Count != 0)
            {
                result.AddError($"Some game objects that are important for the room template are not positioned at (0,0,0), which can cause some weird issues. The problematic game objects are: {string.Join(", ", wrongPositionGameObjects.Select(x => x.name))}.");
            }

            return result;
        }
    }
}