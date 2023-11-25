using System;
using UnityEngine;

[RequireComponent(typeof(PathFinderAgent))]
public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected Transform _player;
    [SerializeField] protected PathFinderAgent _agent;
    [SerializeField] protected HealthSystem _health;
    [SerializeField] protected Transform _visual;

    private const float THRESHOLD_ROTATE = 0.1f;

    protected virtual void Awake()
    {
        _health.OnDie += Die;
    }

    private void Start()
    {
        _agent.SetDestination(_player);
    }

    private void Update()
    {
        if (_agent.Velocity.magnitude > THRESHOLD_ROTATE)
            _visual.up = _agent.Velocity.normalized;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}