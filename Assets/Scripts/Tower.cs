using System;
using UnityEngine;

[RequireComponent(typeof(TowerHealth))]
public class Tower : MonoBehaviour
{
    private TowerHealth _towerHealth;

    private void Start()
    {
        _towerHealth = GetComponent<TowerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            _towerHealth.TakeDamage(enemy.Damage);
    }
}
