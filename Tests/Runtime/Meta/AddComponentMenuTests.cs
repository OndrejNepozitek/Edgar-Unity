using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEngine;

namespace Edgar.Unity.Tests.Runtime.Meta
{
    public class AddComponentMenuTests
    {
        [Test]
        public void CheckMissingAttribute()
        {
            var components = typeof(DungeonGeneratorGrid2D)
                .Assembly
                .GetTypes()
                .Where(x => typeof(MonoBehaviour).IsAssignableFrom(x))
                .ToList();

            var missingAttributes = new List<Type>();

            foreach (var component in components)
            {
                // Ignore abstract classes
                if (component.IsAbstract)
                {
                    continue;
                }

                var attribute = component.GetCustomAttribute<AddComponentMenu>(false);

                if (attribute == null)
                {
                    missingAttributes.Add(component);
                }
            }

            Assert.That(missingAttributes, Is.Empty, () => $"The following components are missing the AddComponentMenu attribute:\n{string.Join("\n", missingAttributes)}");
        }

        [Test]
        public void CheckCorrectMenuPath()
        {
            var components = typeof(DungeonGeneratorGrid2D)
                .Assembly
                .GetTypes()
                .Where(x => typeof(MonoBehaviour).IsAssignableFrom(x))
                .ToList();

            var possibleSuffixes = new List<string>() {"Grid2D", "Grid3D"};
            var wrongMenuPaths = new List<string>();

            foreach (var component in components)
            {
                // Ignore abstract classes
                if (component.IsAbstract)
                {
                    continue;
                }

                var attribute = component.GetCustomAttribute<AddComponentMenu>(false);

                if (attribute == null)
                {
                    continue;
                }

                var componentMenu = attribute.componentMenu;
                var componentNameWithoutSuffix = componentMenu;
                var usedSuffix = "";

                foreach (var suffix in possibleSuffixes)
                {
                    if (component.Name.EndsWith(suffix))
                    {
                        componentNameWithoutSuffix = component.Name.Substring(0, component.Name.Length - suffix.Length);
                        usedSuffix = suffix;
                        break;
                    }
                }

                if (usedSuffix == "")
                {
                    continue;
                }

                var splitByCamelCase = string.Join(" ", SplitCamelCase(componentNameWithoutSuffix));
                var expectedMenu = $"Edgar/{usedSuffix}/{splitByCamelCase} ({usedSuffix})";
                var expectedInternalMenu = $"Edgar/{usedSuffix}/_Internal/{splitByCamelCase} ({usedSuffix})";

                if (componentMenu != expectedMenu && componentMenu != expectedInternalMenu)
                {
                    wrongMenuPaths.Add($"{component.Name}: actual '{componentMenu}', expected '{expectedMenu}'");
                }
            }

            Assert.That(wrongMenuPaths, Is.Empty, () => $"The following components have wrong path in the AddComponentMenu attribute:\n{string.Join("\n", wrongMenuPaths)}");
        }

        private static string[] SplitCamelCase(string source)
        {
            return Regex.Split(source, @"(?<!^)(?=[A-Z])");
        }
    }
}