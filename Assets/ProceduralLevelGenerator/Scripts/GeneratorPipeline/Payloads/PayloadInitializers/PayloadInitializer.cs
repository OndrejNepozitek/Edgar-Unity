using System;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.PayloadInitializers
{
	using System.Linq;
	using Data.Graphs;
	using RoomTemplates.TilemapLayers;
    #if UNITY_EDITOR
	using UnityEditor;
    #endif
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
				ParentGameObject = dungeonHolder,
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

            #if UNITY_EDITOR
			if (TilemapLayersHandler == null)
            {
                TilemapLayersHandler = AssetDatabase
                    .LoadAssetAtPath<TilemapLayersHandler>("Assets/ProceduralLevelGenerator/ScriptableObjects/DefaultTilemapLayersHandler.asset");
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
			}
            #endif

            if (TilemapLayersHandler == null)
            {
				throw new ArgumentNullException(nameof(TilemapLayersHandler), $"{nameof(TilemapLayersHandler)} must not be null");
            }

            // Initialize tilemaps
			TilemapLayersHandler.InitializeTilemaps(dungeonHolder);

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