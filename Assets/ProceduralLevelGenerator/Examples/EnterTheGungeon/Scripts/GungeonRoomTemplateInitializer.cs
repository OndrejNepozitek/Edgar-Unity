using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.RoomTemplateInitializers;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    public class GungeonRoomTemplateInitializer : BaseRoomTemplateInitializer
    {
        public override void Initialize()
        {
            base.Initialize();

            // Custom behaviour
            gameObject.AddComponent<RoomManager>();
        }

        protected override void InitializeTilemaps(GameObject tilemapsRoot)
        {
            var tilemapLayersHandler = ScriptableObject.CreateInstance<GungeonTilemapLayersHandlerBase>();
            tilemapLayersHandler.InitializeTilemaps(tilemapsRoot);

            // Custom behaviour
            tilemapsRoot.transform.Find("Floor").gameObject.AddComponent<RoomEnterHandler>();
        }
    }
}