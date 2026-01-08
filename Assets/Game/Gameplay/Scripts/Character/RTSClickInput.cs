using Entities;
using SampleProject;
using SampleProject.ResourceObject;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CommandController))]
public sealed class RTSClickInput : MonoBehaviour
{
    [Header("Настройки")]
    public LayerMask groundLayer;
    public float maxRayDistance = 100f;

    private CommandController _commandCtrl; // для одиночного выбора самого данного юнита
    private static List<CommandController> selectedUnits = new List<CommandController>();

    private PlayerInputActions inputActions;

    private Rect selectionRect;
    private Vector2 dragStartPos;
    private bool isDragging;

    void Awake()
    {
        _commandCtrl = GetComponent<CommandController>();
        if (_commandCtrl == null)
            Debug.LogError($"{name} не содержит CommandController");

        inputActions = new PlayerInputActions();

        inputActions.Gameplay.LeftMouseClick.performed += ctx => OnLeftMouseClicked();
        inputActions.Gameplay.RightMouseClick.performed += ctx => OnRightMouseClicked();
        inputActions.Gameplay.LeftMouseClick.canceled += ctx => OnLeftMouseReleased();
        inputActions.Gameplay.Enable();
    }

    void Update()
    {
        if (isDragging)
        {
            UpdateSelectionRect();
        }
    }

    private void OnLeftMouseClicked()
    {
        dragStartPos = Mouse.current.position.ReadValue();
        isDragging = true;
    }

    private void OnLeftMouseReleased()
    {
        if (isDragging)
        {
            isDragging = false;

            // Если прямоугольник выделения слишком маленький - скорее всего клик, а не drag selection
            if (selectionRect.width < 5 || selectionRect.height < 5)
            {
                SingleClickSelect();
            }
            else
            {
                MultiSelectUnits();
            }
        }
    }

    private void UpdateSelectionRect()
    {
        Vector2 currentMousePos = Mouse.current.position.ReadValue();
        selectionRect = new Rect
        (
            Mathf.Min(dragStartPos.x, currentMousePos.x),
            Mathf.Min(dragStartPos.y, currentMousePos.y),
            Mathf.Abs(dragStartPos.x - currentMousePos.x),
            Mathf.Abs(dragStartPos.y - currentMousePos.y)
        );
    }

