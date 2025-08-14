using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class CharacterAI : MonoBehaviour
{
    public NavMeshAgent NavAgent; 
    public Animator Animator;

    internal GameObject TargetItem;

    internal IState Idle;
    internal IState Search;
    internal IState Collect;

    private IState _currentState;
    public IState CurrentState => _currentState;

    void Awake()
    {
        if (!NavAgent) NavAgent = GetComponent<NavMeshAgent>();
        if (!Animator) Animator = GetComponent<Animator>();

        Idle = new IdleState(this);
        Search = new SearchState(this);
        Collect = new CollectState(this);

        ChangeState(Idle);
    }

    void Update()
    {
        _currentState?.Update();
    }
       
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
