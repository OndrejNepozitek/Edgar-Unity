using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Edgar.Legacy.Core.MapDescriptions;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Edgar.Unity.Diagnostics
{
    public static class LevelStructureDiagnostics
    {
        public static Result Analyze(DungeonGeneratorLevelGrid2D level)
        {
            // var sw = new Stopwatch();
            // sw.Start();
            
            var resultsPerObject = new List<KeyValuePair<string, List<KeyValue>>>
            {
                new KeyValuePair<string, List<KeyValue>>("Generated level", Analyze(level.RootGameObject))
            };
            
            var roomTemplates = level.RoomInstances.Select(x => x.RoomTemplatePrefab).Distinct().ToList();
            foreach (var roomTemplate in roomTemplates)
            {
                resultsPerObject.Add(new KeyValuePair<string, List<KeyValue>>(roomTemplate.name, Analyze(roomTemplate)));
            }

            var allResults = resultsPerObject
                .Select(x => x.Value)
                .SelectMany(x => x)
                .ToList();
            var identifiers = allResults
                .Select(x => x.GetIdentifier())
                .Distinct()
                .OrderBy(x => x)
                .ToList();
            
            var result = new Result();
            var sb = new StringBuilder();
            sb.AppendLine("!! Generated level structure warning !!");
            sb.AppendLine("It seems like the structure of the generated level game object does not match the structure of (some) room templates.");
            sb.AppendLine("For example, if you change a Tag or a Component on a tilemap layer, you must configure the generator to reflect that.");
            sb.AppendLine();
            sb.AppendLine("SOLUTION: The easiest solution is to go to the Generator component and set the Tilemap Layers Structure field to 'From Example'.");
            sb.AppendLine("Then, assign one of your room templates to the `Tilemap Layers Example` field to act as a template for the generated level.");
            sb.AppendLine("More information: https://ondrejnepozitek.github.io/Edgar-Unity/docs/other/faq/#changes-to-a-room-template-are-lost-after-a-level-is-generated");
            sb.AppendLine();
            sb.AppendLine("DETECTED PROBLEMS:");
            sb.AppendLine();
            
            foreach (var identifier in identifiers)
            {
                var exampleItem = allResults.First(x => x.GetIdentifier() == identifier);
                var valueToObjects = new Dictionary<string, List<string>>();
                var missingObjects = new List<string>();
                
                foreach (var results in resultsPerObject)
                {
                    var item = results.Value.FirstOrDefault(x => x.GetIdentifier() == identifier);
                    if (item != null)
                    {
                        if (!valueToObjects.TryGetValue(item.Value, out var values))
                        {
                            values = new List<string>();
                            valueToObjects.Add(item.Value, values);
                        }
                        values.Add(results.Key);
                    }
                    else
                    {
                        if (exampleItem.Type == KeyValueType.LayerComponent)
                        {
                            // We don't want to report missing components on the layer if the layer itself is missing
                            var layerExists = results.Value.Any(x => x.Type == KeyValueType.Layer && x.TilemapLayer == exampleItem.TilemapLayer);
                            if (layerExists)
                            {
                                missingObjects.Add(results.Key);
                            }
                        }
                        else if (exampleItem.Type == KeyValueType.Layer)
                        {
                            // Do not report missing outline override
                            if (exampleItem.TilemapLayer != GeneratorConstantsGrid2D.OutlineOverrideLayerName)
                            {
                                missingObjects.Add(results.Key);
                            }
                        }
                    }
                }

                switch (exampleItem?.Type)
                {
                    case KeyValueType.Layer:
                    {
                        if (missingObjects.Count > 0)
                        {
                            result.IsPotentialProblem = true;
                            sb.AppendLine($"Tilemap layer \"{exampleItem.TilemapLayer}\" missing:");
                            sb.AppendLine($" - missing in room templates: {FormatObjectNames(missingObjects)}");
                            sb.AppendLine($" - present in room templates: {FormatObjectNames(valueToObjects.Values.First())}");
                        }
                        
                        break;
                    }
                    
                    case KeyValueType.LayerComponent:
                    {
                        if (missingObjects.Count > 0)
                        {
                            result.IsPotentialProblem = true;
                            sb.AppendLine($"Tilemap layer \"{exampleItem.TilemapLayer}\" missing component \"{exampleItem.Key}\":");
                            sb.AppendLine($" - missing in room templates: {FormatObjectNames(missingObjects)}");
                            sb.AppendLine($" - present in room templates: {FormatObjectNames(valueToObjects.Values.First())}");
                        }
                        
                        break;
                    }
                    
                    case KeyValueType.LayerProperty:
                    {
                        if (valueToObjects.Count > 1)
                        {
                            result.IsPotentialProblem = true;
                            sb.AppendLine($"Tilemap layer \"{exampleItem.TilemapLayer}\" - difference in {exampleItem.Key} detected:");
                            foreach (var pair in valueToObjects)
                            {
                                sb.AppendLine($" - \"{pair.Key}\" in room templates: {FormatObjectNames(pair.Value)}");
                            }
                        }
                        
                        break;
                    }
                }
            }

            sb.AppendLine();
            sb.AppendLine("NOTE: You can disable this warning if you uncheck the 'Analyze Level Structure' field inside the generator component.");
            
            result.Summary = sb.ToString();
            
            // Debug.LogWarning(sw.Elapsed);

            return result;
        }
        
        private static string FormatObjectNames(List<string> names)
        {
            if (names.Count == 1)
            {
                return $"\"{names[0]}\"";
            }

            if (names.Count == 2)
            {
                return $"\"{names[0]}\", \"{names[1]}\"";
            }

            return $"\"{names[0]}\", \"{names[2]}\" and more";
        }

        private static List<KeyValue> Analyze(GameObject gameObject)
        {
            var result = new List<KeyValue>();
            var tilemaps = RoomTemplateUtilsGrid2D.GetTilemaps(gameObject);

            foreach (var tilemap in tilemaps)
            {
                var layerName = tilemap.gameObject.name;
                result.Add(new KeyValue(layerName, KeyValueType.Layer, null, layerName));
                result.Add(new KeyValue(layerName, KeyValueType.LayerProperty,"Tag", tilemap.gameObject.tag));
                result.Add(new KeyValue(layerName, KeyValueType.LayerProperty, "Layer", LayerMask.LayerToName(tilemap.gameObject.layer)));

                foreach (var component in tilemap.gameObject.GetComponents<Component>()) 
                {
                    result.Add(new KeyValue(layerName, KeyValueType.LayerComponent, component.GetType().Name, component.GetType().Name));
                }
            }
            
            return result;
        }
        
        private enum KeyValueType
        {
            Undefined,
            Layer,
            LayerProperty,
            LayerComponent,
        }

        private class KeyValue
        {
            /// <summary>
            /// Name of the layer that this concerns.
            /// </summary>
            public string TilemapLayer { get; }
            
            /// <summary>
            /// Type of the value.
            /// </summary>
            public KeyValueType Type { get; }
            
            /// <summary>
            /// Identifier of the tracked value. Can be:
            /// - null when tracking Layer
            /// - component name when tracking LayerComponent
            /// - property name when tracking LayerProperty
            /// </summary>
            public string Key { get; }
            
            /// <summary>
            /// Tracked value. Can be:
            /// - null for Layer and LayerComponent
            /// - property value for LayerProperty
            /// </summary>
            public string Value { get; }
            
            public KeyValue(string tilemapLayer, KeyValueType type, string key, string value)
            {
                TilemapLayer = tilemapLayer;
                Key = key;
                Value = value;
                Type = type;
            }

            public string GetIdentifier()
            {
                return $"{(int)Type}_{Type}_{TilemapLayer}_{Key}";
            }
        }

        public class Result : IDiagnosticResult
        {
            public string Name { get; set; }
            
            public string Summary { get; set; }
            
            public bool IsPotentialProblem { get; set; }
        }
    }
}