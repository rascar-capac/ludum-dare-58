using System;
using Rascar.Toolbox;
using UnityEngine;

public class MoneyManager : Singleton<MoneyManager>
{
    public int Goal;

    public int MoneyAmount;

    public event Action OnInitialized;
    public event Action OnMoneyChanged;

    private void Start()
    {
        //TODO: subscribe to game event to trigger initialization
        Initialize();
    }

    public void CollectMoney(int amount)
    {
        MoneyAmount += amount;

        OnMoneyChanged?.Invoke();

        if (MoneyAmount >= Goal)
        {
            Debug.Log("WIN");
        }
    }

    public void Initialize()
    {
        MoneyAmount = 0;

        OnInitialized?.Invoke();
    }
}
