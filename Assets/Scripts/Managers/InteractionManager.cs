using Rascar.Toolbox;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : Singleton<InteractionManager>
{
    public LayerMask InteractionMask;
    private Camera _mainCamera;
    public IInteractable CurrentInteractable;

    protected override void Awake()
    {
        base.Awake();

        _mainCamera = Camera.main;
        GameManager.Instance.OnGameStopped += GameManager_OnGameStopped;
    }

    private void OnDestroy()
    {
        if (GameManager.TryGetInstance(out GameManager gameManager))
        {
            gameManager.OnGameStopped -= GameManager_OnGameStopped;
        }
    }

    private void Update()
    {
        CheckInteractions();
    }

    public void CheckInteractions()
    {
        if (!GameManager.Instance.GameIsRunning || GameManager.Instance.GameIsPaused)
        {
            return;
        }

        IInteractable previousInteractable = CurrentInteractable;
        CurrentInteractable = null;

        if (Mouse.current.leftButton.isPressed)
        {
            Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if (previousInteractable != null)
            {
                CurrentInteractable = previousInteractable;
            }
            else
            {
                RaycastHit2D[] hitList = Physics2D.RaycastAll(mousePosition, Vector2.zero, distance: Mathf.Infinity, layerMask: InteractionMask);

                foreach (RaycastHit2D hit in hitList)
                {
                    if (hit.collider != null && hit.collider.TryGetComponent(out CurrentInteractable))
                    {
                        break;
                    }
                }
            }

            if (CurrentInteractable != null)
            {
                if (CurrentInteractable != previousInteractable)
                {
                    CurrentInteractable.StartInteraction(mousePosition);
                }

                if (CurrentInteractable != null)
                {
                    CurrentInteractable.HoldInteraction(mousePosition);
                }
            }
        }

        if (previousInteractable != null && CurrentInteractable != previousInteractable)
        {
            previousInteractable.StopInteraction();
        }
    }

    private void GameManager_OnGameStopped(bool isWon, int score)
    {
        CleanUp();
    }

    public void CleanUp()
    {
        CurrentInteractable = null;
    }
}
