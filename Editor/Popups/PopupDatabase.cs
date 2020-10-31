using System.Collections.Generic;

namespace Edgar.Unity.Editor
{
    public static class PopupDatabase
    {
        public static ScenePopup GetExample1Popup()
        {
            const string sceneName = "Example1";

            var sb = new ScenePopupBuilder();
            sb.AppendHeading(sceneName);
            sb.AppendLine("In this scene, you can see a very basic setup of the dungeon generator.");

            return new ScenePopup(
                sceneName: sceneName,
                content: sb.ToString(),
                links: new List<PopupLink>()
                {
                    new PopupLink(PopupHelpers.GetDocsUrl("examples/example-1"), "Example 1 docs"),
                });
        }

        public static ScenePopup GetExample2Popup()
        {
            const string sceneName = "Example2";

            var sb = new ScenePopupBuilder();
            sb.AppendHeading(sceneName);
            sb.AppendLine("In this scene, you can see a very basic setup of the dungeon generator. This setup is slightly more complex than Example1 because of the tileset that is used here.");

            return new ScenePopup(
                sceneName: sceneName,
                content: sb.ToString(),
                links: new List<PopupLink>()
                {
                    new PopupLink(PopupHelpers.GetDocsUrl("examples/example-2"), "Example 2 docs"),
                });
        }

        public static ScenePopup GetCurrentRoomDetectionPopup()
        {
            const string sceneName = "CurrentRoomDetection";

            var sb = new ScenePopupBuilder();
            sb.AppendHeading(sceneName);
            sb.AppendLine("In this scene, you can see how to detect that a player moved from one room to another.");

            return new ScenePopup(
                sceneName: sceneName,
                content: sb.ToString(),
                links: new List<PopupLink>()
                {
                    new PopupLink(PopupHelpers.GetDocsUrl("guides/current-room-detection"), "Current room detection docs"),
                });
        }
    }
}