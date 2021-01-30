using System;
using Edgar.Geometry;
using Edgar.GraphBasedGenerator.Grid2D;
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
            RoomTemplatesLoader.TryGetRoomTemplate(roomTemplate, out var _, out var result);
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

            if (roomTemplate.GetComponent<RoomTemplateSettings>() == null)
            {
                result.AddError($"The {nameof(RoomTemplateSettings)} component is missing on the room template game object.");
            }

            if (roomTemplate.GetComponent<Doors>() == null)
            {
                result.AddError($"The {nameof(Doors)} component is missing on the room template game object.");
            }

            return result;
        }

        /// <summary>
        /// Checks the doors of the room template.
        /// </summary>
        /// <param name="outline"></param>
        /// <param name="doorMode"></param>
        /// <returns></returns>
        public static ActionResult CheckDoors(PolygonGrid2D outline, IDoorModeGrid2D doorMode)
        {
            var result = new ActionResult();

            try
            {
                var doors = doorMode.GetDoors(outline);

                if (doors.Count == 0)
                {
                    if (doorMode is SimpleDoorModeGrid2D)
                    {
                        result.AddError($"The simple door mode is used but there are no valid door positions. Try to decrease door length and/or corner distance.");
                    }
                    else
                    {
                        result.AddError($"The manual door mode is used but no doors were added.");
                    }
                }
            }
            // TODO: this is not optimal - the argument exception might be something different than invalid manual doors
            catch (ArgumentException e)
            {
                if (doorMode is ManualDoorModeGrid2D)
                {
                    result.AddError($"It seems like some of the manual doors are not located on the outline of the room template.");
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
            var roomTemplateSettings = roomTemplate.GetComponent<RoomTemplateSettings>();
            var outline = roomTemplateSettings.GetOutline();

            var doors = roomTemplate.GetComponent<Doors>();

            if (roomTemplate.GetComponent<Doors>() == null)
            {
                var result = new ActionResult();
                result.AddError($"The {nameof(Doors)} component is missing on the room template game object.");
                return result;
            }

            var doorMode = doors.GetDoorMode();

            return CheckDoors(outline, doorMode);
        }
    }
}