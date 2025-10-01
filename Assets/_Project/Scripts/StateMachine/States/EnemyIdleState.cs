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
        _stateMachine.IsHitted = false;
    }


    public override void UpdateLogic()
    {
        base.UpdateLogic();
        TryReciveHit();
        TryDetectPlayer();
    }


    private void TryReciveHit()
    {
        if (_stateMachine.IsHitted) _stateMachine.ChangeState(_stateMachine.EnemyStunnedState);
    }
    private void TryDetectPlayer()
    {
        foreach (var player in PlayerHolder.Instance.Players)
        {
            if (Vector3.Distance(player.transform.position, _stateMachine.transform.position) <= _stateMachine.detectionDistance)
            {
                _stateMachine.Target = player;
                _stateMachine.ChangeState(_stateMachine.EnemyMovingState);
            }
        }
    }
}
    

