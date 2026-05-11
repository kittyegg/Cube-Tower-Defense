using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TurretsShop : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private BlockDetector _blockDetector;
    [SerializeField] private Transform _turretsHolder;
    [SerializeField] private InputAction _deselectAction;
    [SerializeField] private TurretShopItem[] _turretShopItems;
    [SerializeField] private UnityEvent<TurretShopItem> _onTurretSelected;
    [SerializeField] private UnityEvent _onTurretDeselected;

    public event UnityAction<TurretShopItem> OnTurretSelected
    {
        add => _onTurretSelected.AddListener(value);
        remove => _onTurretSelected.RemoveListener(value);
    }

    public event UnityAction OnTurretDeselected
    {
        add => _onTurretDeselected.AddListener(value);
        remove => _onTurretDeselected.RemoveListener(value);
    }

    public IReadOnlyCollection<TurretShopItem> TurretShopItems => _turretShopItems;

    private TurretShopItem _selectedTurret;
    private bool _isTurretSelected = false;
    
    public void SelectTurret(TurretShopItem item)
    {
        _isTurretSelected = _wallet.Money >= item.Price;
        if (!_isTurretSelected)
            return;

        _selectedTurret = item;
        _onTurretSelected?.Invoke(item);
    }

    public void DeselectTurret()
    {
        _isTurretSelected = false;
        _onTurretDeselected?.Invoke();
    }

    private void OnEnable()
    {
        _deselectAction.Enable();
        _deselectAction.performed += DeselectActionPerformed;

        _blockDetector.OnBlockClicked += OnBlockClicked;
    }
    private void OnDisable()
    {
        _deselectAction.performed -= DeselectActionPerformed;
        _deselectAction.Disable();

        _blockDetector.OnBlockClicked -= OnBlockClicked;
    }

    private void Start()
    {
        if (_turretsHolder == null)
            _turretsHolder = transform;
    }

    private void DeselectActionPerformed(InputAction.CallbackContext _) => DeselectTurret();

    private void OnBlockClicked(BlockScript block)
    {
        if (!block.CompareTag("GrassBlock") || !_isTurretSelected || _selectedTurret.Price > _wallet.Money)
            return;

        var pos = block.transform.position;
        pos.y += 1;
        Instantiate(_selectedTurret.TurretPrefab, pos, Quaternion.identity, _turretsHolder);

        _wallet.TakeMoney(_selectedTurret.Price);
        if (_wallet.Money < _selectedTurret.Price)
            DeselectTurret();
    }
}
