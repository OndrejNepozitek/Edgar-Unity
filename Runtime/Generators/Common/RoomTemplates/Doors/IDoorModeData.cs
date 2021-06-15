using Edgar.GraphBasedGenerator.Grid2D;

namespace Edgar.Unity
{
    public interface IDoorModeData
    {
        IDoorModeGrid2D GetDoorMode(Doors doors);
    }
}