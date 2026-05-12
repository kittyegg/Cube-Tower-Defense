using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private SandPathBuildService _sandPathBuildService;
    [SerializeField] private TurretsShop _turretShop;
    [SerializeField] private SpawnDeathEnemyController _spawnDeathControl;

    private void OnEnable()
    {
        _gameManager.OnStateChanged += OnGameStateChanged;
        _sandPathBuildService.OnBuildComplete += OnBuildComplete;
        _turretShop.OnTurretSelected += OnTurretSelected;
        _turretShop.OnTurretDeselected += OnTurretDeselected;
        _spawnDeathControl.OnLastEnemyDestroyed += OnLastEnemyDestroyed;
    }

    private void OnDisable()
    {
        _gameManager.OnStateChanged -= OnGameStateChanged;
        _sandPathBuildService.OnBuildComplete -= OnBuildComplete;
        _turretShop.OnTurretSelected -= OnTurretSelected;
        _turretShop.OnTurretDeselected -= OnTurretDeselected;
        _spawnDeathControl.OnLastEnemyDestroyed -= OnLastEnemyDestroyed;
    }

    private static void OnGameStateChanged(GameManager.GameState newState)
    {
        print("State changed, current state: " + newState);
    }

    private static void OnBuildComplete()
    {
        print("Build complete");
    }

    private static void OnTurretSelected(TurretShopItem item)
    {
        print("Turret selected: " + item.TurretPrefab.name);
    }

    private static void OnTurretDeselected()
    {
        print("Turret deselected");
    }

    private void OnLastEnemyDestroyed()
    {
        print("Last enemy destroyed");
    }
}