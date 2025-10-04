using UnityEngine;

public class Money : MonoBehaviour, IInteractable
{
    public int Value;

    public void Interact()
    {
        CollectMoney();
    }

    public void CollectMoney()
    {
        MoneyManager.Instance.CollectMoney(Value);
        Destroy(gameObject);
        //TODO: animation
    }
}
