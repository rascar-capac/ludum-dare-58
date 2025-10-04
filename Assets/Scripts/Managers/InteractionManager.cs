using Rascar.Toolbox;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : Singleton<InteractionManager>
{
    private Camera _mainCamera;

    protected override void Awake()
    {
        base.Awake();

        _mainCamera = Camera.main;
    }

    private void Update()
    {
        CheckInteractions();
    }

    public void CheckInteractions()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }
}
