using UnityEngine;

public class Money : MonoBehaviour, IInteractable, ITreasure
{
    [SerializeField] private GameObject _object;

    public GameObject Object => _object;
    public int Value;

    private void Awake()
    {
        TreasureProximityDetector.Instance.RegisterTreasure(this);
    }

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
