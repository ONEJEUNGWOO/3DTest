using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AnimationHandler animationHandler;

    private Vector3 direction;
    private Vector2 currentInput;
    private Quaternion currentRotation;
    
    private bool isRun =  false;
    private bool isAttack = false;
    private int runSpeed;

    public Collider _collider;
    public int moveSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        animationHandler = GetComponent<AnimationHandler>();
        
        _collider.enabled = false;
        
        if (animationHandler == null)
        {
            Debug.LogError("AnimationHandler not found on " + gameObject.name);
        }
    }

    private void FixedUpdate()
    {
        Move();
        Run();
    }

    private void Move()
    {
        if (isRun) return;

        direction = transform.forward * currentInput.y + transform.right * currentInput.x;
        direction *= moveSpeed;
        direction.y = _rigidbody.velocity.y;

        _rigidbody.velocity = direction;
        animationHandler.MoveAnimationToggle(direction);
        animationHandler.RunAnimationToggle(isRun);

        Vector3 moveDirection = new Vector3(currentInput.x, 0f, 0f);
        // 입력이 있는 경우에만 회전
        if (moveDirection.magnitude > 0.01f)  //키보드로 회전 할 경우
        {
            // 이동 방향으로 회전 목표값 계산
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            currentRotation = transform.rotation;

            // 현재 회전에서 목표 회전으로 천천히 회전
            float rotationSpeed = 2.5f; // 회전 속도 조절
            transform.rotation = Quaternion.Slerp(transform.rotation, currentRotation * targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void Run()
    {
        if (!isRun) return;
        direction = transform.forward * currentInput.y + transform.right * currentInput.x;
        direction *= runSpeed;
        direction.y = _rigidbody.velocity.y;

        _rigidbody.velocity = direction;
        animationHandler.RunAnimationToggle(isRun);

        Vector3 moveDirection = new Vector3(currentInput.x, 0f, 0f);
        // 입력이 있는 경우에만 회전
        if (moveDirection.magnitude > 0.01f)  //키보드로 회전 할 경우
        {
            // 이동 방향으로 회전 목표값 계산
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            currentRotation = transform.rotation;

            // 현재 회전에서 목표 회전으로 천천히 회전
            float rotationSpeed = 2.5f; // 회전 속도 조절
            transform.rotation = Quaternion.Slerp(transform.rotation, currentRotation * targetRotation, rotationSpeed * Time.deltaTime);
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
            animationHandler.RunAnimationToggle(isRun);
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isRun = true;
            runSpeed = moveSpeed * 2;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isRun = false;
            runSpeed = moveSpeed;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) return;
        {
            animationHandler.IsAttack();
        }
    }

    public void ToggleWeaponCollider()
    {
        isAttack = !isAttack;
        _collider.enabled = isAttack;
    }
}
