using ProceduralLevelGenerator.Unity.Examples.Common;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Examples.Example2.Scripts
{
    /// <summary>
    /// Example implementation of a chest that is opened (sprite change) when the players interacts with it.
    /// </summary>
    public class Example2Chest : InteractableBase
    {
        public bool AlreadyOpened;

        /// <summary>
        /// Make sure to not make it possible to interact with the chest when it is already opened.
        /// </summary>
        /// <returns></returns>
        public override bool IsInteractionAllowed()
        {
            return !AlreadyOpened;
        }

        public override void BeginInteract()
        {
            ShowText("Press E to open chest");
        }

        public override void Interact()
        {
            if (Input.GetKey(KeyCode.E))
            {
                gameObject.transform.Find("Closed").gameObject.SetActive(false);
                gameObject.transform.Find("Open").gameObject.SetActive(true);
                AlreadyOpened = true;
            }
        }

        public override void EndInteract()
        {
            HideText();
        }
    }
}
