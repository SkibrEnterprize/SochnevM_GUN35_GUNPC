using SampleProject;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CommandController))]
public sealed class RTSClickInput : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera _camera = null;

    [Header("Ground")]
    [SerializeField] private LayerMask _groundLayer = 0;

    // Ёкземпл€р автоматически сгенерированного класса Input Actions
    private PlayerInputActions _inputActions;
    private CommandController _commandCtrl;

    void Awake()
    {
        _commandCtrl = GetComponent<CommandController>();

        if (_camera == null) _camera = Camera.main;

        // —оздаЄм и подписываемс€ на событие клика
        _inputActions = new PlayerInputActions();
        var gameplayMap = _inputActions.Gameplay;
        gameplayMap.MouseClick.performed += OnMouseClicked;
    }

    void OnEnable() => _inputActions?.Gameplay.Enable();
    void OnDisable() => _inputActions?.Gameplay.Disable();

    // ------------------------------------------------------------------
    private void OnMouseClicked(InputAction.CallbackContext context)
    {
        print("click");
        // ѕолучаем позицию мыши из контекста (необ€зательно Ц можно использовать Input.mousePosition)
        Vector2 mousePos = Mouse.current.position.ReadValue();

        var ray = _camera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayer))
        {
            // ќтправл€ем команду перемещени€
            _commandCtrl.MoveToPosition(hit.transform);   // или hit.point
        print("comand tomove is sended");
            Debug.Log($"Ray hit point: {hit.point}");
            Debug.Log($"Ray hit transform: {hit.transform}");
        }
    }

    void OnDestroy()
    {
        // ќтключаем подписку, чтобы избежать утечек
        if (_inputActions != null)
        {
            _inputActions.Gameplay.MouseClick.performed -= OnMouseClicked;
            _inputActions.Dispose();
        }
    }
}