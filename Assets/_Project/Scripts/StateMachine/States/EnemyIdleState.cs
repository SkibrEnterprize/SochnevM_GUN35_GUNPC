using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class EnemyIdleState : BaseState
{
    private readonly EnemyFSM _stateMachine;
    public EnemyIdleState(EnemyFSM stateMachine) : base("Idle", stateMachine)
    {
        _stateMachine = stateMachine;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        _stateMachine.AnimationStateController.SetAnimation(CharacterAnimation.Idle);
    }


    public override void OnExit()
    {
        base.OnExit();
    }


    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(_stateMachine.IsHitted) _stateMachine.ChangeState(_stateMachine.EnemyStunnedState);
        _stateMachine.IsHitted = false;
    }
}
    
