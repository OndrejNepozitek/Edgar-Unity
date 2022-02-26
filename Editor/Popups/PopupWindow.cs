using System;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    public class PopupWindow : EditorWindow
    {
        private static PopupWindow LastWindow;

        private IPopup popup;
        private bool doNotShowAgain = false;

        private void OnGUI()
        {
            if (popup == null)
            {
                Close();
                return;
            }

            const int margin = 10;
            GUILayout.BeginVertical(new GUIStyle() {padding = new RectOffset(margin, margin, margin, margin)});
            GUILayout.Label(popup.Content, new GUIStyle(EditorStyles.label) {richText = true, wordWrap = true});
            GUILayout.FlexibleSpace();

            if (popup.Links != null && popup.Links.Count > 0)
            {
                GUILayout.Label("Useful links:");

                foreach (var link in popup.Links)
                {
                    if (GUILayout.Button(" - link: " + link.Text, GUI.skin.label))
                    {
                        Application.OpenURL(link.Url);
                    }

                    var lastRect = GUILayoutUtility.GetLastRect();
                    GUI.Label(lastRect, "   ___");
                }
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Close"))
                Close();

            doNotShowAgain = GUILayout.Toggle(doNotShowAgain, "Do not show this popup again. <size=8>(All popups can be enabled again by going to \"Edit/Edgar - Enable all popups again\")</size>", new GUIStyle(EditorStyles.toggle) {richText = true});

            GUILayout.Space(10);

            GUILayout.Label("<b>! Warning !</b>: <size=9>Please do not create your game directly inside this scene. If you modify this scene, it will be harder for you to update the asset to new versions in the future. It is okay to use this scene as a playground. However, be prepared to lose all your modifications as I recommend to delete the whole asset folder when upgrading.</size>", new GUIStyle(EditorStyles.label) {richText = true, wordWrap = true});

            GUILayout.EndVertical();
        }

        private void OnDestroy()
        {
            if (doNotShowAgain)
            {
                PopupManager.DisablePopup(popup);
            }
        }

        public static void Open(IPopup popup)
        {
            CloseLastPopup();

            var window = ScriptableObject.CreateInstance<PopupWindow>();
            var size = new Vector2(600, 330);
            window.minSize = size;
            window.maxSize = size;
            window.titleContent = new GUIContent($"Edgar - {popup.Title}");
            window.popup = popup;
            window.ShowUtility();

            LastWindow = window;
        }

        public static void CloseLastPopup()
        {
            if (LastWindow != null)
            {
                LastWindow.Close();
            }
        }
    }
}