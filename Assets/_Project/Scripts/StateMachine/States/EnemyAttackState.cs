using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAttackState : BaseState
{
    private readonly EnemyFSM _stateMachine;
    public EnemyAttackState(EnemyFSM stateMachine) : base("Attack", stateMachine)
    {
        _stateMachine = stateMachine;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        _stateMachine.AnimationStateController.SetAnimation(CharacterAnimation.Hit);
    }


    public override void OnExit()
    {
        base.OnExit();
    }


    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Vector3.Distance(_stateMachine.transform.position, _stateMachine.Target.transform.position) >= _stateMachine.hitDistance)
        {
            _stateMachine.ChangeState(_stateMachine.EnemyMovingState);
        }
    }   
}
