using TMPro;
using UnityEngine;

public class BrokenObjectsUI : MonoBehaviour
{
    public TMP_Text Count;

    private void Awake()
    {
        BrokenObjectsLimiter.Instance.OnInitialized += BrokenObjectsLimiter_OnInitialized;
        BrokenObjectsLimiter.Instance.OnCountChanged += BrokenObjectsLimiter_OnCountChanged;
    }

    private void OnDestroy()
    {
        if (BrokenObjectsLimiter.TryGetInstance(out BrokenObjectsLimiter limiter))
        {
            limiter.OnCountChanged -= BrokenObjectsLimiter_OnCountChanged;
        }
    }

    private void BrokenObjectsLimiter_OnInitialized()
    {
        RefreshCount(withAnimation: false);
    }

    private void BrokenObjectsLimiter_OnCountChanged()
    {
        RefreshCount(withAnimation: true);
    }

    private void RefreshCount(bool withAnimation)
    {
        if (withAnimation)
        {
            //TODO: animation
        }

        Count.text = $"{BrokenObjectsLimiter.Instance.BrokenObjectsCount}/{BrokenObjectsLimiter.Instance.MaxBrokenObjectsCount}";
    }
}
