using UnityEngine;
using System.Collections;

public class EndWaveStateSwitch : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private SpawnDeathEnemyController _spawnDeathEnemyController;

    private void OnEnable()
    {
        _spawnDeathEnemyController.OnLastEnemyDestroyed += OnLastEnemyDestroyed;
    }

    private void OnDisable()
    {
        _spawnDeathEnemyController.OnLastEnemyDestroyed -= OnLastEnemyDestroyed;
    }

    private void OnLastEnemyDestroyed()
    {
        _gameManager.CurrentState = GameManager.GameState.BuildingTurrets;
    }
}
