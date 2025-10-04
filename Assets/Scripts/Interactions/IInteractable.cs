using UnityEngine;

public interface IInteractable
{
    void StartInteraction(Vector2 mouseWorldPosition);
    void HoldInteraction(Vector2 mouseWorldPosition);
    void StopInteraction();
}
