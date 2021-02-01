using System.Text;

namespace Edgar.Unity.Editor
{
    public class ScenePopupBuilder
    {
        private readonly StringBuilder sb = new StringBuilder();

        public void AppendHeading(string sceneName)
        {
            AppendLine(PopupHelpers.GetSceneHeading(sceneName));
        }

        public void AppendLine(string line = "")
        {
            sb.AppendLine(line);
        }

        public void BeginAdditionalSteps()
        {
            AppendLine("This scene requires some additional steps from you to work properly.");
            AppendLine($"<size={PopupHelpers.AdditionalStepsSize}><b>Before running the example, please do the following:</b>");
        }

        public void EndAdditionalSteps()
        {
            AppendLine("</size>");
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}