using UnityEngine;
using Zenject;
public class LookController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _mouseSensitivity = 2.0f;
    private Controls _controls;
    private Vector2 _lookDirection;

    [Inject]
    private void Construct(Controls controls)
    {
        _controls = controls;
    }
    private void OnEnable()
    {
        _controls.Player.Look.performed += context => _lookDirection = context.ReadValue<Vector2>();
        _controls.Player.Look.canceled += context => _lookDirection = Vector2.zero;
    }
    private void OnDisable()
    {
        _controls.Player.Look.performed -= context => _lookDirection = context.ReadValue<Vector2>();
        _controls.Player.Look.canceled -= context => _lookDirection = Vector2.zero;
    }

    private void Update()
    {
        Look();
    }
    private void Look()
    {
        transform.Rotate(0, _lookDirection.x * _mouseSensitivity, 0);

        float newCameraEulerAnglesX = _cameraTransform.eulerAngles.x - _lookDirection.y * _mouseSensitivity;

        newCameraEulerAnglesX = ClampAngle(newCameraEulerAnglesX, -30, 30);

        _cameraTransform.eulerAngles = new Vector3(newCameraEulerAnglesX, _cameraTransform.eulerAngles.y, 0);
    }
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle > 180) angle -= 360;

        angle = Mathf.Clamp(angle, min, max);
        return angle;

    }
}
