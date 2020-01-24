using System;
using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    public class GungeonRoom : Room
    {
        public RoomType Type;

        public override string GetDisplayName()
        {
            return Type.ToString();
        }
    }
}

