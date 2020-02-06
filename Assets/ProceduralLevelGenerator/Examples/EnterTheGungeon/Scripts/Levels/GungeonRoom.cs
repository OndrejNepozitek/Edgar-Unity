using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts.Levels
{
    public class GungeonRoom : Room
    {
        public GungeonRoomType Type;

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}