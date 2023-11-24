using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HurtfulObject : MonoBehaviour
{
    [SerializeField, Min(0)] private int _damage;
    public Action OnHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthSystem _target))
        {
            _target.Hit(_damage);
            OnHit?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent(out HealthSystem _target))
        {
            _target.Hit(_damage);
            OnHit?.Invoke();
            Destroy(gameObject);
        }
    }
}
