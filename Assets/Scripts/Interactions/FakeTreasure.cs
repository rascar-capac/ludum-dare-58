using UnityEngine;

public class FakeTreasure : MonoBehaviour, IInteractable, ITreasure
{
    [SerializeField] private GameObject _object;

    public GameObject Object => _object;

    private void Awake()
    {
        TreasureProximityDetector.Instance.RegisterTreasure(this);
    }

    public void StartInteraction(Vector2 mouseWorldPosition)
    {
        Disappear();
    }

    public void HoldInteraction(Vector2 mouseWorldPosition) { }
    public void StopInteraction() { }

    public void Disappear()
    {
        TreasureProximityDetector.Instance.UnregisterTreasure(this);
        Destroy(Object);
        //TODO: animation
    }
}
