using Edgar.GraphBasedGenerator.Grid2D;

namespace Edgar.Unity
{
    public interface IDoorModeDataGrid2D
    {
        IDoorModeGrid2D GetDoorMode(DoorsGrid2D doors);
    }
}