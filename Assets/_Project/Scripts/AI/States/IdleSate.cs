using UnityEngine;

public class IdleState : IState
{
    private readonly CharacterAI _ai;
    private float _timer = 0f;

    public IdleState(CharacterAI ai) => _ai = ai;

    public void Enter()
    {
        _timer = 0f;
        _ai.NavAgent.isStopped = true;
    }

    public void Update()
    {
        _timer += Time.deltaTime;

        _ai.Animator.SetFloat("toSearch", _timer);
        if (_timer >= 5f)
            _ai.ChangeState(_ai.Search);
    }

    public void Exit() {}
}