using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerCombat _playerCombat;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private HealthSystem _healthSystem;

    private Vector2 _startPosition;

    public HealthSystem HealthSystem => _healthSystem;

    private void OnEnable()
    {
        GameManager.instance.OnChangeState += StateListener;
    }
    private void OnDisable()
    {
        GameManager.instance.OnChangeState -= StateListener;
    }

    private void Awake()
    {
        _startPosition = transform.position;
        _healthSystem.OnDie += Die;
    }

    private void StateListener(GameState state)
    {
        if (state == GameState.GAME)
        {
            _playerCombat.enabled = true;
            _playerMovement.enabled = true;
            _healthSystem.Initialize();

            transform.position = _startPosition;
            transform.rotation = Quaternion.identity;
        }
        else
        {
            _playerCombat.enabled = false;
            _playerMovement.enabled = false;
        }
    }

    private void Die()
    {
        _playerMovement.enabled = false;
        _playerCombat.enabled = false;
    }
}