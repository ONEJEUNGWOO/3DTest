using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput input;
    private Rigidbody rigidbody;

    private Vector3 direction;
    private Vector2 currentInput;
    public int moveSpeed;


    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        direction = transform.forward * currentInput.y + transform.right * currentInput.x;
        direction *= moveSpeed;
        direction.y = rigidbody.velocity.y;

        rigidbody.velocity = direction;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            currentInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            currentInput = Vector2.zero;
        }
    }
}
