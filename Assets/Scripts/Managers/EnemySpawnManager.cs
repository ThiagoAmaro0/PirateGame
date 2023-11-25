using System;
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
    private int _score;

    public Action<int> OnScore;

    private void OnEnable()
    {

        GameManager.instance.OnChangeState += StateListener;
    }
    private void OnDisable()
    {

        GameManager.instance.OnChangeState -= StateListener;
    }

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
    private void StateListener(GameState state)
    {
        if (state == GameState.GAME)
        {
            _enemies = new List<BaseEnemy>();
            _running = true;
            _score = 0;
            OnScore?.Invoke(_score);
        }
        else if (state == GameState.WIN || state == GameState.LOSE)
        {
            foreach (BaseEnemy enemy in _enemies)
            {
                if (enemy)
                {
                    Destroy(enemy.gameObject);
                }
            }
            _running = false;
        }
    }

    private void Spawn()
    {
        _spawnTime = _levelConfig.SpawnDelay + Time.time;
        Transform spawn = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];
        BaseEnemy enemy = Instantiate(_enemiesPrefabs[UnityEngine.Random.Range(0, _enemiesPrefabs.Length)],
                                     spawn.position, Quaternion.identity, transform);
        enemy.Agent.Grid = _grid;
        enemy.Player = _player.transform;
        enemy.Health.OnDie += IncreaseScore;

        _enemies.Add(enemy);
    }

    private void IncreaseScore()
    {
        _score++;
        OnScore?.Invoke(_score);
    }
}
