using Assets.ProceduralLevelGenerator.Examples.Common;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.Example2.Scripts
{
    public class Example2Chest : InteractableBase
    {
        public bool AlreadyOpened;

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
