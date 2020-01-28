using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.PrecomputedLevels
{
    public abstract class PrecomputedLevelsHandlerBase : AbstractPrecomputedLevelsHandler
    {
        public List<string> PrecomputedLevels;

        public override void OnComputationStarted()
        {
            PrecomputedLevels = new List<string>();
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
            PrecomputedLevels.Add(JsonConvert.SerializeObject(SaveLevelData(payload), new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameHandling = TypeNameHandling.All
            }));
        }

        protected abstract void LoadLevelData(object payload, string savedDataString);

        protected abstract object SaveLevelData(object payload);
    }
}