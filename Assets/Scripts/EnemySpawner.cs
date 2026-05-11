using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private SandPathBuildService _pathBuildService;
    [SerializeField] private Transform _enemyHolder;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Wallet _wallet;

    //private readonly List<Enemy> _enemies = new();

    private void Awake()
    {
        if (_gameManager == null)
            _gameManager = FindAnyObjectByType<GameManager>();
        if (_enemyHolder == null)
            _enemyHolder = transform;
        if (_spawnPoint == null)
            _spawnPoint = transform;
    }

    private void OnEnable()
    {
        _gameManager.OnLevelChanged += OnLevelChanged;
    }
    private void OnDisable()
    {
        _gameManager.OnLevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(Level level) => StartCoroutine(SpawnCoroutine(level));

    private IEnumerator SpawnCoroutine(Level level)
    {
        var wait = new WaitForSeconds(level.LevelTimeSec / level.EnemiesPrefabs.Count);

        foreach (var prefab in level.EnemiesPrefabs)
        {
            var enemy = Instantiate(prefab, _spawnPoint.position, _spawnPoint.rotation, _enemyHolder);
            enemy.PathBuildService = _pathBuildService;
            enemy.OnDeath += e => _wallet.AddMoney(e.KillReward);
            //_enemies.Add(enemy);

            yield return wait;
        }
    }
}
