using UnityEngine;

namespace Edgar.Unity.Examples.Example1
{
    /// <summary>
    /// Example implementation of doors that are opened (disabled) after pressing E if the player is near enough.
    /// </summary>
    public class Example1Door : InteractableBase
    {
        /// <summary>
        /// Show text when the interaction begins (player is close to the doors).
        /// </summary>
        public override void BeginInteract()
        {
            ShowText("Press E to open doors");
        }

        /// <summary>
        /// Check for key press when the player is near.
        /// </summary>
        public override void Interact()
        {
            if (InputHelper.GetKey(KeyCode.E))
            {
                gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Hide text when the interaction ends (player gets further from doors).
        /// </summary>
        public override void EndInteract()
        {
            HideText();
        }
    }
}