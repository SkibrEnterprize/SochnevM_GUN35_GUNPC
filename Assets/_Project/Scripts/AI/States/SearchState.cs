using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchState : IState
{
    private readonly CharacterAI _ai;
    private Vector3 _targetPos;
    private float _reachedDist = 1f; // как близко к цели считаем

    public SearchState(CharacterAI ai) => _ai = ai;

    public void Enter()
    {
        SetRandomDestination();
        _ai.NavAgent.isStopped = false;
        _ai.Animator.SetTrigger("toSearch");
    }

    public void Update()
    {
        // ≈сли дошли до цели Ц ищем новую
        if (!(_ai.NavAgent.pathPending || _ai.NavAgent.remainingDistance > _reachedDist))
            SetRandomDestination();

        // ѕровер€ем, видим ли предмет
        Collider[] hits = Physics.OverlapSphere(_ai.transform.position, 20f,
                                                LayerMask.GetMask("Dirt"));
        foreach (var hit in hits)
        {
            _ai.TargetItem = hit.gameObject;
            _ai.ChangeState(_ai.Collect);
            break;
        }
    }

    public void Exit() { /* можно очистить цель */ }

    private void SetRandomDestination()
    {
        Vector3 randomDir = Random.insideUnitSphere * 10f; // диапазон поиска
        randomDir += _ai.transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, 10f, NavMesh.AllAreas))
            _targetPos = hit.position;

        _ai.NavAgent.SetDestination(_targetPos);
    }
}
