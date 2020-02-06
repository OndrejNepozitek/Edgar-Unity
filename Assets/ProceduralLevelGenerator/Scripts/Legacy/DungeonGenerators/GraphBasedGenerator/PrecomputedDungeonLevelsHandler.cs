using System;
using System.Collections.Generic;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Legacy.PrecomputedLevels;
using MapGeneration.Interfaces.Core.MapDescriptions;
using MapGeneration.Interfaces.Core.MapLayouts;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Random = System.Random;
#if UNITY_EDITOR

#endif

namespace Assets.ProceduralLevelGenerator.Scripts.Legacy.DungeonGenerators.GraphBasedGenerator
{
    // TODO: should be probably located somewhere else
    [CreateAssetMenu(menuName = "Dungeon generator/Generators/Precomputed dungeon levels", fileName = "PrecomputeDungeonLevels")]
    public class PrecomputedDungeonLevelsHandler : AbstractPrecomputedLevelsHandler
    {
        public List<SavedData> PrecomputedLevels;

        public override void OnComputationStarted()
        {
            PrecomputedLevels = new List<SavedData>();
        }

        public override void OnComputationEnded()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
#endif
        }

        public override void LoadLevel(object payload)
        {
            // TODO: how to handle randomness here?
            var random = new Random();
            var precomputedLevelString = PrecomputedLevels[random.Next(PrecomputedLevels.Count)];

            LoadLevelData(payload, precomputedLevelString);
        }

        public override void SaveLevel(object payload)
        {
            PrecomputedLevels.Add(SaveLevelData(payload));
        }

        protected void LoadLevelData(object payload, SavedData savedData)
        {
            // TODO: handle names
            var jsonSerializedData = JsonConvert.DeserializeObject<JsonSerializedData>(savedData.JsonSerializedData, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameHandling = TypeNameHandling.Auto
            });

            var generatorPayload = payload as IGraphBasedGeneratorPayload;

            if (savedData == null || generatorPayload == null)
            {
                throw new InvalidOperationException();
            }

            // TODO: probably serialize and deserialize LevelDescription
            throw new NotImplementedException();
            //generatorPayload.GeneratedLayout = jsonSerializedData.GeneratedLayout;
            //generatorPayload.RoomDescriptionsToRoomTemplates = new TwoWayDictionary<IRoomTemplate, GameObject>();

            //for (int i = 0; i < jsonSerializedData.RoomTemplates.Count; i++)
            //{
            //    generatorPayload.RoomDescriptionsToRoomTemplates.Add(jsonSerializedData.RoomTemplates[i], savedData.RoomTemplateGameObjects[i]);
            //}
        }

        protected SavedData SaveLevelData(object payload)
        {
            var generatorPayload = payload as IGraphBasedGeneratorPayload;

            if (generatorPayload == null)
            {
                throw new InvalidOperationException(
                    $"This precomputed levels handler can only work with payload implementing {nameof(IGraphBasedGeneratorPayload)}");
            }

            return new SavedData();
        }

        [Serializable]
        public class SavedData
        {
            public string JsonSerializedData;

            public List<GameObject> RoomTemplateGameObjects;
        }

        public class JsonSerializedData
        {
            public IMapLayout<Room> GeneratedLayout { get; set; }

            public List<IRoomTemplate> RoomTemplates { get; set; }
        }
    }
}