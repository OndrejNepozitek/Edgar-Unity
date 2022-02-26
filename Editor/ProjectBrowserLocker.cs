using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    /// <summary>
    /// The goal of this class is to lock the project view before an object (usually a room)
    /// is selected. It prevents the project view from changing the active folder. After the
    /// object is selected, the project view is unlocked.
    /// </summary>
    internal static class ProjectBrowserLocker
    {
        private static readonly MethodInfo GetAllProjectsBrowsersMethod;
        private static readonly PropertyInfo IsLockedProperty;
        private static readonly bool CanBeUsed;

        private static readonly List<EditorWindow> WindowsToUnlock = new List<EditorWindow>();

        static ProjectBrowserLocker()
        {
            var projectBrowserType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.ProjectBrowser");

            if (projectBrowserType == null)
            {
                return;
            }

            GetAllProjectsBrowsersMethod = projectBrowserType.GetMethod("GetAllProjectBrowsers", BindingFlags.Static | BindingFlags.Public);
            IsLockedProperty = projectBrowserType.GetProperty("isLocked", BindingFlags.NonPublic | BindingFlags.Instance);

            if (GetAllProjectsBrowsersMethod != null && IsLockedProperty != null)
            {
                CanBeUsed = true;
            }
        }

        public static void Lock()
        {
            if (!CanBeUsed)
            {
                return;
            }

            var projectBrowsers = (IEnumerable) GetAllProjectsBrowsersMethod.Invoke(null, Array.Empty<object>());

            foreach (EditorWindow projectBrowser in projectBrowsers)
            {
                var isLocked = (bool) IsLockedProperty.GetValue(projectBrowser);

                if (!isLocked)
                {
                    IsLockedProperty.SetValue(projectBrowser, true);
                    WindowsToUnlock.Add(projectBrowser);
                }
            }
        }

        public static void Unlock()
        {
            foreach (var projectBrowser in WindowsToUnlock)
            {
                IsLockedProperty.SetValue(projectBrowser, false);
            }

            WindowsToUnlock.Clear();
        }
    }
}