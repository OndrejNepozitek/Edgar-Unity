using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Tasks
{
    public class RampartsPostProcessConfig : PipelineConfig
    {
        public int WallDepth = 50;

        public TileBase WallTile;
    }

    public class RampartsPostProcessTask<TPayload> : ConfigurablePipelineTask<TPayload, RampartsPostProcessConfig>
        where TPayload : class, IGraphBasedGeneratorPayload, IGeneratorPayload
    {
        private Tilemap wallsTilemap;

        public override void Process()
        {
            wallsTilemap = Payload.Tilemaps.Single(x => x.name == "Walls");

            foreach (var roomInstance in Payload.GeneratedLevel.GetAllRoomInstances())
            {
                AddWalls(roomInstance);
            }

            Payload.Tilemaps.Single(x => x.name == "Other 3").gameObject.SetActive(false);
        }

        private void AddWalls(RoomInstance roomInstance)
        {
            var roomTemplatePrefab = roomInstance.RoomTemplatePrefab;
            var tilemaps = roomTemplatePrefab.GetComponentsInChildren<Tilemap>().Where(x => x.name != "Other 3").ToList();
            var usedTiles = RoomTemplatesLoader.GetUsedTiles(tilemaps).Select(x => x.ToUnityIntVector3()); // TODO: make better
            var minY = usedTiles.Min(x => x.y);

            foreach (var pos in usedTiles.Where(x => x.y == minY))
            {
                for (int i = 1; i <= Config.WallDepth; i++)
                {
                    var wallPosition = roomInstance.Position + pos + Vector3Int.down * i;
                    wallsTilemap.SetTile(wallPosition, Config.WallTile);
                }
            }
        }
    }
}