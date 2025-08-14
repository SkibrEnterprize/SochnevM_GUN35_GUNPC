using UnityEngine;
using UnityEngine.AI;

public class SearchState : IState
{
    private readonly CharacterAI _ai;
    private Vector3 _targetPos;
    private float _reachedDist = 1f;
    private float _searchArea = 20f;

    public SearchState(CharacterAI ai) => _ai = ai;

    public void Enter()
    {
        SetRandomDestination();
        _ai.NavAgent.isStopped = false;
    }

    public void Update()
    {
        if (!(_ai.NavAgent.pathPending || _ai.NavAgent.remainingDistance > _reachedDist))
            SetRandomDestination();

        Collider[] hits = Physics.OverlapSphere(_ai.transform.position, _searchArea,
                                                LayerMask.GetMask("Dirt"));
        _ai.Animator.SetInteger("toCollect", hits.Length);
        if (hits.Length >= 5)
        {
            foreach (var hit in hits)
            {
                _ai.TargetItem = hit.gameObject;
                _ai.ChangeState(_ai.Collect);
                break;
            }
        }
    }

    public void Exit() {}

    private void SetRandomDestination()
    {
        Vector3 randomDir = Random.insideUnitSphere * _searchArea;
        randomDir += _ai.transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, _searchArea, NavMesh.AllAreas))
            _targetPos = hit.position;

        _ai.NavAgent.SetDestination(_targetPos);
    }
}
