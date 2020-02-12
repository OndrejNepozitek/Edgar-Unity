using Assets.ProceduralLevelGenerator.Examples.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.ProceduralLevelGenerator.Examples.Example1.Scripts
{
    public class Example1Door : InteractableBase
    {
        public override void BeginInteract()
        {
            ShowText("Press E to open doors");
        }

        public override void Interact()
        {
            if (Input.GetKey(KeyCode.E))
            {
                gameObject.SetActive(false);
            }
        }

        public override void EndInteract()
        {
            HideText();
        }
    }
}
