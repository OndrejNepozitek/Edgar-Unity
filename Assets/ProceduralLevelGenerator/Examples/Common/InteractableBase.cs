using UnityEngine;
using UnityEngine.UI;

namespace Assets.ProceduralLevelGenerator.Examples.Common
{
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        protected Text InteractionText;

        public void Start()
        {
            InteractionText = GameObject.Find("Canvas").transform.Find("Interaction")?.GetComponent<Text>();
        }

        protected void ShowText(string text)
        {
            if (InteractionText != null)
            {
                InteractionText.gameObject.SetActive(true);
                InteractionText.text = text;
            }
        }

        protected void HideText()
        {
            if (InteractionText != null)
            {
                InteractionText.gameObject.SetActive(false);
            }
        }

        public virtual void BeginInteract()
        {

        }

        public virtual void Interact()
        {

        }

        public virtual void EndInteract()
        {

        }

        public virtual bool IsInteractionAllowed()
        {
            return true;
        }
    }
}