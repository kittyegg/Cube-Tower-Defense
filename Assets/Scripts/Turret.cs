using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TurretBullet _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;
    [Header("Stats")]
    [SerializeField] private float _reloadTime = 1f;
    [SerializeField] private float _damage;
    [SerializeField] private float _distance = 3f;
    [Header("Economy")]
    [SerializeField] private int _price;
    [SerializeField] private float _sellingMultiplier = 0.5f;
    [SerializeField] private float _upgradeCostMultiplier = 0.2f;
    [SerializeField] private float _upgradeCostMultiplierIncrement = 0.1f;
    [Header("Upgrade")]
    [SerializeField] TurretUpgrade[] _upgrades;

    private float _reloadTimer;
    private int _lvl;

    public int Price => _price;
    public float Damage => _damage;
    public float Distance => _distance;
    public int SellPrice => Mathf.RoundToInt(_price * _sellingMultiplier);
    public int UpgradeCost => Mathf.RoundToInt(_price * _upgradeCostMultiplier);
    public bool IsMaxLevel => _lvl >= _upgrades.Length - 1;

    private void Start()
    {
        if (_bulletSpawnPoint == null)
            _bulletSpawnPoint = transform;
    }

    public void Upgrade()
    {
        if (IsMaxLevel)
            return;

        var upgrade = _upgrades[_lvl++];
        // TODO: исправить умножение на 0
        _damage *= upgrade.UpgradeDamageMultiplier;
        _distance *= upgrade.UpgradeDistanceMultiplier;
        _reloadTime *= upgrade.UpgradeReloadTimeMultiplier;
        _upgradeCostMultiplier += _upgradeCostMultiplierIncrement;
    }

    private void Update()
    {
        SpawnBullet(Vector3.forward);
        SpawnBullet(Vector3.back);
        SpawnBullet(Vector3.left);
        SpawnBullet(Vector3.right);

        _reloadTimer += Time.deltaTime;
    }

    private void SpawnBullet(Vector3 direction)
    {
        if (_reloadTimer < _reloadTime || !DetectEnemy(direction))
            return;

        _bulletPrefab.Spawn(this, _bulletSpawnPoint.position, direction);
        _reloadTimer = 0f;
    }

    private bool DetectEnemy(Vector3 direction) =>
        Physics.Raycast(transform.position, direction, out var hit, _bulletPrefab.Distance + _distance) &&
        hit.transform.TryGetComponent(out Enemy _);
}
