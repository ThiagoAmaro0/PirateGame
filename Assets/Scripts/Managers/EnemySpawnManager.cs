using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private PathfinderGrid _grid;
    [SerializeField] private ConfigurationSO _levelConfig;
    [SerializeField] private BaseEnemy[] _enemiesPrefabs;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private PlayerManager _player;

    private List<BaseEnemy> _enemies;

    private float _spawnTime;
    private bool _running = true;

    private void Awake()
    {
        _enemies = new List<BaseEnemy>();
    }

    private void Update()
    {
        if (!_running) return;

        if (_spawnTime <= Time.time)
        {
            Spawn();
        }
    }

    public void GameOver()
    {
        _running = false;
        foreach (BaseEnemy enemy in _enemies)
        {
            if (enemy)
            {
                enemy.Stop();
            }
        }
    }

    private void Spawn()
    {
        _spawnTime = _levelConfig.SpawnDelay + Time.time;
        Transform spawn = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        BaseEnemy enemy = Instantiate(_enemiesPrefabs[Random.Range(0, _enemiesPrefabs.Length)],
                                     spawn.position, Quaternion.identity, transform);
        enemy.Agent.Grid = _grid;
        enemy.Player = _player.transform;
        _enemies.Add(enemy);
    }
}
