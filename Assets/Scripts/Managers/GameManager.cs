using System;
using Rascar.Toolbox;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : Singleton<GameManager>
{
    public InputActionReference _pauseInput;

    public event Action OnGameReady;
    public event Action OnGameStarted;
    public event Action<bool, int> OnGameStopped;
    public event Action<bool> OnGamePauseChanged;
    public bool GameIsRunning;
    public bool GameIsPaused;

    protected override void Awake()
    {
        base.Awake();

        _pauseInput.action.performed += _ => TogglePause();
    }

    private void Start()
    {
        OnGameReady?.Invoke();
    }

    public void StartGame()
    {
        GameIsRunning = true;
        SetPause(false);

        OnGameStarted?.Invoke();
    }

    public void StopGame()
    {
        bool isWon = MoneyManager.Instance.MoneyAmount >= MoneyManager.Instance.Goal && TimeManager.Instance.Timer > 0f;
        int score = MoneyManager.Instance.MoneyAmount;

        GameIsRunning = false;
        SetPause(false);

        OnGameStopped?.Invoke(isWon, score);
    }

    public void SetPause(bool isPaused)
    {
        if (isPaused && !GameIsRunning)
        {
            return;
        }

        GameIsPaused = isPaused;
        Time.timeScale = GameIsPaused ? 0f : 1f;
        OnGamePauseChanged?.Invoke(GameIsPaused);
    }

    public void TogglePause()
    {
        SetPause(!GameIsPaused);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}