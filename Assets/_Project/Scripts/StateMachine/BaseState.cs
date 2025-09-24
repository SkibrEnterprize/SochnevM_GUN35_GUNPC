using UnityEngine;

public class BaseState
{
    public string Name;
    protected StateMachine StateMachine;

    public BaseState(string name, StateMachine stateMachine)
    {
        Name = name;
        StateMachine = stateMachine;
    }

    public virtual void OnEnter() { }
    public virtual void UpdateLogic() { }
    public virtual void OnExit() { }
}
