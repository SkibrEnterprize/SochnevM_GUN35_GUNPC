using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class CharacterAI : MonoBehaviour
{
    // Ссылки на компоненты
    [Header("References")]
    public NavMeshAgent NavAgent;      // можно задать через инспектор, либо взять в Awake()
    public Animator Animator;

    // Объект для сбора (можно сделать публичным, но обычно не публикуем)
    internal GameObject TargetItem;

    // Состояния
    internal IState Idle;
    internal IState Search;
    internal IState Collect;

    private IState _currentState;

    void Awake()
    {
        if (!NavAgent) NavAgent = GetComponent<NavMeshAgent>();
        if (!Animator) Animator = GetComponent<Animator>();

        // Инициализируем состояния
        Idle = new IdleState(this);
        Search = new SearchState(this);
        Collect = new CollectState(this);

        ChangeState(Idle);   // стартуем с Idle
    }

    void Update()
    {
        _currentState?.Update();
    }

    /// <summary>
    /// Переход к другому состоянию
    /// </summary>
    public void ChangeState(IState newState)
    {
        if (_currentState == newState) return;

        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    private void OnDrawGizmos()
    {        
        Gizmos.color = Color.green;
        
        Gizmos.DrawWireSphere(transform.position, 20f);

    }
}
