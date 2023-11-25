using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private EnemySpawnManager _spawner;
    [SerializeField] private SessionManager _sessionManager;
    [SerializeField] private TMP_Text _gameScoreText;
    [SerializeField] private TMP_Text _winScoreText;
    [SerializeField] private TMP_Text _loseScoreText;
    [SerializeField] private TMP_Text _timerText;

    private void OnEnable()
    {
        _spawner.OnScore += UpdateScore;
    }

    private void OnDisable()
    {
        _spawner.OnScore -= UpdateScore;
    }

    private void Update()
    {
        if (GameManager.instance.State == GameState.GAME)
        {
            _timerText.text = $"Time left: {_sessionManager.GetTimeLeft()}s";
        }
    }

    private void UpdateScore(int value)
    {
        _gameScoreText.text = $"Score: {value}";
        _loseScoreText.text = $"Score: {value}";
        _winScoreText.text = $"Score: {value}";
    }
}
