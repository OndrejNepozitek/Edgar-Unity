using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    public class GungeonRoom : Room
    {
        public RoomType Type;

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}