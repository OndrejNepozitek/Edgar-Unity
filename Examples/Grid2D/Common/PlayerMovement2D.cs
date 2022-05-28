#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
#define EDGAR_NEW_INPUT
using UnityEngine.InputSystem;
#endif
using System;
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

        #if EDGAR_NEW_INPUT
        private readonly InputAction horizontalAxis = new InputAction();
        private readonly InputAction verticalAxis = new InputAction();
        #endif

        private Func<float> horizontalAxisGetter;
        private Func<float> verticalAxisGetter;

        private void OnEnable()
        {
            #if EDGAR_NEW_INPUT
            horizontalAxis.AddCompositeBinding("Axis")
                .With("Positive", "<Keyboard>/#(D)")
                .With("Negative", "<Keyboard>/#(A)");
            verticalAxis.AddCompositeBinding("Axis")
                .With("Positive", "<Keyboard>/#(W)")
                .With("Negative", "<Keyboard>/#(S)");

            horizontalAxis.Enable();
            verticalAxis.Enable();

            horizontalAxisGetter = () => horizontalAxis.ReadValue<float>();
            verticalAxisGetter = () => verticalAxis.ReadValue<float>();
            #else
            horizontalAxisGetter = () => Input.GetAxisRaw("Horizontal");
            verticalAxisGetter = () => Input.GetAxisRaw("Vertical");
            #endif
        }

        private void OnDisable()
        {
            #if EDGAR_NEW_INPUT
            horizontalAxis.Disable();
            verticalAxis.Disable();
            #endif
        }

        public void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Update()
        {
            movement.x = horizontalAxisGetter();
            movement.y = verticalAxisGetter();

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