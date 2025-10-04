using UnityEngine;
using UnityEngine.InputSystem;

public class Draggable : MonoBehaviour, IInteractable
{
    public Rigidbody2D Rigidbody;
    public SpriteRenderer SpriteRenderer;
    public Transform Object;

    private int _initialLayer;
    private int _initialSortingOrder;

    public void HoldInteraction(Vector2 mouseWorldPosition)
    {
        Object.transform.position = mouseWorldPosition;
    }

    public void StartInteraction(Vector2 mouseWorldPosition)
    {
        StartDrag();
    }

    public void StopInteraction()
    {
        StopDrag();
    }

    public void StartDrag()
    {
        Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        Rigidbody.gameObject.layer = LayerMask.NameToLayer("Dragged");
        Rigidbody.linearVelocity = Vector3.zero;
        Rigidbody.angularVelocity = 0f;
        SpriteRenderer.sortingOrder = 30;
    }

    public void StopDrag()
    {
        Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        Rigidbody.gameObject.layer = _initialLayer;
        SpriteRenderer.sortingOrder = _initialSortingOrder;

        Vector2 delta = Mouse.current.delta.value;
        delta *= 3;
        delta = new(Mathf.Clamp(delta.x, -100f, 100f), Mathf.Clamp(delta.y, -100f, 100f));

        Rigidbody.AddForce(delta, ForceMode2D.Impulse);
        Debug.Log(Mouse.current.delta.value * 3);
    }

    private void Awake()
    {
        _initialLayer = Rigidbody.gameObject.layer;
        _initialSortingOrder = SpriteRenderer.sortingOrder;
    }
}