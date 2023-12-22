using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpPlayer : MonoBehaviour
{

    private PlayerControls input = null;
    private Rigidbody2D rb;
    public bool isGrounded;
    public float groundCheckDistance;
    private float bufferCheckDistance = 0.1f;


    [Header("Ground Check")]
    [SerializeField] Transform groundCheckPos;
    [SerializeField] Vector2 groundCheckSize;
    public LayerMask groundLayer;


    [Header("Jumping")]
    public float jumpForce = 10f;
    private bool isJumping;
    private int maxJumps = 2;
    private int jumpsRemaining;

    [Header("Animation")]
    private Animator myAnim;

    // Adding gravity acceleration while falling
    [Header("Gravity")]
    [SerializeField] float baseGravity;
    [SerializeField] float maxFallSpeed;
    [SerializeField] float fallSpeedMultiplier;

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

        checkGrounded();

        Gravity();
    }

    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        jumpsRemaining--;
        if (isJumping && jumpsRemaining > 0)
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
        myAnim.Play("Jump");
        GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Gravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier; // fall increasing faster
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }

    private void checkGrounded()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
        }
    }
}
