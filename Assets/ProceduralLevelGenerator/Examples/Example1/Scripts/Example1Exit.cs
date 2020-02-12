using Assets.ProceduralLevelGenerator.Examples.Common;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.Example1.Scripts
{
    public class Example1Exit : InteractableBase
    {
        public override void BeginInteract()
        {
            ShowText("Press E to exit the level");
        }

        public override void Interact()
        {
            if (Input.GetKey(KeyCode.E))
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