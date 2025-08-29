using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput input;
    private Rigidbody _rigidbody;
    private AnimationHandlerA animationHandler;

    private Vector3 direction;
    private Vector2 currentInput;
    public int moveSpeed;


    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        animationHandler = GetComponent<AnimationHandlerA>();
        if (animationHandler == null)
        {
            Debug.LogError("AnimationHandler not found on " + gameObject.name);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        direction = transform.forward * currentInput.y + transform.right * currentInput.x;
        direction *= moveSpeed;
        direction.y = _rigidbody.velocity.y;

        _rigidbody.velocity = direction;
        animationHandler.MoveAnimationToggle(direction);

        Vector3 moveDirection = new Vector3(currentInput.x / 2, 0f, currentInput.y / 2);

        // 입력이 있는 경우에만 회전
        if (moveDirection.magnitude > 0.01f)
        {
            // 이동 방향으로 회전 목표값 계산
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // 현재 회전에서 목표 회전으로 천천히 회전
            float rotationSpeed = 2.5f; // 회전 속도 조절
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
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

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) return;

        animationHandler.IsAttack();
    }
}
