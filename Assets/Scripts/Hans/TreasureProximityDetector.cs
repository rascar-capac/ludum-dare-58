using System.Collections.Generic;
using Rascar.Toolbox;
using Rascar.Toolbox.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

public class TreasureProximityDetector : Singleton<TreasureProximityDetector>
{
    public float FreakingMaxDistance;
    public Animator HansAnimator;
    private Camera _mainCamera;
    public List<ITreasure> _treasureList = new();
    public bool GameHasBeenStartedOnce;

    protected override void Awake()
    {
        base.Awake();

        _mainCamera = Camera.main;

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

    public void RegisterTreasure(ITreasure treasure)
    {
        _treasureList.Add(treasure);
    }

    private void Update()
    {
        CheckTreasureProximity();
    }

    private void CheckTreasureProximity()
    {
        if (!GameManager.Instance.GameIsRunning)
        {
            return;
        }

        Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        foreach (ITreasure treasure in _treasureList)
        {
            if ((treasure.Object.transform.position.ToVector2() - mousePosition).sqrMagnitude < FreakingMaxDistance)
            {
                SetFreaking(true);

                return;
            }
        }

        SetFreaking(false);
    }

    public void SetFreaking(bool isFreaking)
    {
        HansAnimator.SetBool("freaking", isFreaking);
    }

    private void GameManager_OnGameStarted(bool isFirstGame)
    {
        if (!isFirstGame)
        {
            _treasureList.Clear();
        }
    }

    private void GameManager_OnGameStopped(bool isWon, int score)
    {
        SetFreaking(false);
    }
}