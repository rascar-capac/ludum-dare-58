using System;
using Rascar.Toolbox;

public class GameManager : Singleton<GameManager>
{
    public event Action OnGameReady;
    public event Action OnGameStarted;
    public event Action<bool, int> OnGameStopped;
    public bool GameIsRunning;

    private void Start()
    {
        OnGameReady?.Invoke();
    }

    public void StartGame()
    {
        GameIsRunning = true;

        OnGameStarted?.Invoke();
    }

    public void StopGame()
    {
        bool isWon = MoneyManager.Instance.MoneyAmount >= MoneyManager.Instance.Goal && TimeManager.Instance.Timer > 0f;
        int score = MoneyManager.Instance.MoneyAmount;

        GameIsRunning = false;

        OnGameStopped?.Invoke(isWon, score);
    }
}