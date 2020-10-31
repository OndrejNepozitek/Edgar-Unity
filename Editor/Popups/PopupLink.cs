namespace Edgar.Unity.Editor
{
    public class PopupLink
    {
        public string Url { get; }

        public string Text { get; }

        public PopupLink(string url, string text)
        {
            Url = url;
            Text = text;
        }
    }
}