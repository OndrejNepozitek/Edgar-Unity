using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;

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
