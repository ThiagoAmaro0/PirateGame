using System;
using UnityEngine;

[RequireComponent(typeof(PathFinderAgent))]
public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] protected PathFinderAgent _agent;
    [SerializeField] protected HealthSystem _health;
    [SerializeField] protected Transform _visual;
    public PathFinderAgent Agent { get => _agent; }

    private const float THRESHOLD_ROTATE = 0.1f;

    public Transform Player
    {
        get => player;
        set
        {
            player = value;
            _agent.SetDestination(Player);
        }
    }

    protected virtual void Awake()
    {
        _health.OnDie += Die;
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

    public void Stop()
    {
        _agent.Stop();
    }
}