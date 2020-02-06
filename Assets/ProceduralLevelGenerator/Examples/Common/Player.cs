using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.Common
{
    public class Player : MonoBehaviour
    {
        private IInteractable interactableInFocus;

        public void OnTriggerEnter2D(Collider2D collider)
        {
            var interactable = collider.GetComponent<IInteractable>();

            if (interactable == null)
            {
                return;
            }

            interactableInFocus?.EndInteract();
            interactableInFocus = interactable;
            interactableInFocus.BeginInteract();
        }

        public void OnTriggerExit2D(Collider2D collider)
        {
            var interactable = collider.GetComponent<IInteractable>();

            if (interactable == interactableInFocus)
            {
                interactableInFocus?.EndInteract();
                interactableInFocus = null;
            }
        }

        public void OnTriggerStay2D(Collider2D collider)
        {
            interactableInFocus?.Interact();
        }
    }
}