using UnityEngine;

public class ActivateOnGameStarted : MonoBehaviour
{
    public GameObject Target;

    private void Awake()
    {
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnGameStopped += GameManager_OnGameStopped;

        SetActive(GameManager.Instance.GameIsStarted);
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
        SetActive(true);
    }

    private void GameManager_OnGameStopped(bool isWon, int score)
    {
        SetActive(false);
    }

    private void SetActive(bool isActive)
    {
        Target.SetActive(isActive);
    }
}
