using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerMovementController : MonoBehaviour
{    
    [HideInInspector] public Controls PlayerInput;
    [SerializeField] private float _moveSpeed = 5f;
    public Vector2 MovementInput { get; private set; } = Vector2.zero;
   
    private InputAction _moveAction;

    [Inject]
    public void Construct(Controls playerInput)
    {
        PlayerInput = playerInput;
    }

    private void Awake()
    {              
            _moveAction = PlayerInput.FindAction("Move");
    }


    private void OnEnable()
    {
        if (_moveAction != null) _moveAction.Enable();
    }
    private void OnDisable()
    {
        if (_moveAction != null) _moveAction.Disable();
    }

    public void UpdateMovement()
    {
        MovementInput = _moveAction?.ReadValue<Vector2>() ?? Vector2.zero;

        if (MovementInput == Vector2.zero) return;

        Vector3 movementDelta = new Vector3(MovementInput.x, MovementInput.y, 0f)
                                   * _moveSpeed
                                   * Time.deltaTime;
       
            transform.Translate(movementDelta, Space.World);
    }
}
