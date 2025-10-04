using TMPro;
using UnityEngine;

public class MoneyCountUI : MonoBehaviour
{
    public TMP_Text Text;

    private void Awake()
    {
        MoneyManager.Instance.OnInitialized += MoneyManager_OnInitialized;
        MoneyManager.Instance.OnMoneyChanged += MoneyManager_OnMoneyChanged;
    }

    private void OnDestroy()
    {
        MoneyManager.Instance.OnMoneyChanged -= MoneyManager_OnMoneyChanged;
    }

    private void MoneyManager_OnInitialized()
    {
        RefreshText(withAnimation: false);
    }

    private void MoneyManager_OnMoneyChanged()
    {
        RefreshText(withAnimation: true);
    }

    private void RefreshText(bool withAnimation)
    {
        if (withAnimation)
        {
            //TODO: animation
        }

        Text.text = MoneyManager.Instance.MoneyAmount.ToString();
    }
}
