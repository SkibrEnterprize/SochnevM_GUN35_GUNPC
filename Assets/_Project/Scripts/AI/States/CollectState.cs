using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : IState
{
    private readonly CharacterAI _ai;
    private float _collectTime = 2f; // сколько времени занимает сбор
    private float _timer;

    public CollectState(CharacterAI ai) => _ai = ai;

    public void Enter()
    {
        if (_ai.TargetItem == null)
        {
            _ai.ChangeState(_ai.Search); // если предмет исчез – возвращаемся к поиску
            return;
        }

        _ai.NavAgent.isStopped = false;
        _ai.NavAgent.SetDestination(_ai.TargetItem.transform.position);
        _timer = 0f;
        _ai.Animator.SetTrigger("toCollect");
    }

    public void Update()
    {
        if (_ai.TargetItem == null)
        {
            _ai.ChangeState(_ai.Search);
            return;
        }

        // Если уже рядом, начинаем сбор
        float dist = Vector3.Distance(_ai.transform.position, _ai.TargetItem.transform.position);
        if (dist <= 1.5f)   // чуть больше, чтобы не зависать в NavMeshAgent
        {
            _timer += Time.deltaTime;
            if (_timer >= _collectTime)
            {
                Collect();
            }
        }
    }

    public void Exit()
    {
        _ai.TargetItem = null; // сбросить ссылку
    }

    private void Collect()
    {
        // «Собираем» предмет – убираем его из сцены или делаем что‑то ещё
        
        Object.Destroy(_ai.TargetItem);        
        _ai.ChangeState(_ai.Idle);   // возвращаемся в Idle после сбора
    }
}