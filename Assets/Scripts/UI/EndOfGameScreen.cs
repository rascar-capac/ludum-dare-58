using TMPro;
using UnityEngine;

public class EndOfGameScreen : MonoBehaviour
{
    public TMP_Text GameStatus;
    public TMP_Text Score;

    private void Awake()
    {
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnGameStopped += GameManager_OnGameStopped;

        Close();
    }

    private void OnDestroy()
    {
        if (GameManager.TryGetInstance(out GameManager gameManager))
        {
            gameManager.OnGameStarted -= GameManager_OnGameStarted;
            gameManager.OnGameStopped -= GameManager_OnGameStopped;
        }
    }

    private void GameManager_OnGameStarted()
    {
        Close();
    }

    private void GameManager_OnGameStopped(bool isWon, int score)
    {
        Refresh(isWon, score);
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

    private void Refresh(bool isWon, int score)
    {
        gameObject.SetActive(true);

        GameStatus.text = isWon ? "Tax collected!" : "You didn't collect enough…";
        Score.text = score.ToString();
    }
}
