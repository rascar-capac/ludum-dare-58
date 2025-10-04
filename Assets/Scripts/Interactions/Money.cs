using UnityEngine;

public class Money : MonoBehaviour, IInteractable
{
    public GameObject Object;
    public int Value;

    public void StartInteraction(Vector2 mouseWorldPosition)
    {
        CollectMoney();
    }

    public void HoldInteraction(Vector2 mouseWorldPosition) { }
    public void StopInteraction() { }

    public void CollectMoney()
    {
        MoneyManager.Instance.CollectMoney(Value);
        Destroy(Object);
        //TODO: animation
    }
}
