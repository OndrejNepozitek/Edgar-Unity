using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.PayloadInitializers;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.ProceduralLevelGraphs.Scripts
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Procedural level graphs/Payload initializer", fileName = "PayloadInitializer")]
    public class CustomPayloadInitializer : PayloadInitializer
    {
        public override object InitializePayload()
        {
            return InitializePayload<Room>();
        }
    }
}