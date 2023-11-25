using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _player;
    [SerializeField] private ConfigurationSO _levelConfig;
    [SerializeField] private EnemySpawnManager _spawnManager;
    private float _endTime;
    private bool _gameOver;

    private void Awake()
    {
        _endTime = 60 * _levelConfig.GameDuration + Time.time;
        _player.HealthSystem.OnDie += GameOver;
    }

    private void Update()
    {
        if (_endTime < Time.time && !_gameOver)
        {
            _gameOver = true;
            Win();
        }
    }

    private void Win()
    {
        _spawnManager.GameOver();
    }

    private void GameOver()
    {
        _spawnManager.GameOver();
    }
}
