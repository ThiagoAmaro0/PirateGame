using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private Sprite[] _hullSprites;
    [SerializeField] private Sprite[] _sailSprites;
    [SerializeField] private SpriteRenderer _hullRenderer;
    [SerializeField] private SpriteRenderer _sailRenderer;
    [SerializeField] private GameObject _explosionPrefab;
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
        _hullRenderer.sprite = _hullSprites[_hullSprites.Length - 1];
        _sailRenderer.sprite = _sailSprites[_hullSprites.Length - 1];
        OnHealthChange?.Invoke(_currentHealth);
    }

    public void Hit(int damage)
    {
        if (_currentHealth == 0)
            return;

        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _hullRenderer.sprite = _hullSprites[0];
            _sailRenderer.sprite = _sailSprites[0];
            OnDie?.Invoke();
            Destroy(Instantiate(_explosionPrefab, transform.position, Quaternion.identity), 0.5f);
        }
        else
        {
            int spriteIndex = Mathf.RoundToInt((_currentHealth / (float)_maxHealth) * _hullSprites.Length);

            if (spriteIndex == 0)
                spriteIndex++;

            _hullRenderer.sprite = _hullSprites[spriteIndex];
            _sailRenderer.sprite = _sailSprites[spriteIndex];

        }
        OnHealthChange?.Invoke(_currentHealth);
    }
}