using System.Collections.Generic;

namespace Edgar.Unity.Editor
{
    public class Popup
    {
        public virtual string Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Content { get; set; }

        public virtual List<PopupLink> Links { get; set; }
    }
}