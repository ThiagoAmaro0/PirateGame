using UnityEngine;

[CreateAssetMenu(fileName = "ConfigurationSO", menuName = "Painful Smile Test/ConfigurationSO", order = 0)]
public class ConfigurationSO : ScriptableObject
{
    [SerializeField, Range(0f, 3f)] private float _gameDuration;
    [SerializeField] private float _spawnDelay;

    public float GameDuration { get => _gameDuration; set => _gameDuration = value; }
    public float SpawnDelay { get => _spawnDelay; set => _spawnDelay = value; }
}