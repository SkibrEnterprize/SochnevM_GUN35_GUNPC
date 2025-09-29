using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHitState : BaseState
{
    private readonly PlayerFSM _stateMachine;
    public PlayerHitState(PlayerFSM stateMachine) : base("Hit", stateMachine)
    {
        _stateMachine = stateMachine;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        _stateMachine.PlayerHitController.PerformAttack();
        _stateMachine.AnimationStateController.SetAnimation(CharacterAnimation.Hit);
    }


    public override void OnExit()
    {        
        base.OnExit();
    }

    public override void UpdateLogic()
    {

        if (_stateMachine.AnimationStateController.IsAnimationDone) _stateMachine.ChangeState(_stateMachine.PlayerIdleState);
    }
}
