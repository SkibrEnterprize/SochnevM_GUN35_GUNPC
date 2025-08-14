using UnityEngine;

public class CollectState : IState
{
    private readonly CharacterAI _ai;
    private float _collectTime = 2f;
    private float _timer;


    public CollectState(CharacterAI ai) => _ai = ai;

    public void Enter()
    {
        if (_ai.TargetItem == null)
        {
            _ai.ChangeState(_ai.Search);
            return;
        }

        _ai.NavAgent.isStopped = false;
        _ai.NavAgent.SetDestination(_ai.TargetItem.transform.position);
        _timer = 0f;
    }

    public void Update()
    {
        if (_ai.TargetItem == null)
        {
            _ai.ChangeState(_ai.Search);
            return;
        }

        float dist = Vector3.Distance(_ai.transform.position, _ai.TargetItem.transform.position);
        if (dist <= 1.5f)
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
        _ai.TargetItem = null;
    }

    private void Collect()
    {
        
        Object.Destroy(_ai.TargetItem);        
        _ai.ChangeState(_ai.Idle);
        _ai.Animator.SetTrigger("toIdle");
    }
}