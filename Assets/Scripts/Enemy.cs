using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    public int hp;

    private Vector3 distance = new Vector3(0, 0, -1);

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(target.position - distance);

        //반대방향 보게 하는 법
        //Vector3 dir = (target.position - transform.position).normalized;
        //Quaternion targetRot = Quaternion.LookRotation(-dir);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
        
        transform.LookAt(target);

        if (hp != 0) return;

        Dead();
    }

    public void TakeDamage(int damage, Vector3 knockBackDir, int knockBackForce)
    {
        hp -= damage;
        knockBackDir.y = 0;

        StartCoroutine(KnockBackCoroutine(knockBackDir, knockBackForce, 1f));
        //_rigidbody.velocity += knockBackDir * knockBackForce;
        //_rigidbody.AddForce(knockBackDir * knockBackForce, ForceMode.Impulse);
    }

    public void Dead()
    {
        if (hp == 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator KnockBackCoroutine(Vector3 dir, float force, float duration)
    {
        float elapsed = 0;
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + dir * force;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            elapsed = Mathf.Clamp01(elapsed);

            float easedOut = 1 - Mathf.Pow(1 - elapsed, 3);

            transform.position = Vector3.Lerp(startPos, targetPos, easedOut / duration);

            yield return null;
        }

        transform.position = targetPos;
    }
}
