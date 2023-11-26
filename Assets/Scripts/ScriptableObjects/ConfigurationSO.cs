using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigurationSO", menuName = "Painful Smile Test/ConfigurationSO", order = 0)]
public class ConfigurationSO : ScriptableObject
{
    [SerializeField] private int _minDuration = 60;
    [SerializeField] private int _maxDuration = 180;
    [SerializeField] private int _minDelay = 1;
    [SerializeField] private int _maxDelay = 30;
    private int _gameDuration = 60;
    private int _spawnDelay = 3;

    public int GameDuration { get => _gameDuration; set => SetDuration(value); }
    public int SpawnDelay { get => _spawnDelay; set => SetDelay(value); }

    private void SetDuration(int value)
    {
        _gameDuration = value;
        _gameDuration = Mathf.Clamp(_gameDuration, _minDuration, _maxDuration);
    }

    private void SetDelay(int value)
    {
        _spawnDelay = value;
        _spawnDelay = Mathf.Clamp(_spawnDelay, _minDelay, _maxDelay);
    }
}