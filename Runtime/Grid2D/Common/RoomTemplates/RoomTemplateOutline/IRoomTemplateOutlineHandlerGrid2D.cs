namespace Edgar.Unity
{
    /// <summary>
    /// Interface for custom room template outline handlers.
    /// </summary>
    public interface IRoomTemplateOutlineHandlerGrid2D
    {
        /// <summary>
        /// Gets the outline of the room template.
        /// </summary>
        /// <returns></returns>
        Polygon2D GetRoomTemplateOutline();
    }
}