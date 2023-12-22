using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls input = null;
    private Vector2 moveInput;
    private float moveSpeed = 5f;
    private bool grounded;
    private Animator charAnim;

    private void Awake()
    {
        input = new PlayerControls();
        charAnim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // Subscribe to the move action events
        input.Enable();
        input.Land.Move.performed += OnMovePerformed;
        input.Land.Move.canceled += OnMoveCancelled;
    }

    private void OnDisable()
    {
        // Unsubscribe from the move action events
        input.Disable();
        input.Land.Move.performed -= OnMovePerformed;
        input.Land.Move.canceled -= OnMoveCancelled;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        // Read the normalized move input from the input system
        moveInput = context.ReadValue<Vector2>().normalized;
        charAnim.SetBool("Walk", true);
    }

    private void OnMoveCancelled(InputAction.CallbackContext context)
    {
        // Reset move input when the move action is cancelled
        moveInput = Vector2.zero;
        charAnim.SetBool("Walk", false);
    }

    void Move()
    {
        Vector2 moveDirection = new Vector2(moveInput.x, 0f).normalized;
        Vector2 moveVelocity = moveDirection * moveSpeed;

        transform.Translate(moveVelocity * Time.deltaTime, Space.World);


        if (moveDirection.x > 0)
            changeDirection(5);

        if (moveDirection.x<0)
            changeDirection(-5);
    }


    public void changeDirection(int direction)

    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

}

