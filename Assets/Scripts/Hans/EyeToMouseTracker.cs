using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class EyeToMouseTracker : MonoBehaviour
{
    public Transform Eye;
    public float MaxEyeDistance;
    public float MaxMouseDistance;
    public float EyeSpeed;
    public float DistanceWithMinProximityMultiplier;
    public float DistanceWithMaxProximityMultiplier;

    private Camera _mainCamera;
    private Vector2 _initialPosition;
    private Vector3 _currentVelocity;

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
        float eyeDistance = MaxEyeDistance / MaxMouseDistance * mouseDistance;
        eyeDistance = Mathf.Clamp(math.remap(DistanceWithMinProximityMultiplier, DistanceWithMaxProximityMultiplier, eyeDistance, MaxEyeDistance, eyeDistance), eyeDistance, MaxEyeDistance);

        Vector2 targetPosition = _initialPosition + eyeDistance * eyeToMouseDirection;

        Eye.position = Vector3.SmoothDamp(Eye.position, targetPosition, ref _currentVelocity, EyeSpeed);
    }
}
