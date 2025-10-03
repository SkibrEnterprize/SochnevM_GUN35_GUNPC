using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAttackState : BaseState
{
    private readonly EnemyFSM _stateMachine;
    private float _attackTimer;            
    private const float HitDuration = 0.6f;

    public EnemyAttackState(EnemyFSM stateMachine) : base("Attack", stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _stateMachine.AnimationStateController.SetAnimation(CharacterAnimation.Hit);
        Debug.Log("Hit");
        _attackTimer = HitDuration;         
    }

    public override void UpdateLogic()
    {
        if (_stateMachine.IsHitted) _stateMachine.ChangeState(_stateMachine.EnemyStunnedState);
        base.UpdateLogic();
      
        if (_attackTimer > 0f)
            _attackTimer -= Time.deltaTime;
       
        bool tooFar = Vector3.Distance(_stateMachine.transform.position,
                                      _stateMachine.Target.transform.position) >= _stateMachine.hitDistance;
        bool readyToExit = _attackTimer <= 0f;          

        if (tooFar || readyToExit)
            _stateMachine.ChangeState(_stateMachine.EnemyIdleState);
    }

    public override void OnExit()
    {
        base.OnExit();
        // Здесь можно сбросить состояние анимации, если нужно
    }
}
