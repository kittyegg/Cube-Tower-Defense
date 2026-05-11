using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private TurretBullet _bulletPrefab;
    [SerializeField] private float _reloadTime = 1f;
    [SerializeField] private float _damage;
    [SerializeField] private float _distance;

    private float _reloadTimer;

    private void Update()
    {
        if (_reloadTimer >= _reloadTime)
        {
            SpawnBullet(Vector3.forward);
            SpawnBullet(Vector3.back);
            SpawnBullet(Vector3.left);
            SpawnBullet(Vector3.right);
            
            _reloadTimer = 0f;
        }
        
        _reloadTimer += Time.deltaTime;
    }

    private void SpawnBullet(Vector3 direction)
    {
        if (!DetectEnemy(direction))
            return;
        
        var rot = transform.rotation;
        rot.SetLookRotation(direction);
        Instantiate(_bulletPrefab, transform.position, rot, transform);
    }

    private bool DetectEnemy(Vector3 direction) =>
        Physics.Raycast(transform.position, direction, out var hit, _bulletPrefab.Distance + _distance) &&
        hit.transform.TryGetComponent(out Enemy _);
}
