using System;
using System.Collections.Generic;
using System.Linq;
using Edgar.Geometry;
using Edgar.GraphBasedGenerator.Common.Doors;
using Edgar.GraphBasedGenerator.Grid2D;

namespace Edgar.Unity
{
    [Serializable]
    public class SimpleDoorModeDataGrid2D : IDoorModeDataGrid2D
    {
        public int DistanceFromCorners = 1;

        public int DoorLength = 1;

        public SettingsMode Mode;

        public SimpleDoorModeSettingsGrid2D VerticalDoors;

        public SimpleDoorModeSettingsGrid2D HorizontalDoors;

        public enum SettingsMode
        {
            Basic,
            DifferentHorizontalAndVertical
        }

        public IDoorModeGrid2D GetDoorMode(DoorsGrid2D doors)
        {
            var doorLines = GetDoorLines(doors);

            if (doorLines == null)
            {
                throw new ArgumentException("The provided simple door mode is incorrect.");
            }

            var transformedDoorLines = doorLines
                .Select(x => x.ToInternal())
                .ToList();

            return new ManualDoorModeGrid2D(transformedDoorLines);
        }

        public List<DoorLineGrid2D> GetDoorLines(DoorsGrid2D doors)
        {
            var doorLines = new List<DoorLineGrid2D>();

            try
            {
                var polygon = RoomTemplateLoaderGrid2D.GetPolygonFromRoomTemplate(doors.gameObject);

                if (polygon == null)
                {
                    return null;
                }

                foreach (var originalLine in polygon.GetLines())
                {
                    var line = originalLine;
                    var settings = GetSettings(line);

                    if (line.Length - settings.Margin1 - settings.Margin2 < settings.Length - 1)
                    {
                        continue;
                    }

                    if (!settings.Enabled)
                    {
                        continue;
                    }

                    if (line.GetDirection() == OrthogonalLineGrid2D.Direction.Bottom ||
                        line.GetDirection() == OrthogonalLineGrid2D.Direction.Left)
                    {
                        line = line.SwitchOrientation();
                    }

                    var doorLineTemp = line.Shrink(settings.Margin1, settings.Margin2);
                    var doorLine = new DoorLineGrid2D()
                    {
                        From = doorLineTemp.From.ToUnityIntVector3(),
                        To = doorLineTemp.To.ToUnityIntVector3(),
                        Length = settings.Length,
                    };
                    doorLines.Add(doorLine);
                }
            }
            catch (InvalidOutlineException)
            {
            }

            return doorLines;
        }

        private SimpleDoorModeSettingsGrid2D GetSettings(OrthogonalLineGrid2D line)
        {
            if (Mode == SettingsMode.Basic)
            {
                var data = this;

                return new SimpleDoorModeSettingsGrid2D()
                {
                    Length = data.DoorLength,
                    Margin1 = data.DistanceFromCorners,
                    Margin2 = data.DistanceFromCorners,
                };
            }

            return line.GetDirectionVector().X != 0 ? HorizontalDoors : VerticalDoors;
        }
    }
}