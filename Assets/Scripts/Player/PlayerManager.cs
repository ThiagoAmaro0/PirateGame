using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerCombat _playerCombat;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private HealthSystem _healthSystem;

    public HealthSystem HealthSystem => _healthSystem;

    private void Awake()
    {
        _healthSystem.OnDie += Die;
    }

    private void Die()
    {
        _playerMovement.enabled = false;
        _playerCombat.enabled = false;
        print("DIE");
    }
}