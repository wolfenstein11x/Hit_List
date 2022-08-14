using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float flipCorrection = 1f;

    Vector2 moveInput;
    Rigidbody2D rigidBody;
    Vector2 sizeScale;
    Animator animator;
    CapsuleCollider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sizeScale = new Vector2(transform.localScale.x, transform.localScale.y);
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (value.isPressed)
        { 
            animator.SetTrigger("jump");
            rigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
        animator.SetBool("running", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x ) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            // correct for the sprite flip point not being at its center
            float xPosCorrected = transform.position.x + (flipCorrection * Mathf.Sign(rigidBody.velocity.x));  
            transform.position = new Vector2(xPosCorrected, transform.position.y);

            // flip sprite
            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x) * sizeScale.x, 1f * sizeScale.y);
        }

    }
}
