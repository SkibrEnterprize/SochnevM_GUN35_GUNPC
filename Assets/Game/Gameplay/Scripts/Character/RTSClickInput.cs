using Entities;
using SampleProject;
using SampleProject.ResourceObject;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CommandController))]
public sealed class RTSClickInput : MonoBehaviour
{
    [Header("Настройки")]
    public LayerMask groundLayer;
    public float maxRayDistance = 100f;

    private CommandController _commandCtrl;   // ссылка на прокси

    void Awake()
    {
        _commandCtrl = GetComponent<CommandController>();
        if (_commandCtrl == null)
            Debug.LogError($"{name} не содержит CommandController");

        // --- Input System ----------------------------------------------------
        var inputActions = new PlayerInputActions();
        inputActions.Gameplay.LeftMouseClick.performed += OnLeftMouseClicked;
        inputActions.Gameplay.RigthMouseClick.performed += OnRightMouseClicked;
        inputActions.Gameplay.Enable();          // сразу включаем
    }

    private void OnRightMouseClicked(InputAction.CallbackContext context)
    {
        _commandCtrl.Stop();
    }

    private void OnLeftMouseClicked(InputAction.CallbackContext context)
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        // луч к любому слою который указан)
        if (!Physics.Raycast(ray, out var hit, maxRayDistance, groundLayer))
            return;   // ничего не попало

        // Проверяем попадание в NavMesh

        bool isEntityAreResource = hit.collider.gameObject.TryGetComponent(out ResourceEntity resources);
        if (isEntityAreResource)
        {
            _commandCtrl.GatherResource(resources);
            return;
        }

        //попадание в объект

        bool isEntityAreTarget = hit.collider.gameObject.TryGetComponent(out CharacterEntity target) &&
                                    hit.collider.gameObject.TryGetComponent(out EnemyComponent enemyComponent);

        if (isEntityAreTarget)
        {
            _commandCtrl.AttackTarget(target);
            return;
        }

        bool isOnNavMesh = NavMesh.SamplePosition(
                               hit.point,
                               out var navHit,
                               0.5f,                // 0?– точная позиция
                               NavMesh.AllAreas);

        if (isOnNavMesh)
        {
            _commandCtrl.MoveToPosition(hit.point);            
        }

    }


}
