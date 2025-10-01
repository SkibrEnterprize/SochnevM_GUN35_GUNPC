using UnityEngine;
using Zenject;

[RequireComponent(typeof(EnemyMovementController))]
[RequireComponent(typeof(AnimationStateController))]

public class EnemyFSM : StateMachine
{
    public readonly float hitDistance = 0.9f;
    public readonly float detectionDistance = 6f;
    //public readonly Vector2 hitDelay = new Vector2(1,2);

    [HideInInspector] public EnemyIdleState EnemyIdleState;
    [HideInInspector] public EnemyMovingState EnemyMovingState;
    //[HideInInspector] public EnemyHitState EnemyHitState;
    [HideInInspector] public EnemyAttackState EnemyAttackState;
    [HideInInspector] public EnemyStunnedState EnemyStunnedState;

    public EnemyMovementController MovementController { get; private set; }
    public Transform Target;
    public bool IsHitted;

    private void Awake()
    {
        MovementController = GetComponent<EnemyMovementController>();
        AnimationStateController = GetComponent<AnimationStateController>();

        EnemyIdleState = new EnemyIdleState(this);
        EnemyMovingState = new EnemyMovingState(this);
        EnemyAttackState = new EnemyAttackState(this);
        EnemyStunnedState = new EnemyStunnedState(this);
    }

    protected override BaseState GetInitialState()
    {
        return EnemyIdleState;
    }

}
