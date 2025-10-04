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
        TimeManager.Instance.OnTimerChanged -= TimeManager_OnTimerChanged;
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
