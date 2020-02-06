using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.Common
{
    public class PlayerMovement : MonoBehaviour
    {
        private Animator animator;

        private Vector2 movement;
        public float MoveSpeed = 5f;

        public Rigidbody2D Rigidbody;

        private SpriteRenderer spriteRenderer;

        public void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetBool("running", Rigidbody.velocity.magnitude > float.Epsilon);

            var flipSprite = spriteRenderer.flipX ? movement.x > 0.01f : movement.x < -0.01f;
            if (flipSprite)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }

        public void FixedUpdate()
        {
            Rigidbody.MovePosition(Rigidbody.position + movement.normalized * MoveSpeed * Time.fixedDeltaTime);
        }
    }
}