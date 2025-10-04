using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public Button ResumeButton;
    public Button RestartButton;
    public Button OptionsButton;
    public Button QuitButton;

    private void Awake()
    {
        GameManager.Instance.OnGamePauseChanged += GameManager_OnGamePaused;

        ResumeButton.onClick.AddListener(ResumeButton_OnClick);
        RestartButton.onClick.AddListener(RestartButton_OnClick);
        OptionsButton.onClick.AddListener(OptionsButton_OnClick);
        QuitButton.onClick.AddListener(QuitButton_OnClick);

        Close();
    }

    private void OnDestroy()
    {
        if (GameManager.TryGetInstance(out GameManager gameManager))
        {
            gameManager.OnGamePauseChanged -= GameManager_OnGamePaused;
        }

        ResumeButton.onClick.RemoveListener(ResumeButton_OnClick);
        RestartButton.onClick.RemoveListener(RestartButton_OnClick);
        OptionsButton.onClick.RemoveListener(OptionsButton_OnClick);
        QuitButton.onClick.RemoveListener(QuitButton_OnClick);
    }

    private void GameManager_OnGamePaused(bool isPaused)
    {
        if (isPaused)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    private void ResumeButton_OnClick()
    {
        GameManager.Instance.SetPause(false);
    }

    private void RestartButton_OnClick()
    {
        GameManager.Instance.StartGame();
    }

    private void OptionsButton_OnClick()
    {
        //TODO: open options menu
    }

    private void QuitButton_OnClick()
    {
        GameManager.Instance.QuitGame();
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

    private void Open()
    {
        gameObject.SetActive(true);
    }
}
