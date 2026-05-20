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
    [SerializeField] private UnityEvent _onDestroyTurretSelected;
    [SerializeField] private UnityEvent _onDestroyTurretDeselected;

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

    public event UnityAction OnDestroyTurretSelected
    {
        add => _onDestroyTurretSelected.AddListener(value);
        remove => _onDestroyTurretSelected.RemoveListener(value);
    }

    public event UnityAction OnDestroyTurretDeselected
    {
        add => _onDestroyTurretDeselected.AddListener(value);
        remove => _onDestroyTurretDeselected.RemoveListener(value);
    }

    public IReadOnlyCollection<TurretShopItem> TurretShopItems => _turretShopItems;

    private TurretShopItem _selectedTurret;
    private bool _isTurretSelected = false;
    private bool _isDestroySelected = false;

    public void SelectTurret(TurretShopItem item)
    {
        _isTurretSelected = _wallet.Money >= item.Price;
        if (!_isTurretSelected)
            return;

        if (_isDestroySelected)
            DeselectDestroyTurret();

        _selectedTurret = item;
        _onTurretSelected?.Invoke(item);
    }

    public void DeselectTurret()
    {
        _isTurretSelected = false;
        _onTurretDeselected?.Invoke();
    }

    public void SelectDestroyTurret()
    {
        if (_isTurretSelected)
            DeselectTurret();

        _isDestroySelected = true;
        _onDestroyTurretSelected?.Invoke();
    }

    public void DeselectDestroyTurret()
    {
        _isDestroySelected = false;
        _onDestroyTurretDeselected?.Invoke();
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

    private void DeselectActionPerformed(InputAction.CallbackContext _)
    {
        if (_isTurretSelected)
            DeselectTurret();
        else if (_isDestroySelected)
            DeselectDestroyTurret();
    }

    private void OnBlockClicked(BlockScript block)
    {
        if (block.Type == BlockScript.BlockType.Turret)
        {
            DestroyTurret(block.GetComponent<Turret>());
            return;
        }

        if (!block.CompareTag("GrassBlock") || !_isTurretSelected || _selectedTurret.Price > _wallet.Money)
            return;

        var pos = block.transform.position;
        pos.y += 1;
        Instantiate(_selectedTurret.TurretPrefab, pos, Quaternion.identity, _turretsHolder);

        _wallet.TakeMoney(_selectedTurret.Price);
        if (_wallet.Money < _selectedTurret.Price)
            DeselectTurret();
    }

    private void DestroyTurret(Turret turret)
    {
        if (turret == null || !_isDestroySelected)
            return;

        _wallet.AddMoney(turret.SellPrice);
        Destroy(turret.gameObject);
    }
}
