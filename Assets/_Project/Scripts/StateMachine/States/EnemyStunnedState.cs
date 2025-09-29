using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyStunnedState : BaseState
{
    private readonly EnemyFSM _stateMachine;
    public EnemyStunnedState(EnemyFSM stateMachine) : base("Stunned", stateMachine)
    {
        _stateMachine = stateMachine;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        _stateMachine.AnimationStateController.SetAnimation(CharacterAnimation.Stunned);
    }


    public override void OnExit()
    {
        base.OnEnter();       
    }

    public override void UpdateLogic()
    {
        if (_stateMachine.AnimationStateController.IsAnimationDone) _stateMachine.ChangeState(_stateMachine.EnemyIdleState);
    }
    
}
