using Rascar.Toolbox.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransparentOnMouseOver : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        CheckMouseOver();
    }

    public void CheckMouseOver()
    {
        if (!GameManager.Instance.GameIsRunning)
        {
            SetSpriteTransparent(false);

            return;
        }

        Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D[] hitList = Physics2D.RaycastAll(mousePosition, Vector2.zero, distance: Mathf.Infinity);

        foreach (RaycastHit2D hit in hitList)
        {
            if (hit.collider != null && hit.collider.tag == "Hans")
            {
                SetSpriteTransparent(true);

                return;
            }
        }

        SetSpriteTransparent(false);
    }

    private void SetSpriteTransparent(bool isTransparent)
    {
        SpriteRenderer.SetAlpha(isTransparent ? 0.5f : 1f);
    }
}
