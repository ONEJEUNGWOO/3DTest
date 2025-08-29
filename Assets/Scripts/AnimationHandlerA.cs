using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandlerA : MonoBehaviour
{
    private static readonly int isMoving = Animator.StringToHash("IsMove");
    private static readonly int isAttack = Animator.StringToHash("IsAttack");


    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    public void MoveAnimationToggle(Vector3 direction)
    {
        animator.SetBool(isMoving, direction.magnitude > 0.5f);
    }

    public void IsAttack()
    {
        animator.SetTrigger(isAttack);
    }
}
