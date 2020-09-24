using UnityEngine;
using UnityEngine.UI;

namespace Edgar.Unity.Examples
{
    /// <summary>
    /// Very simple implementation of an interactable object.
    /// Inheriting classes should override provided methods.
    /// </summary>
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        protected Text InteractionText;

        public void Start()
        {
            InteractionText = GameObject.Find("Canvas")?.transform.Find("Interaction")?.GetComponent<Text>();
        }

        /// <summary>
        /// Shows a text on the screen if the corresponding UI object exits.
        /// </summary>
        /// <param name="text"></param>
        protected void ShowText(string text)
        {
            if (InteractionText != null)
            {
                InteractionText.gameObject.SetActive(true);
                InteractionText.text = text;
            }
        }

        /// <summary>
        /// Hides the interaction text if the corresponding UI object exits.
        /// </summary>
        protected void HideText()
        {
            if (InteractionText != null)
            {
                InteractionText.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Override if needed. Base implementation does nothing.
        /// </summary>
        /// <inheritdoc/> 
        public virtual void BeginInteract()
        {
            /* empty */
        }

        /// <summary>
        /// Override if needed. Base implementation does nothing.
        /// </summary>
        /// <inheritdoc/> 
        public virtual void Interact()
        {
            /* empty */
        }

        /// <summary>
        /// Override if needed. Base implementation does nothing.
        /// </summary>
        /// <inheritdoc/> 
        public virtual void EndInteract()
        {
            /* empty */
        }

        /// <summary>
        /// Override if needed. Base implementation returns always true if game object is active.
        /// </summary>
        /// <inheritdoc/> 
        public virtual bool IsInteractionAllowed()
        {
            return gameObject.activeSelf;
        }

        public void OnDisable()
        {
            EndInteract();
        }
    }
}