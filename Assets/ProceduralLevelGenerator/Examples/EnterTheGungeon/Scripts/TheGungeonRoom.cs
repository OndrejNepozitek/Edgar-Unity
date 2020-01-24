using System;
using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    public class TheGungeonRoom : Room
    {
        public RoomType Type;

        public override string GetDisplayName()
        {
            return Type.ToString();
        }
    }
}

