using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpPlayer : MonoBehaviour
{
    public bool isGrounded;
    public float groundCheckDistance;
    private float bufferCheckDistance = 0.1f;
    private PlayerControls input = null;
    public float jumpForce = 10f;
    private Animator myAnim;
    private bool isJumping;
    private Rigidbody2D rb;

    private void Awake()
    {
        input = new PlayerControls();
        myAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Land.Jump.started += OnJumpStarted;
        input.Land.Jump.canceled += OnJumpCanceled;
    }


    private void OnDisable()
    {
        input.Land.Jump.started -= OnJumpStarted;
        input.Land.Jump.canceled -= OnJumpCanceled;
    }


    private void FixedUpdate()
    {
        

        groundCheckDistance = (GetComponent<CapsuleCollider2D>().size.y / 2) + bufferCheckDistance;

        RaycastHit2D raycast = Physics2D.Raycast(transform.position, -Vector2.up, groundCheckDistance);

        if (raycast.collider)
        {
            isGrounded = true;
            isJumping = true;

        }
        else
        {
            isGrounded = false;
            isJumping = false;
        }

    }

    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        if (isJumping)
        {
            Jump();
        }
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        isJumping = false;
    }

    private void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpForce,ForceMode2D.Impulse);
    }
}
