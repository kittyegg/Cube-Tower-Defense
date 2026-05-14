using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnDeathEnemyController : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private UnityEvent _onLastEnemyDestroyed;

    public event UnityAction OnLastEnemyDestroyed
    {
        add => _onLastEnemyDestroyed.AddListener(value);
        remove => _onLastEnemyDestroyed.RemoveListener(value);
    }

    private readonly List<Enemy> _enemies = new();

    private void OnEnable()
    {
        _enemySpawner.OnEnemySpawned += OnEnemySpawned;
    }

    private void OnDisable()
    {
        _enemySpawner.OnEnemySpawned -= OnEnemySpawned;
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        enemy.OnDeath += e => _wallet.AddMoney(e.KillReward);
        enemy.OnDestroyed += () =>
        {
            _enemies.Remove(enemy);
            if (_enemies.Count == 0)
                _onLastEnemyDestroyed?.Invoke();
        };
        _enemies.Add(enemy);
    }
}