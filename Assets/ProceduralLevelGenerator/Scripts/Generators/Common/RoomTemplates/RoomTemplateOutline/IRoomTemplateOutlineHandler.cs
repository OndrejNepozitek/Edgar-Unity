using Assets.ProceduralLevelGenerator.Scripts.Utils;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.RoomTemplateOutline
{
    public interface IRoomTemplateOutlineHandler
    {
        Polygon2D GetRoomTemplateOutline();
    }
}