using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Damage {  get; private set; }
    public Vector3 KnockBackDir { get; private set; }
    public int KnockBackForce { get; private set; }


    private bool isAttackCoolTime = false;

    private float attackCoolTime = 1f;

    private void Awake()
    {
        Damage = 1;
        KnockBackForce = 3;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy") || isAttackCoolTime) return;
        Debug.Log(isAttackCoolTime.ToString());
        
        StartCoroutine(AttackCoolTimeCheck());
        KnockBackDir = (other.transform.position - transform.parent.position).normalized;
        EnemyManager.Instance.testEnemy.TakeDamage(Damage, KnockBackDir, KnockBackForce);
    }

    private IEnumerator AttackCoolTimeCheck()
    {
        isAttackCoolTime = true;
        yield return new WaitForSeconds(attackCoolTime);
        isAttackCoolTime = false;
    }
}
