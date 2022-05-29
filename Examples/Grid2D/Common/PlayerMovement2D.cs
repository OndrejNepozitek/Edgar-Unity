using UnityEngine;

namespace Edgar.Unity.Examples
{
    /// <summary>
    /// A simple player movement script.
    /// </summary>
    public class PlayerMovement2D : MonoBehaviour
    {
        public float MoveSpeed = 5f;

        private Animator animator;
        private Vector2 movement;
        private new Rigidbody2D rigidbody;
        private SpriteRenderer spriteRenderer;

        public void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Update()
        {
            movement.x = InputHelper.GetHorizontalAxis();
            movement.y = InputHelper.GetVerticalAxis();

            animator.SetBool("running", rigidbody.velocity.magnitude > float.Epsilon);

            // Flip sprite if needed
            var flipSprite = spriteRenderer.flipX ? movement.x > 0.01f : movement.x < -0.01f;
            if (flipSprite)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }

        public void FixedUpdate()
        {
            rigidbody.MovePosition(rigidbody.position + movement.normalized * MoveSpeed * Time.fixedDeltaTime);
        }
    }
}