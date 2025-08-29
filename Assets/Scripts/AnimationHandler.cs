using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int isMoving = Animator.StringToHash("IsMove");
    private static readonly int isRun = Animator.StringToHash("IsRun");
    private static readonly int isAttack_1 = Animator.StringToHash("IsAttack_1");
    private static readonly int isAttack_2 = Animator.StringToHash("IsAttack_2");
    private static readonly int isAttack_3 = Animator.StringToHash("IsAttack_3");
    private static readonly int isAttack_4 = Animator.StringToHash("IsAttack_4");

    private int combo = 4;
    private float attackCoolTime = 1.2f;
    private bool isAttacking = true;

    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void MoveAnimationToggle(Vector3 direction)
    {
        animator.SetBool(isMoving, direction.magnitude > 0.5f);
    }

    public void RunAnimationToggle(bool isrun)
    {
        animator.SetBool(isRun, isrun);
    }

    public void IsAttack()
    {
        if (isAttacking == false) return;

        switch (combo)
        {
            case 4:
                animator.SetTrigger(isAttack_1);
                StartCoroutine(CoolTimeCheck());
                combo = 3;
                break;
            case 3:
                animator.SetTrigger(isAttack_2);
                StartCoroutine(CoolTimeCheck());
                combo = 2;
                break;
            case 2:
                animator.SetTrigger(isAttack_3);
                StartCoroutine(CoolTimeCheck());
                combo = 1;
                break;
            case 1:
                animator.SetTrigger(isAttack_4);
                StartCoroutine(CoolTimeCheck());
                combo = 4;
                break;
        }
    }

    public IEnumerator CoolTimeCheck()
    {
        isAttacking = false;
        yield return new WaitForSeconds(attackCoolTime);
        isAttacking = true;
    }
}
