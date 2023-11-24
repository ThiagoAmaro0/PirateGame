using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HurtfulObject : MonoBehaviour
{
    [SerializeField, Min(0)] protected int _damage;
    public Action OnHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthSystem _target))
        {
            _target.Hit(_damage);
            OnHit?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent(out HealthSystem _target))
        {
            _target.Hit(_damage);
            OnHit?.Invoke();
            Destroy(gameObject);
        }
    }
}
