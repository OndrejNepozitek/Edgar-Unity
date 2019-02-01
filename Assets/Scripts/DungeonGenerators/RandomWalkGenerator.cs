namespace Assets.Scripts.DungeonGenerators
{
	using System;
	using System.Collections.Generic;
	using GeneratorPipeline;
	using Payloads;
	using Pipeline;
	using UnityEngine;
	using Random = System.Random;

	[CreateAssetMenu(menuName = "Dungeon generator/Generators/Random walk generator", fileName = "Random walk generator")]
	public class RandomWalkGeneratorConfig : PipelineConfig
	{
		public int Width = 50;

		public int Height = 50;

		public int MaxTunnels = 20;

		public int MaxLength = 7;
	}

	public class RandomWalkGenerator<TPayload> : ConfigurablePipelineTask<TPayload, RandomWalkGeneratorConfig>
		where TPayload : class, IGeneratorPayload
	{
		public override void Process()
		{
			var remainingTunnels = Config.MaxTunnels;
			var possibleDirections = new List<Vector2Int>
			{
				Vector2Int.down,
				Vector2Int.left,
				Vector2Int.right,
				Vector2Int.up
			};
			var random = new Random();

			var posMax = new Vector2Int(Config.Width / 2, Config.Height / 2);
			var posMin = new Vector2Int(-Config.Width / 2, -Config.Height / 2);

			var currentPosition = new Vector2Int(random.Next(posMin.x, posMax.x + 1), random.Next(posMin.y, posMax.y + 1));

			while (remainingTunnels != 0)
			{
				var randomDirection = possibleDirections[random.Next(0, possibleDirections.Count)];
				var randomLength = random.Next(Config.MaxLength) + 1;
				var newPosition = currentPosition + randomDirection * randomLength;

				newPosition.x = Math.Min(newPosition.x, posMax.x);
				newPosition.y = Math.Min(newPosition.y, posMax.y);
				newPosition.x = Math.Max(newPosition.x, posMin.x);
				newPosition.y = Math.Max(newPosition.y, posMin.y);

				if (newPosition.x == currentPosition.x)
				{
					for (int i = Math.Min(newPosition.y, currentPosition.y); i <= Math.Max(newPosition.y, currentPosition.y); i++)
					{
						var position = new Vector3Int(newPosition.x, i, 0);
						var position2 = new Vector3Int(newPosition.x + 1, i, 0);

						Payload.MarkerMaps[0].SetMarker(position, new Marker() { Type = MarkerTypes.Wall });
						Payload.MarkerMaps[0].SetMarker(position2, new Marker() { Type = MarkerTypes.Wall }); 
					}
				}
				else
				{
					for (int i = Math.Min(newPosition.x, currentPosition.x); i <= Math.Max(newPosition.x, currentPosition.x); i++)
					{
						var position = new Vector3Int(i, newPosition.y, 0);
						var position2 = new Vector3Int(i, newPosition.y + 1, 0);

						Payload.MarkerMaps[0].SetMarker(position, new Marker() { Type = MarkerTypes.Wall });
						Payload.MarkerMaps[0].SetMarker(position2, new Marker() { Type = MarkerTypes.Wall });
					}
				}

				currentPosition = newPosition;
				remainingTunnels--;
			}
		}
	}
}