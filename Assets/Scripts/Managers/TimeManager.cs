using System;
using Rascar.Toolbox;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    public float CountdownDuration;
    public float Timer;
    public bool CountdownIsRunning;

    public event Action OnTimerChanged;

    protected override void Awake()
    {
        base.Awake();

        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnGameStopped += GameManager_OnGameStopped;
    }

    private void OnDestroy()
    {
        if (GameManager.TryGetInstance(out GameManager gameManager))
        {
            gameManager.OnGameStarted -= GameManager_OnGameStarted;
            gameManager.OnGameStopped -= GameManager_OnGameStopped;
        }
    }

    private void GameManager_OnGameStarted(bool isFirstGame)
    {
        StartCountdown();
    }

    private void GameManager_OnGameStopped(bool isWon, int score)
    {
        StopCountdown();
    }

    private void StartCountdown()
    {
        Timer = CountdownDuration;
        CountdownIsRunning = true;
    }

    private void StopCountdown()
    {
        Timer = 0f;
        CountdownIsRunning = false;
    }

    private void Update()
    {
        UpdateCountdown();
    }

    private void UpdateCountdown()
    {
        if (!CountdownIsRunning)
        {
            return;
        }

        Timer -= Time.deltaTime;

        OnTimerChanged?.Invoke();

        if (Timer < 0f)
        {
            GameManager.Instance.StopGame();
        }
    }
}