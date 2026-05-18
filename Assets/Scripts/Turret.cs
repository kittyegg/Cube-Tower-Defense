using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private TurretBullet _bulletPrefab;
    [SerializeField] private float _reloadTime = 1f;
    [SerializeField] private float _damage;
    [SerializeField] private float _distance = 3f;
    [SerializeField] private int _price;

    private float _reloadTimer;

    public int Price => _price;
    public float Damage => _damage;
    public float Distance => _distance;

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

        _bulletPrefab.Spawn(this, direction);
        _reloadTimer = 0f;
    }

    private bool DetectEnemy(Vector3 direction) =>
        Physics.Raycast(transform.position, direction, out var hit, _bulletPrefab.Distance + _distance) &&
        hit.transform.TryGetComponent(out Enemy _);
}
