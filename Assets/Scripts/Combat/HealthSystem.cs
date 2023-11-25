using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    public Action<int> OnHealthChange;
    public Action OnDie;

    public int MaxHealth { get => _maxHealth; private set => _maxHealth = value; }

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        _currentHealth = MaxHealth;
        OnHealthChange?.Invoke(_currentHealth);
    }

    public void Hit(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnDie?.Invoke();
        }
        OnHealthChange?.Invoke(_currentHealth);
    }
}