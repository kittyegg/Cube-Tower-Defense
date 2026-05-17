using UnityEngine;

public class MoneyViewer : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMPro.TextMeshProUGUI _moneyText;

    private void OnEnable()
    {
        _wallet.OnMoneyChanged += OnMoneyChanged;
        OnMoneyChanged(_wallet.Money);
    }

    private void OnDisable()
    {
        _wallet.OnMoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        _moneyText.text = money.ToString();
    }
}
