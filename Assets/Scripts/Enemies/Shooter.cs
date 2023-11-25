using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : BaseEnemy
{
    [SerializeField] private float _fireRate;
    [SerializeField] private Cannon _cannon;
    [SerializeField] private Collider2D _collider;
    private float _fireTime;
    private void FixedUpdate()
    {
        if (_agent.IsStoppedByDistance)
        {
            Vector2 dir = (Player.position - transform.position).normalized;
            _visual.rotation = Quaternion.LookRotation(Vector3.forward, dir);
            if (Time.time >= _fireTime)
            {
                _fireTime = _fireRate + Time.time;
                _cannon.Fire(_collider);
            }
        }
    }

}
