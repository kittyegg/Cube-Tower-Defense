using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TurretUpdater : MonoBehaviour
{
    [SerializeField] private BlockDetector _blockDetector;
    [SerializeField] private TurretsShop _shop;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private UnityEvent<Turret> _onTurretClicked;

    public event UnityAction<Turret> OnTurretClicked
    {
        add => _onTurretClicked.AddListener(value);
        remove => _onTurretClicked.RemoveListener(value);
    }

    private Turret _clickedTurret;
    private bool _isDestroyingTurret;

    private Turret ClickedTurret
    {
        get => _clickedTurret;
        set
        {
            _clickedTurret = value;
            _onTurretClicked?.Invoke(value);
        }
    }

    private void OnEnable()
    {
        _blockDetector.OnBlockClicked += OnBlockClicked;
        _shop.OnDestroyTurretSelected += OnDestoyTurretSelected;
        _shop.OnDestroyTurretDeselected += OnDestroyTurretDeselected;
    }

    private void OnDisable()
    {
        _blockDetector.OnBlockClicked -= OnBlockClicked;
        _shop.OnDestroyTurretSelected -= OnDestoyTurretSelected;
        _shop.OnDestroyTurretDeselected -= OnDestroyTurretDeselected;
    }

    private void Start()
    {
        _sellButton.onClick.AddListener(SellTurret);
        _upgradeButton.onClick.AddListener(UpgradeTurret);
    }

    private void OnBlockClicked(BlockScript block)
    {
        if (_isDestroyingTurret)
            return;

        if (_clickedTurret != null)
            ClickedTurret = null;

        if (block.Type == BlockScript.BlockType.Turret)
            ClickedTurret = block.GetComponent<Turret>();
    }

    private void OnDestoyTurretSelected()
    {
        _isDestroyingTurret = true;
        ClickedTurret = null;
    }

    private void OnDestroyTurretDeselected()
    {
        _isDestroyingTurret = false;
        ClickedTurret = null;
    }

    private void UpgradeTurret()
    {
        if (ClickedTurret == null || _wallet.Money < ClickedTurret.UpgradeCost)
            return;

        _wallet.TakeMoney(ClickedTurret.UpgradeCost);
        ClickedTurret.Upgrade();
    }

    private void SellTurret()
    {
        if (ClickedTurret == null)
            return;

        _wallet.AddMoney(ClickedTurret.SellPrice);
        Destroy(ClickedTurret.gameObject);
        ClickedTurret = null;
    }
}