    private void SingleClickSelect()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out var hit, maxRayDistance))
        {
            bool ctrlHeld = Keyboard.current.ctrlKey.isPressed;

            if (hit.collider.gameObject.TryGetComponent(out CommandController clickedUnit))
            {
                if (!ctrlHeld)
                    ClearSelection();

                if (!selectedUnits.Contains(clickedUnit))
                {
                    selectedUnits.Add(clickedUnit);
                    HighlightUnit(clickedUnit, true);
                }
                return;
            }
        }

        // Клик вне юнита — очистить выбор если Ctrl не зажат
        if (!Keyboard.current.ctrlKey.isPressed)
            ClearSelection();
    }

    private void MultiSelectUnits()
    {
        if (!Keyboard.current.ctrlKey.isPressed)
            ClearSelection();

        foreach (var unit in FindObjectsOfType<CommandController>())
        {
            Vector3 posViewport = Camera.main.WorldToScreenPoint(unit.transform.position);
            // Проверяем, попадает ли юнит в область выделения (по экранным координатам)
            if (selectionRect.Contains(posViewport, true))
            {
                if (!selectedUnits.Contains(unit))
                {
                    selectedUnits.Add(unit);
                    HighlightUnit(unit, true);
                }
            }
        }
    }

    private void ClearSelection()
    {
        foreach (var unit in selectedUnits)
        {
            HighlightUnit(unit, false);
        }
        selectedUnits.Clear();
    }

    private void HighlightUnit(CommandController unit, bool highlight)
    {
        // Тут можно включать/выключать выделение для визуального эффекта
        // Например: unit.SetSelectionHighlight(highlight);
    }

    private void OnRightMouseClicked()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        if (!Physics.Raycast(ray, out var hit, maxRayDistance))
            return;

        // При клике правой кнопкой по ресурсу — команда сбора
        if (hit.collider.gameObject.TryGetComponent(out ResourceEntity resource))
        {
            foreach (var unit in selectedUnits)
                unit.GatherResource(resource);

            return;
        }

        // При клике Правой мышкой по врагу — атака для всех юнитов
        if (hit.collider.gameObject.TryGetComponent(out CharacterEntity target) &&
            hit.collider.gameObject.TryGetComponent(out EnemyComponent enemyComp))
        {
            foreach (var unit in selectedUnits)
                unit.AttackTarget(target);

            return;
        }

        // Если клик по земле — перемещение всех выбранных юнитов
        bool isOnNavMesh = NavMesh.SamplePosition(
            hit.point,
            out var navHit,
            0.5f,
            NavMesh.AllAreas);

        if (isOnNavMesh)
        {
            foreach (var unit in selectedUnits)
                unit.MoveToPosition(navHit.position);
        }
    }

    private void OnDestroy()
    {
        inputActions.Gameplay.LeftMouseClick.performed -= ctx => OnLeftMouseClicked();
        inputActions.Gameplay.RightMouseClick.performed -= ctx => OnRightMouseClicked();
        inputActions.Gameplay.LeftMouseClick.canceled -= ctx => OnLeftMouseReleased();
        inputActions.Gameplay.Disable();
    }

    // Опционально можно добавить OnGUI для визуализации selectionRect (прямоугольника выделения)
    void OnGUI()
    {
        if (isDragging)
        {
            var rect = GetScreenRect(dragStartPos, Mouse.current.position.ReadValue());
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
}




//----------------Code for one unit--------------------------

//using Entities;
//using SampleProject;
//using SampleProject.ResourceObject;
//using UnityEngine;
//using UnityEngine.AI;
//using UnityEngine.InputSystem;

//[RequireComponent(typeof(CommandController))]
//public sealed class RTSClickInput : MonoBehaviour
//{
//    [Header("Настройки")]
//    public LayerMask groundLayer;
//    public float maxRayDistance = 100f;

//    private CommandController _commandCtrl;   // ссылка на прокси

//    void Awake()
//    {
//        _commandCtrl = GetComponent<CommandController>();
//        if (_commandCtrl == null)
//            Debug.LogError($"{name} не содержит CommandController");

//        // --- Input System ----------------------------------------------------
//        var inputActions = new PlayerInputActions();
//        inputActions.Gameplay.LeftMouseClick.performed += OnLeftMouseClicked;
//        inputActions.Gameplay.RigthMouseClick.performed += OnRightMouseClicked;
//        inputActions.Gameplay.Enable();          // сразу включаем
//    }

//    private void OnRightMouseClicked(InputAction.CallbackContext context)
//    {
//        _commandCtrl.Stop();
//    }

//    private void OnLeftMouseClicked(InputAction.CallbackContext context)
//    {
//        Vector2 screenPos = Mouse.current.position.ReadValue();
//        Ray ray = Camera.main.ScreenPointToRay(screenPos);

//        // луч к любому слою который указан)
//        if (!Physics.Raycast(ray, out var hit, maxRayDistance, groundLayer))
//            return;   // ничего не попало

//        // Проверяем попадание в NavMesh

//        bool isEntityAreResource = hit.collider.gameObject.TryGetComponent(out ResourceEntity resources);
//        if (isEntityAreResource)
//        {
//            _commandCtrl.GatherResource(resources);
//            return;
//        }

//        //попадание в объект

//        bool isEntityAreTarget = hit.collider.gameObject.TryGetComponent(out CharacterEntity target) &&
//                                    hit.collider.gameObject.TryGetComponent(out EnemyComponent enemyComponent);

//        if (isEntityAreTarget)
//        {
//            _commandCtrl.AttackTarget(target);
//            return;
//        }

//        bool isOnNavMesh = NavMesh.SamplePosition(
//                               hit.point,
//                               out var navHit,
//                               0.5f,                // 0?– точная позиция
//                               NavMesh.AllAreas);

//        if (isOnNavMesh)
//        {
//            _commandCtrl.MoveToPosition(hit.point);            
//        }

//    }


//}
