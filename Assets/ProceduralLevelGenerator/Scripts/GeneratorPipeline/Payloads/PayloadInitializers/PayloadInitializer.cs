namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.PayloadInitializers
{
	using System.Linq;
	using Data.Graphs;
	using RoomTemplates.TilemapLayers;
	using UnityEditor;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Random = System.Random;

	/// <summary>
	/// Basic payload initializer.
	/// </summary>
	[CreateAssetMenu(menuName = "Dungeon generator/Pipeline/Payload initializer", fileName = "PayloadInitializer")]
	public class PayloadInitializer : AbstractPayloadInitializer
	{
		public AbstractTilemapLayersHandler TilemapLayersHandler;

		public bool UseRandomSeed = true;

		public int RandomGeneratorSeed;

		public bool PrintUsedSeed = true;

		protected static readonly string DungeonHolderName = "Generated dungeon";

		protected readonly Random SeedsGenerator = new Random();

		/// <summary>
		/// Initializes payload.
		/// </summary>
		/// <returns></returns>
		public override object InitializePayload()
		{
			return InitializePayload<Room>();
		}

		/// <summary>
		/// Initializes payload with a given type of room.
		/// </summary>
		/// <returns></returns>
		protected object InitializePayload<TRoom>()
		{
			var dungeonHolder = GetDungeonHolder();
			var random = GetRandomNumbersGenerator();

			return new PipelinePayload<TRoom>()
			{
				GameObject = dungeonHolder,
				Tilemaps = dungeonHolder.GetComponentsInChildren<Tilemap>().ToList(),
				Random = random,
			};
		}

		/// <summary>
		/// Gets GameObject that holds generated dungeons.
		/// </summary>
		/// <returns></returns>
		protected virtual GameObject GetDungeonHolder()
		{
			// Destroy old dungeon holder
			var dungeonHolderOld = GameObject.Find(DungeonHolderName);

			if (dungeonHolderOld != null)
			{
				DestroyImmediate(dungeonHolderOld);
			}

			// Create new dungeon holder
			var dungeonHolder = new GameObject(DungeonHolderName);
			dungeonHolder.AddComponent<Grid>();

			// Initialize tilemaps
			var tilemapLayersHandler = TilemapLayersHandler;

			if (tilemapLayersHandler == null)
			{
				tilemapLayersHandler = AssetDatabase
					.LoadAssetAtPath<TilemapLayersHandler>("Assets/ProceduralLevelGenerator/ScriptableObjects/DefaultTilemapLayersHandler.asset");
			}

			tilemapLayersHandler.InitializeTilemaps(dungeonHolder);

			return dungeonHolder;
		}

		/// <summary>
		/// Gets random numbers generator.
		/// </summary>
		/// <returns></returns>
		protected virtual Random GetRandomNumbersGenerator()
		{
			var seed = UseRandomSeed ? SeedsGenerator.Next() : RandomGeneratorSeed;

			if (PrintUsedSeed)
			{
				Debug.Log($"Random generator seed: {seed}");
			}

			return new Random(seed);
		}
	}
}