using System;
using System.Collections.Generic;
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

    public void UnregisterTreasure(ITreasure treasure)
    {
        _treasureList.Remove(treasure);
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
            if ((ToVector2(treasure.Object.transform.position) - mousePosition).sqrMagnitude < FreakingMaxDistance * FreakingMaxDistance)
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

    public Vector2 ToVector2(Vector3 vector, EAxis ignoredAxis = EAxis.Z)
    {
        return ignoredAxis switch
        {
            EAxis.X => new Vector2(vector.y, vector.z),
            EAxis.Y => new Vector2(vector.x, vector.z),
            EAxis.Z => new Vector2(vector.x, vector.y),
            _ => throw new ArgumentException($"Tried to ignore wrong axis type {ignoredAxis}"),
        };
    }

    public enum EAxis
    {
        X,
        Y,
        Z,
    }
}