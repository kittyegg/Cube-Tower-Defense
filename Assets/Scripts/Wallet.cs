using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _money;
    [SerializeField] private UnityEvent<int> _onMoneyChanged;
    
    public event UnityAction<int> OnMoneyChanged
    {
        add => _onMoneyChanged.AddListener(value);
        remove => _onMoneyChanged.RemoveListener(value);
    }
    
    public int Money
    {
        get => _money;
        private set
        {
            _money = value;
            _onMoneyChanged?.Invoke(value);
        }
    }
    
    public void AddMoney(int money) => Money += money;

    public bool TakeMoney(int money)
    {
        if (Money < money)
            return false;
        
        Money -= money;
        return true;
    }
}
