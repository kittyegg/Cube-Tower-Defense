using System;
using UnityEngine;

public class TowerHealthViewer : MonoBehaviour
{
    [SerializeField] private TowerHealth _towerHealth;

    private void OnEnable()
    {
        _towerHealth.OnHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _towerHealth.OnHealthChanged -= OnHealthChanged;
    }

    private static void OnHealthChanged(TowerHealth.OnHealthChangedEventArgs e)
    {
        print("Health changed: " + e.CurrentHealth);
    }
}
