using UnityEditor;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.Common
{
    public abstract class Interactable : MonoBehaviour
    {
        public float Radius = 1;

        private Transform player;
        private bool isInteracting;

        public virtual void Start()
        {
            player = GameObject.FindWithTag("Player")?.transform;
            isInteracting = false;
        }

        public void Update()
        {
            if (player != null)
            {
                if (Vector3.Distance(transform.position, player.position) < Radius)
                {
                    if (!isInteracting)
                    {
                        isInteracting = true;
                        BeginInteract();
                    }

                    Interact();
                }
                else
                {
                    if (isInteracting)
                    {
                        isInteracting = false;
                        EndInteract();
                    }
                }
            }
        }

        protected virtual void BeginInteract()
        {

        }

        protected virtual void Interact()
        {

        }

        protected virtual void EndInteract()
        {

        }

        public void OnDrawGizmosSelected()
        {
            Handles.DrawWireDisc(transform.position ,Vector3.back, Radius);
        }
    }
}