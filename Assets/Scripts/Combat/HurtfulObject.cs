using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HurtfulObject : MonoBehaviour
{
    [SerializeField, Min(0)] protected int _damage;
    [SerializeField] private GameObject _explosionPrefab;
    public Action OnHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthSystem _target))
        {
            _target.Hit(_damage);
            OnHit?.Invoke();
            Explosion();
            Destroy(gameObject);
        }
        else if (other.TryGetComponent(out HurtfulObject hurtful))
        {
            if (TryGetComponent(out HealthSystem _this))
                return;
            OnHit?.Invoke();
            Explosion();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent(out HealthSystem _target))
        {
            _target.Hit(_damage);
            OnHit?.Invoke();
            Explosion();
            Destroy(gameObject);
        }
        else if (other.transform.TryGetComponent(out HurtfulObject hurtful))
        {
            if (TryGetComponent(out HealthSystem _this))
                return;
            OnHit?.Invoke();
            Explosion();
            Destroy(gameObject);

        }
    }

    private void Explosion()
    {
        Destroy(Instantiate(_explosionPrefab, transform.position, Quaternion.identity), 0.5f);
    }
}
