using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class TurretsShopViewer : MonoBehaviour
{
    [SerializeField] private TurretsShop _shop;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Button _buttonPrefab;

    private Animator _animator;
    private bool _hidden = true;
    private readonly Dictionary<Button, TurretShopItem> _buttons = new();

    private static readonly int _hideAnimation = Animator.StringToHash("HideTurretShopPanelAnimation");
    private static readonly int _showAnimation = Animator.StringToHash("ShowTurretShopPanelAnimation");

    private void Start()
    {
        foreach (var item in _shop.TurretShopItems)
        {
            var button = Instantiate(_buttonPrefab, transform);
            var icon = button.GetComponentInChildrenWithoutParent<Image>();
            if (icon != null && item.Icon != null)
            {
                icon.gameObject.SetActive(true);
                icon.sprite = item.Icon;
            }

            button.onClick.AddListener(() => _shop.SelectTurret(item));
            _buttons.Add(button, item);
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _gameManager.OnStateChanged += OnGameStateChanged;
        _wallet.OnMoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _gameManager.OnStateChanged -= OnGameStateChanged;
        _wallet.OnMoneyChanged -= OnMoneyChanged;
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        if (!_hidden || state != GameManager.GameState.BuildingTurrets)
            return;

        _animator.Play(_showAnimation);
        _hidden = false;
    }

    private void OnMoneyChanged(int money)
    {
        foreach (var (button, item) in _buttons)
        {
            button.interactable = money >= item.Price;
        }
    }
}
