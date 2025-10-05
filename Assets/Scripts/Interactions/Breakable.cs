using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IInteractable, ITreasure
{
    [SerializeField] private GameObject _object;

    public GameObject Object => _object;
    public List<GameObject> Content;

    private void Awake()
    {
        TreasureProximityDetector.Instance.RegisterTreasure(this);
    }

    public void StartInteraction(Vector2 mouseWorldPosition)
    {
        Break();
    }

    public void HoldInteraction(Vector2 mouseWorldPosition) { }
    public void StopInteraction() { }

    public void Break()
    {
        //TODO: break into multiple pieces

        foreach (GameObject content in Content)
        {
            Instantiate(content, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f), Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward));
        }

        Destroy(Object);
    }
}
