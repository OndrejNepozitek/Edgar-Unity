using System.Collections.Generic;

namespace Edgar.Unity.Editor
{
    public class ScenePopup : IPopup
    {
        public string Id { get; }

        public string SceneName { get; }

        public string ScenePath { get; }

        public string Title { get; }

        public string Content { get; }

        public List<PopupLink> Links { get; }

        public ScenePopup(string sceneName, string content, string id = null, string scenePath = null, string title = null, List<PopupLink> links = null)
        {
            SceneName = sceneName;
            Content = content;
            Id = id ?? $"scene_{sceneName}";
            ScenePath = scenePath ?? $"/{sceneName}/{sceneName}";
            Title = title ?? sceneName;
            Links = links;
        }
    }
}