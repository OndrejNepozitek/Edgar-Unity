using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;

namespace Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Levels
{
    public class DeadCellsRoom : Room
    {
        public DeadCellsRoomType Type;

        public bool Outside;

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
