using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private SandPathBuildService _sandPathBuildService;
    [SerializeField] private TurretsShop _turretShop;

    private void OnEnable()
    {
        _gameManager.OnStateChanged += OnGameStateChanged;
        _sandPathBuildService.OnBuildComplete += OnBuildComplete;
        _turretShop.OnTurretSelected += OnTurretSelected;
        _turretShop.OnTurretDeselected += OnTurretDeselected;
    }

    private void OnDisable()
    {
        _gameManager.OnStateChanged -= OnGameStateChanged;
        _sandPathBuildService.OnBuildComplete -= OnBuildComplete;
        _turretShop.OnTurretSelected -= OnTurretSelected;
        _turretShop.OnTurretDeselected -= OnTurretDeselected;
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
}