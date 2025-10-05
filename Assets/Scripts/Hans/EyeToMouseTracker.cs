using UnityEngine;
using UnityEngine.InputSystem;

public class EyeToMouseTracker : MonoBehaviour
{
    public Transform Eye;
    public float MouseToEyeDistanceRatio;
    public float EyeSpeed;

    private Camera _mainCamera;
    private Vector2 _initialPosition;
    private Vector3 _currentVelocity;

    //à partir d’une certaine distance de son oeil, on ajoute un multiplicateur de distance pour simuler la proximité et le faire loucher

    private void Awake()
    {
        _mainCamera = Camera.main;
        _initialPosition = Eye.position;
    }

    private void Update()
    {
        Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 eyePosition = Eye.position;
        Vector2 eyeToMouseDirection = (mousePosition - eyePosition).normalized;
        float mouseDistance = (eyePosition - mousePosition).magnitude;

        Vector2 targetPosition = _initialPosition + MouseToEyeDistanceRatio * mouseDistance * eyeToMouseDirection;

        Eye.position = Vector3.SmoothDamp(Eye.position, targetPosition, ref _currentVelocity, EyeSpeed);
    }
}
