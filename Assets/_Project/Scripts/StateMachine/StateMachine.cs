using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public AnimationStateController AnimationStateController { get; private set; }
    public BaseState CurrentState { get; private set; }

    private void Start()
    {
        CurrentState = GetInitialState();
        if (CurrentState != null) CurrentState.OnEnter();
    }

    private void Update()
    {
        if (CurrentState != null) CurrentState.UpdateLogic();
    }

    public void ChangeState(BaseState newSatate)
    {
        CurrentState.OnExit();
        CurrentState = newSatate;
        CurrentState.OnEnter();
    }


    protected virtual BaseState GetInitialState()
    {
        return null;
    }
}
