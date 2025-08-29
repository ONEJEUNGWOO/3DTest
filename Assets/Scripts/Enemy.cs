using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;

    private Rigidbody _rigidbody;
    private Vector3 changeValue;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damage, Vector3 knockBackDir, int knockBackForce )
    {
        hp -= damage;
        knockBackDir.y = 0;

        StartCoroutine(KnockBackCoroutine(knockBackDir, knockBackForce, 1f));
        //_rigidbody.velocity += knockBackDir * knockBackForce;
        //_rigidbody.AddForce(knockBackDir * knockBackForce, ForceMode.Impulse);
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
