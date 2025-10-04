using System;
using Rascar.Toolbox;

public class GameManager : Singleton<GameManager>
{
    public event Action OnGameStarted;
    public event Action<bool, int> OnGameStopped;

    private void Start()
    {
        //TODO: hook to the menu
        StartGame();
    }

    public void StartGame()
    {
        OnGameStarted?.Invoke();
    }

    public void StopGame()
    {
        bool isWon = MoneyManager.Instance.MoneyAmount >= MoneyManager.Instance.Goal && TimeManager.Instance.Timer > 0f;
        int score = MoneyManager.Instance.MoneyAmount;

        OnGameStopped?.Invoke(isWon, score);
    }
}