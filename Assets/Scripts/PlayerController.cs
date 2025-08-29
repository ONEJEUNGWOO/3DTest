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

        // �Է��� �ִ� ��쿡�� ȸ��
        if (moveDirection.magnitude > 0.01f)
        {
            // �̵� �������� ȸ�� ��ǥ�� ���
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // ���� ȸ������ ��ǥ ȸ������ õõ�� ȸ��
            float rotationSpeed = 2.5f; // ȸ�� �ӵ� ����
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
