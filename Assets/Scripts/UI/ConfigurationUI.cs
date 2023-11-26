using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationUI : MonoBehaviour
{
    [SerializeField] private ConfigurationSO _levelConfig;
    [SerializeField] private int _durationChangeStep;
    [SerializeField] private int _delayChangeStep;
    [SerializeField] private TMP_Text _gameDurationText;
    [SerializeField] private TMP_Text _spawnDelayText;
    [SerializeField] private Button _increaseSpawnTimeButton;
    [SerializeField] private Button _decreaseSpawnTimeButton;
    [SerializeField] private Button _increaseGameDurationButton;
    [SerializeField] private Button _decreaseGameDurationButton;

    private void Awake()
    {
        _increaseGameDurationButton.onClick.AddListener(() => AddGameDuration(_durationChangeStep));
        _decreaseGameDurationButton.onClick.AddListener(() => AddGameDuration(-_durationChangeStep));

        _increaseSpawnTimeButton.onClick.AddListener(() => AddSpawnDelay(_delayChangeStep));
        _decreaseSpawnTimeButton.onClick.AddListener(() => AddSpawnDelay(-_delayChangeStep));
    }

    private void OnEnable()
    {
        UpdateValues();
    }

    private void UpdateValues()
    {
        _gameDurationText.text = $"{_levelConfig.GameDuration} s";
        _spawnDelayText.text = $"{_levelConfig.SpawnDelay} s"; ;
    }
    private void AddGameDuration(int step)
    {
        _levelConfig.GameDuration += step;
        UpdateValues();
    }

    private void AddSpawnDelay(int step)
    {
        _levelConfig.SpawnDelay += step;
        UpdateValues();
    }
}
