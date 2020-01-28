using System;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Utils;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators.Platformers
{
    /// <summary>
    ///     Actual implementation of the task that generates platfomers.
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    public class PlatformerGeneratorTask<TPayload> : GraphBasedGeneratorBaseTask<TPayload, PlatformerGeneratorConfig>
        where TPayload : class, IGeneratorPayload, IGraphBasedGeneratorPayload, IRandomGeneratorPayload
    {
        private readonly DungeonGeneratorUtils dungeonGeneratorUtils = new DungeonGeneratorUtils();

        public override void Process()
        {
            throw new InvalidOperationException("Platformers are currently not supported");

            //if (Config.Timeout <= 0)
            //{
            //	throw new ArgumentException("Timeout must be a positive number.");
            //}

            //if (Payload.MapDescription.IsWithCorridors)
            //{
            //	throw new ArgumentException("Platformer levels cannot have corridors.");
            //}

            //var stopwatch = new Stopwatch();
            //stopwatch.Start();

            //if (Config.ShowDebugInfo)
            //{
            //	Debug.Log("--- Generator started ---");
            //}

            //// Setup map description
            //var mapDescription = Payload.MapDescription;

            //// Generate layout
            //var layout = GenerateLayout(mapDescription, GetGenerator(mapDescription), Config.Timeout, Config.ShowDebugInfo);

            //// Setup room templates
            //Payload.Layout = TransformLayout(layout, Payload.RoomDescriptionsToRoomTemplates);

            //// Apply tempaltes
            //if (Config.ApplyTemplate)
            //{
            //	ApplyTemplates();
            //}

            //// Center grid
            //if (Config.CenterGrid)
            //{
            //	Payload.Tilemaps[0].CompressBounds();
            //	Payload.Tilemaps[0].transform.parent.position = -Payload.Tilemaps[0].cellBounds.center;
            //}

            //if (Config.ShowDebugInfo)
            //{
            //	Debug.Log($"--- Completed. {stopwatch.ElapsedMilliseconds / 1000f:F} s ---");
            //}
        }

        //protected IBenchmarkableLayoutGenerator<MapDescription<int>, IMapLayout<int>> GetGenerator(MapDescription<int> mapDescription)
        //{
        //	var generator = PlatformerGeneratorFactory.GetPlatformerGenerator<int>();
        //	generator.InjectRandomGenerator(Payload.Random);

        //	return generator;
        //}

        ///// <summary>
        ///// Copies tiles from individual room templates to the tilemaps that hold generated dungeons.
        ///// </summary>
        //protected void ApplyTemplates()
        //{
        //	var nonCorridors = Payload.Layout.GetAllRoomInfo().Where(x => !x.IsCorridor).ToList();
        //	var corridors = Payload.Layout.GetAllRoomInfo().Where(x => x.IsCorridor).ToList();

        //	dungeonGeneratorUtils.ApplyTemplates(nonCorridors, Payload.Tilemaps);
        //	dungeonGeneratorUtils.ApplyTemplates(corridors, Payload.Tilemaps);
        //}
    }
}