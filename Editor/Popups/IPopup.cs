using System.Collections.Generic;

namespace Edgar.Unity.Editor
{
    public interface IPopup
    {
        string Id { get; }

        string Title { get; }

        string Content { get; }

        List<PopupLink> Links { get; }
    }
}