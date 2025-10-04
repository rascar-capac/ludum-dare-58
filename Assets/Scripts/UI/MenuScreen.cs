#if UNITY_EDITOR
using TMPro;
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    public Button StartButton;
    public Button OptionsButton;
    public Button QuitButton;
    public GameObject StatsContainer;
    public TMP_Text GameStatus;
    public TMP_Text Score;

    private void Awake()
    {
        GameManager.Instance.OnGameReady += GameManager_OnGameReady;
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnGameStopped += GameManager_OnGameStopped;

        StartButton.onClick.AddListener(StartButton_OnClick);
        OptionsButton.onClick.AddListener(OptionsButton_OnClick);
        QuitButton.onClick.AddListener(QuitButton_OnClick);

        Close();
    }

    private void OnDestroy()
    {
        if (GameManager.TryGetInstance(out GameManager gameManager))
        {
            gameManager.OnGameReady -= GameManager_OnGameReady;
            gameManager.OnGameStarted -= GameManager_OnGameStarted;
            gameManager.OnGameStopped -= GameManager_OnGameStopped;
        }

        StartButton.onClick.RemoveListener(StartButton_OnClick);
        OptionsButton.onClick.RemoveListener(OptionsButton_OnClick);
        QuitButton.onClick.RemoveListener(QuitButton_OnClick);
    }

    private void GameManager_OnGameReady()
    {
        Refresh();
    }

    private void GameManager_OnGameStarted()
    {
        Close();
    }

    private void GameManager_OnGameStopped(bool isWon, int score)
    {
        Refresh(isWon, score);
    }

    private void StartButton_OnClick()
    {
        GameManager.Instance.StartGame();
    }

    private void OptionsButton_OnClick()
    {
        //TODO: open options menu
    }

    private void QuitButton_OnClick()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

    private void Refresh()
    {
        gameObject.SetActive(true);

        StatsContainer.SetActive(false);
    }

    private void Refresh(bool isWon, int score)
    {
        gameObject.SetActive(true);

        StatsContainer.SetActive(true);
        GameStatus.text = isWon ? "Tax collected!" : "You didn't collect enough…";
        Score.text = score.ToString();
    }
}
