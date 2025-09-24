using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : BaseState
{
    private readonly PlayerFSM _stateMachine;
    public PlayerIdleState(PlayerFSM stateMachine) : base("Moving", stateMachine)
    {
        _stateMachine = stateMachine;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        _stateMachine.InputController.Player.LightHit.performed += OnHit;
        _stateMachine.InputController.Player.HeavyHit.performed += OnHit;

        _stateMachine.AnimationStateController.SetAnimation(CharacterAnimation.Walk);
    }


    public override void OnExit()
    {
        base.OnEnter();
        _stateMachine.InputController.Player.LightHit.performed -= OnHit;
        _stateMachine.InputController.Player.HeavyHit.performed -= OnHit;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _stateMachine.MovementController.UpdateMovement();

        if(_stateMachine.MovementController.MovementInput == Vector2.zero)
            _stateMachine.ChangeState(_stateMachine.PlayerIdleState);
    }
    private void OnHit(InputAction.CallbackContext context)
    {
        _stateMachine.ChangeState(_stateMachine.PlayerHitState);
    }
}
