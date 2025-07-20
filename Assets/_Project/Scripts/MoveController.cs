using UnityEngine;
using Zenject;
[RequireComponent(typeof(CharacterController))]

public class MoveController : MonoBehaviour
{
    private CharacterController _characterController;
    private Controls _controls;
    [SerializeField] private float _moveSpeed; 
    [SerializeField] private float _jumpForce = 8.0f; 
    [SerializeField] private float _gravity = 40.0f; 
    [SerializeField] private float _accelerate = 1f;
    private bool _isAccelerate = false;
    private Vector3 _velocity;
    private Vector2 _moveDirection;

    [Inject]
    private void Construct(Controls controls)
    {
        _controls = controls;
    }

    private void OnEnable()
    {
        _characterController = GetComponent<CharacterController>();
        _controls.Player.Move.performed += context => _moveDirection = context.ReadValue<Vector2>();
        _controls.Player.Move.canceled += context => _moveDirection = Vector2.zero;
    }
    private void OnDisable()
    {
        _controls.Player.Move.performed -= context => _moveDirection = context.ReadValue<Vector2>();
        _controls.Player.Move.canceled -= context => _moveDirection = Vector2.zero;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(_moveDirection.x, 0, _moveDirection.y);

        direction.Normalize();

        direction = transform.TransformDirection(direction);

        bool isGrounded = _characterController.isGrounded;

        if (isGrounded && _controls.Player.Jump.triggered)
        {
            _velocity.y = _jumpForce;
        }

        _velocity.y -= _gravity * Time.deltaTime;

        direction.y = _velocity.y;

        if (!_isAccelerate)
        {
            _characterController.Move(direction * _moveSpeed * Time.deltaTime);
        }
        else
        {
            _characterController.Move(direction * _moveSpeed * _accelerate * Time.deltaTime);
        }
        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0;
        }
    }

}
