using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _player;
    [SerializeField] private ConfigurationSO _levelConfig;
    private float _endTime;
    private bool _gameOver;

    private void OnEnable()
    {
        GameManager.instance.OnChangeState += StateListener;
        _player.HealthSystem.OnDie += GameOver;
    }
    private void OnDisable()
    {
        GameManager.instance.OnChangeState -= StateListener;
        _player.HealthSystem.OnDie -= GameOver;
    }

    private void StateListener(GameState state)
    {
        if (state == GameState.GAME)
        {
            _endTime = _levelConfig.GameDuration + Time.time;
            _gameOver = false;
        }
        else if (state == GameState.WIN || state == GameState.LOSE)
        {
            _gameOver = true;
        }
    }

    private void Awake()
    {
        _endTime = _levelConfig.GameDuration + Time.time;
    }

    private void Update()
    {
        if (_endTime < Time.time && !_gameOver)
        {
            _gameOver = true;
            Win();
        }
    }

    public int GetTimeLeft()
    {
        return (int)(_endTime - Time.time);
    }

    private void Win()
    {
        GameManager.instance.SetState(GameState.WIN);
    }

    private void GameOver()
    {
        GameManager.instance.SetState(GameState.LOSE);
    }
}
