using Assets.ProceduralLevelGenerator.Examples.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.ProceduralLevelGenerator.Examples.Example1.Scripts
{
    public class Example1Door : MonoBehaviour, IInteractable
    {
        private Text interactionText;

        public void Start()
        {
            interactionText = GameObject.Find("Canvas").transform.Find("Interaction")?.GetComponent<Text>();
        }

        public void BeginInteract()
        {
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(true);
                interactionText.text = "Press E to open doors";
            }
        }

        public void Interact()
        {
            if (Input.GetKey(KeyCode.E))
            {
                gameObject.SetActive(false);
            }
        }

        public void EndInteract()
        {
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }
}
