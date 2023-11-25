using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthGUI : MonoBehaviour
{
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private Image _healthBarImage;
    private void Start()
    {
        _healthSystem.OnHealthChange += UpdateHealthBar;

    }

    private void UpdateHealthBar(int currentHealth)
    {
        _healthBarImage.fillAmount = currentHealth / (float)_healthSystem.MaxHealth;
    }
}
