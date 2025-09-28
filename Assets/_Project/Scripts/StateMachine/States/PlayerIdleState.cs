using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : BaseState
{
    private readonly PlayerFSM _stateMachine;
    public PlayerIdleState(PlayerFSM stateMachine) : base("Idle", stateMachine)
    {
        _stateMachine = stateMachine;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        _stateMachine.InputController.Player.Move.performed += OnMove;
        _stateMachine.InputController.Player.LightHit.performed += OnHit;
        //_stateMachine.InputController.Player.Jump.performed += OnJump;

        _stateMachine.AnimationStateController.SetAnimation(CharacterAnimation.Idle);
    }


    public override void OnExit()
    {
        base.OnExit();
        _stateMachine.InputController.Player.Move.performed -= OnMove;
        _stateMachine.InputController.Player.LightHit.performed -= OnHit;
        //_stateMachine.InputController.Player.Jump.performed -= OnJump;
    }


    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _stateMachine.MovementController.UpdateMovement();

        if(_stateMachine.MovementController.MovementInput == Vector2.zero)
            _stateMachine.ChangeState(_stateMachine.PlayerIdleState);
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        _stateMachine.ChangeState(_stateMachine.PlayerMovingState);
    }
    private void OnHit(InputAction.CallbackContext context)
    {
        _stateMachine.ChangeState(_stateMachine.PlayerHitState);
    }
    private void OnJump(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }
}
