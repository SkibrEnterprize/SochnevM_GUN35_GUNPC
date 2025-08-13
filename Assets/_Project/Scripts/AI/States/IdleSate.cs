using UnityEngine;

public class IdleState : IState
{
    private readonly CharacterAI _ai;
    private float _timer = 0f;

    public IdleState(CharacterAI ai) => _ai = ai;

    public void Enter()
    {
        _timer = 0f;
        _ai.NavAgent.isStopped = true; // персонаж не движется
        _ai.Animator.SetTrigger("toIdle"); // если нужен анимационный эффект
    }

    public void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= 5f)                 // Idle ? Search через 5 сек.
            _ai.ChangeState(_ai.Search);
    }

    public void Exit() { /* ничего не делаем */ }
}