using System;

public class BrokenObjectsLimiter : Singleton<BrokenObjectsLimiter>
{
    public int MaxBrokenObjectsCount;

    public int BrokenObjectsCount;

    public event Action OnInitialized;
    public event Action OnCountChanged;

    protected override void Awake()
    {
        base.Awake();

        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        Breakable.OnBroken += Breakable_OnBroken;
    }

    private void OnDestroy()
    {
        if (GameManager.TryGetInstance(out GameManager gameManager))
        {
            gameManager.OnGameStarted -= GameManager_OnGameStarted;
        }

        Breakable.OnBroken -= Breakable_OnBroken;
    }

    private void GameManager_OnGameStarted(bool isFirstGame)
    {
        Initialize();
    }

    private void Breakable_OnBroken(Breakable breakable)
    {
        IncrementBrokenObject();
    }

    public void IncrementBrokenObject()
    {
        BrokenObjectsCount++;

        OnCountChanged?.Invoke();

        if (BrokenObjectsCount >= MaxBrokenObjectsCount)
        {
            GameManager.Instance.StopGame();
        }
    }

    public void Initialize()
    {
        BrokenObjectsCount = 0;

        OnInitialized?.Invoke();
    }
}
