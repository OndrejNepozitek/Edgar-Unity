using UnityEngine;

namespace Edgar.Unity.Examples.Example1
{
    /// <summary>
    /// Example implementation of an exit is activated by pressing E and loads the next level.
    /// </summary>
    public class Example1Exit : InteractableBase
    {
        public override void BeginInteract()
        {
            ShowText("Press E to exit the level");
        }

        public override void Interact()
        {
            if (InputHelper.GetKey(KeyCode.E))
            {
                Example1GameManager.Instance.LoadNextLevel();
            }
        }

        public override void EndInteract()
        {
            HideText();
        }
    }
}