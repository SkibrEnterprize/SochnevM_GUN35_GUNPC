using Entities;
using SampleProject;
using SampleProject.ResourceObject;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class RTSInputController : MonoBehaviour
{
    [Header("Настройки")]
    public LayerMask _groundLayer;
    public LayerMask _unitLayerMask;
    public float maxRayDistance = 100f;

    private PlayerInputActions _inputActions;
    private GroupeMoverManager _groupeMoverManager;

    private Rect _selectionRect;
    private Vector2 _dragStartPos;
    private bool _isDragging;

    private Vector3 _startWorld;    // мировая точка начала drag
    private Vector3 _endWorld;      // мировая точка конца drag (отпускание)
    private Bounds _selectionBounds; // вычисленный куб

    // Выбранные юниты с двумя компонентами для удобства
    private readonly List<(CharacterEntity character, CommandController command)> _selectedUnits =
        new List<(CharacterEntity, CommandController)>();

    void Awake()
    {
        _inputActions = new PlayerInputActions();

        _inputActions.Gameplay.LeftMouseClick.performed += ctx => OnLeftMouseDown();
        _inputActions.Gameplay.LeftMouseClick.canceled += ctx => OnLeftMouseUp();
        _inputActions.Gameplay.RightMouseClick.performed += ctx => OnRightMouseClick();

        _inputActions.Gameplay.Enable();
    }

    void Update()
    {
        if (_isDragging)
        {
            UpdateSelectionRect();
            CalculateSelectionBounds();
        }
    }

    private void OnLeftMouseDown()
    {
        _dragStartPos = Mouse.current.position.ReadValue();
        _isDragging = true;
    }

    private void OnLeftMouseUp()
    {
        if (!_isDragging)
            return;

        _isDragging = false;

        // Минимальный размер прямоугольника, чтобы отличать drag от клика
        if (_selectionRect.width < 5 || _selectionRect.height < 5)
            SingleClickSelect();
        else
            MultiSelectUnits();
    }

    private void UpdateSelectionRect()
    {
        Vector2 currentMousePos = Mouse.current.position.ReadValue();
        _selectionRect = new Rect
        (
            Mathf.Min(_dragStartPos.x, currentMousePos.x),
            Mathf.Min(_dragStartPos.y, currentMousePos.y),
            Mathf.Abs(_dragStartPos.x - currentMousePos.x),
            Mathf.Abs(_dragStartPos.y - currentMousePos.y)
        );
    }

    private void SingleClickSelect()
    {
        ClearSelectionIfNoCtrl();

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance))
        {
            var charEntity = hit.collider.gameObject.GetComponent<CharacterEntity>();
            var commandCtrl = hit.collider.gameObject.GetComponent<CommandController>();

            if (charEntity != null && commandCtrl != null)
            {
                AddUnitToSelection(charEntity, commandCtrl);
                return;
            }
        }
        // Если клик был вне юнита и Ctrl не зажат, очищаем выделение
        if (!Keyboard.current.ctrlKey.isPressed)
            ClearSelection();
    }

    private void MultiSelectUnits()
    {
        // если пользователь не держит Ctrl очищаем выборку
        ClearSelectionIfNoCtrl();

        CalculateSelectionBounds();
        // находим коллайдеры ----------
        Collider[] hits = Physics.OverlapBox(
        _selectionBounds.center,
        _selectionBounds.extents,
        Quaternion.identity,
        _unitLayerMask);

        foreach (var col in hits)
        {
            if (!col.TryGetComponent<CharacterEntity>(out var unit))
                continue;
            if (!unit.TryGetComponent<CommandController>(out var cmdCtrl))
                continue;

            AddUnitToSelection(unit, cmdCtrl);
        }
    }
    private void CalculateSelectionBounds()
    {
        _startWorld = RaycastToGround(_dragStartPos);
        _endWorld = RaycastToGround(Mouse.current.position.ReadValue());

        if (_startWorld == Vector3.zero || _endWorld == Vector3.zero)
            return; // луч не пересек плоскость

        Vector3 center = (_startWorld + _endWorld) / 2f;
        Vector3 extents = new Vector3(
            Mathf.Abs(_endWorld.x - _startWorld.x) * 0.5f,
            1.5f,                                 // высота куба
            Mathf.Abs(_endWorld.z - _startWorld.z) * 0.5f);

        _selectionBounds = new Bounds(center, extents * 2f);
    }

    // Вспомогательная функция: луч к плоскости Y=0

    private Vector3 RaycastToGround(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(screenPos.x, screenPos.y, 0f));
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // XZ?плоскость

        if (groundPlane.Raycast(ray, out float distance))
            return ray.GetPoint(distance);

        return Vector3.zero;   // если не пересек – возвращаем ноль
    }


    private void AddUnitToSelection(CharacterEntity character, CommandController cmd)
    {
        if (!_selectedUnits.Exists(t => t.character == character))
        {
            _selectedUnits.Add((character, cmd));
            HighlightUnit(character, true);
        }
    }

    private void ClearSelection()
    {
        foreach (var (character, _) in _selectedUnits)
            HighlightUnit(character, false);

        _selectedUnits.Clear();
    }

    private void ClearSelectionIfNoCtrl()
    {
        if (!Keyboard.current.ctrlKey.isPressed)
            ClearSelection();
    }

    private void HighlightUnit(CharacterEntity character, bool highlight)
    {
        // Можно реализовать включение/выключение визуальной подсветки
        character.GetComponent<Outline>().enabled = highlight;
    }

    private void OnRightMouseClick()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (!Physics.Raycast(ray, out RaycastHit hit, maxRayDistance))
            return;
        Debug.Log("HIT");

        // Проверяем объекты на взаимодействие
        GameObject target = hit.collider.gameObject;

        foreach (var character in _selectedUnits)
        {
            // Передаем в CommandController сами ссылки, для удобства в логике

            // Сбор ресурсов
            if (target.TryGetComponent<ResourceEntity>(out var resource))
            {
                Debug.Log("Resources!!");
                character.command.GatherResource(resource);
                continue;
            }

            // Атака врага
            if (target.TryGetComponent<CharacterEntity>(out var enemyCharacter) &&
                target.TryGetComponent<EnemyComponent>(out _))
            {
                character.command.AttackTarget(enemyCharacter);
                continue;
            }

            // Перемещение по NavMesh
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 0.5f, NavMesh.AllAreas))
            {
                character.command.MoveToPosition(navHit.position);
            }
        }
    }

    void OnDestroy()
    {
        _inputActions.Gameplay.LeftMouseClick.performed -= ctx => OnLeftMouseDown();
        _inputActions.Gameplay.LeftMouseClick.canceled -= ctx => OnLeftMouseUp();
        _inputActions.Gameplay.RightMouseClick.performed -= ctx => OnRightMouseClick();
        _inputActions.Gameplay.Disable();
    }


    // Визуализация (прямоугольника выделения)
    void OnGUI()
    {
        if (_isDragging)
        {
            var rect = GetScreenRect(_dragStartPos, Mouse.current.position.ReadValue());
            DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    #region Helpers для отрисовки прямоугольника выделения

    public static Rect GetScreenRect(Vector2 screenPosition1, Vector2 screenPosition2)
    {
        // Создает Rect с правильным положением и размером из двух точек
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        var topLeft = Vector2.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector2.Max(screenPosition1, screenPosition2);
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

    public static void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, Texture2D.whiteTexture);
        GUI.color = Color.white;
    }

    public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        // Левая
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        // Правая
        DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        // Верхняя
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        // Нижняя
        DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
    }
    #endregion

    private void OnDrawGizmos()
    {

        // Выбираем цвет, чтобы куб был виден
        Gizmos.color = new Color(0f, 1f, 0f);   // полупрозрачный зелёный

        // WireCube – только контур; если нужен заполненный, используйте DrawCube()
        Gizmos.DrawWireCube(_selectionBounds.center, _selectionBounds.size);

        // Попробуем найти коллайдеры
        Collider[] hits = Physics.OverlapBox(
            _selectionBounds.center,
            _selectionBounds.extents,
            Quaternion.identity,
            _unitLayerMask);
    }
}