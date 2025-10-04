using System;
using Rascar.Toolbox;

public class MoneyManager : Singleton<MoneyManager>
{
    public int Goal;

    public int MoneyAmount;

    public event Action OnInitialized;
    public event Action OnMoneyChanged;

    protected override void Awake()
    {
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStarted -= GameManager_OnGameStarted;
    }

    public void CollectMoney(int amount)
    {
        MoneyAmount += amount;

        OnMoneyChanged?.Invoke();

        if (MoneyAmount >= Goal)
        {
            GameManager.Instance.StopGame();
        }
    }

    public void Initialize()
    {
        MoneyAmount = 0;

        OnInitialized?.Invoke();
    }

    private void GameManager_OnGameStarted()
    {
        Initialize();
    }
}
