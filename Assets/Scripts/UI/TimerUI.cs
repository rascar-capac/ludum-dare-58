using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public TMP_Text Timer;

    private void Awake()
    {
        TimeManager.Instance.OnTimerChanged += TimeManager_OnTimerChanged;
    }

    private void OnDestroy()
    {
        if (TimeManager.TryGetInstance(out TimeManager timeManager))
        {
            timeManager.OnTimerChanged -= TimeManager_OnTimerChanged;
        }
    }

    private void TimeManager_OnTimerChanged()
    {
        RefreshTimer();
    }

    private void RefreshTimer()
    {
        Timer.text = TimeManager.Instance.Timer.ToString("N2");
    }
}
