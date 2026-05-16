using System;
using UnityEngine;
using UnityEngine.Events;

public class TowerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth = 100f;
    
    [SerializeField] private UnityEvent<OnHealthChangedEventArgs> _onHealthChanged;
    
    public event UnityAction<OnHealthChangedEventArgs> OnHealthChanged
    {
        add => _onHealthChanged.AddListener(value);
        remove => _onHealthChanged.RemoveListener(value);
    }
    
    public float MaxHealth
    {
        get => _maxHealth;
        private set
        {
            _maxHealth = value;
            _onHealthChanged?.Invoke(new(_currentHealth, _maxHealth));
        }
    }

    public float CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            _currentHealth = value;
            _onHealthChanged?.Invoke(new(_currentHealth, _maxHealth));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Enemy enemy))
            return;

        TakeDamage(enemy.Damage);
        Destroy(enemy.gameObject);
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    public class OnHealthChangedEventArgs : EventArgs
    {
        public readonly float CurrentHealth;
        public readonly float MaxHealth;

        public OnHealthChangedEventArgs(float currentHealth, float maxHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }
    }
}
