using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TowerHealth))]
public class Tower : MonoBehaviour
{
    [SerializeField] private UnityEvent _onGameOver;

    public event UnityAction OnGameOver
    {
        add => _onGameOver.AddListener(value);
        remove => _onGameOver.RemoveListener(value);
    }

    private TowerHealth _towerHealth;

    private void OnEnable()
    {
        _towerHealth = GetComponent<TowerHealth>();
        _towerHealth.OnHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _towerHealth.OnHealthChanged -= OnHealthChanged;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            _towerHealth.TakeDamage(enemy.Damage);
    }

    private void OnHealthChanged(TowerHealth.OnHealthChangedEventArgs args)
    {
        if (args.CurrentHealth <= 0)
            _onGameOver?.Invoke();
    }
}
