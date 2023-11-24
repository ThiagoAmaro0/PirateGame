using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : HurtfulObject
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _lifeTime = 5;

    public void SetOwner(Collider2D owner)
    {
        Physics2D.IgnoreCollision(owner, _collider);
    }

    public void Fire(Vector2 direction)
    {
        transform.up = direction;
        _rb.velocity = transform.up * _speed;
        Destroy(gameObject, _lifeTime);
    }

}
