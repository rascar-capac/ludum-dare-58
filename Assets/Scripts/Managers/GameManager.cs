using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : Singleton<GameManager>
{
    public InputActionReference _pauseInput;

    public event Action OnGameReady;
    public event Action<bool> OnGameStarted;
    public event Action<bool, int> OnGameStopped;
    public event Action<bool> OnGamePauseChanged;
    public bool GameIsStarted;
    public bool GameIsPaused;
    public bool GameIsRunning => GameIsStarted && !GameIsPaused;
    public bool GameHasBeenStartedOnce;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        GameIsStarted = true;
        SetPause(false);

        OnGameStarted?.Invoke(!GameHasBeenStartedOnce);

        GameHasBeenStartedOnce = true;
    }

    public void StopGame()
    {
        bool isWon = MoneyManager.Instance.MoneyAmount >= MoneyManager.Instance.Goal /*&& TimeManager.Instance.Timer > 0f */ && BrokenObjectsLimiter.Instance.BrokenObjectsCount < BrokenObjectsLimiter.Instance.MaxBrokenObjectsCount;
        int score = MoneyManager.Instance.MoneyAmount;

        GameIsStarted = false;
        SetPause(false);

        OnGameStopped?.Invoke(isWon, score);
    }

    public void SetPause(bool isPaused)
    {
        if (isPaused && !GameIsStarted)
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