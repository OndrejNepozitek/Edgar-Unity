using System.Text;

namespace Edgar.Unity.Editor
{
    public class PopupHelpers
    {
        public static readonly string DocsUrl = "https://ondrejnepozitek.github.io/Edgar-Unity/docs/";

        public static readonly int HeadingSize = 15;

        public static readonly int HeadingLargeSize = 18;

        public static readonly int AdditionalStepsSize = 13;

        public static string GetDocsUrl(string path)
        {
            return $"{DocsUrl}{path}";
        }

        public static string GetSceneHeading(string scene)
        {
            return $"<size={HeadingSize}>Welcome to the <size={HeadingLargeSize}><b>{scene}</b></size> example scene!</size>";
        }
    }
}