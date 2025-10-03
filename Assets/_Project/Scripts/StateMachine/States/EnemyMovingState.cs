using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyMovingState : BaseState
{
    private readonly EnemyFSM _stateMachine;
    public EnemyMovingState(EnemyFSM stateMachine) : base("Moving", stateMachine)
    {
        _stateMachine = stateMachine;
    }
    public override void OnEnter()
    {
        base.OnEnter();
       
        _stateMachine.AnimationStateController.SetAnimation(CharacterAnimation.Moving);
    }


    public override void OnExit()
    {
        base.OnExit();        
    }

    public override void UpdateLogic()
    {
       base.UpdateLogic();
        if (_stateMachine.IsHitted) _stateMachine.ChangeState(_stateMachine.EnemyStunnedState);
        _stateMachine.MovementController.UpdateMovement(_stateMachine.Target.transform);
        if (Vector3.Distance(_stateMachine.transform.position, _stateMachine.Target.transform.position) <= _stateMachine.hitDistance)
        {
            _stateMachine.ChangeState(_stateMachine.EnemyAttackState);
        }
    }
    private void OnHit(InputAction.CallbackContext context)
    {
       
    }
}
