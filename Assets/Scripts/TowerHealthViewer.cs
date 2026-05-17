using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class TowerHealthViewer : MonoBehaviour
{
    [SerializeField] private TowerHealth _towerHealth;

    private Slider _slider;

    private void OnEnable()
    {
        _towerHealth.OnHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _towerHealth.OnHealthChanged -= OnHealthChanged;
    }

    private void Start()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnHealthChanged(TowerHealth.OnHealthChangedEventArgs e)
    {
        _slider.value = _slider.maxValue / e.MaxHealth * e.CurrentHealth;
    }
}
