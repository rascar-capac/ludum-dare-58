using UnityEngine;

public class Money : MonoBehaviour, IInteractable
{
    public GameObject Object;
    public int Value;

    public void Interact()
    {
        CollectMoney();
    }

    public void CollectMoney()
    {
        MoneyManager.Instance.CollectMoney(Value);
        Destroy(Object);
        //TODO: animation
    }
}
