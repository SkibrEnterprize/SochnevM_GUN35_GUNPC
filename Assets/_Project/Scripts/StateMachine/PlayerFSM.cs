using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMovementController))]
[RequireComponent(typeof(AnimationStateController))]
[RequireComponent(typeof(Animator))]
public class PlayerFSM : StateMachine
{
    public readonly float hitDistance = 0.9f;

    [HideInInspector] public PlayerIdleState PlayerIdleState;
    [HideInInspector] public PlayerMovingState PlayerMovingState;
    [HideInInspector] public PlayerHitState PlayerHitState;
    [HideInInspector] public PlayerStunnedState PlayerStunnedState;
    [HideInInspector] public Controls InputController;     // ссылка на компонент PlayerInput

    public PlayerMovementController MovementController { get; private set; }
    [Inject]
    public void Construct(Controls playerInput)
    {
        InputController = playerInput;
    }

    private void Awake()
    {
        MovementController = GetComponent<PlayerMovementController>();
        AnimationStateController = GetComponent<AnimationStateController>();

        PlayerIdleState = new PlayerIdleState(this);
        PlayerMovingState = new PlayerMovingState(this);
        PlayerHitState = new PlayerHitState(this);
        PlayerStunnedState = new PlayerStunnedState(this);
    }

    protected override BaseState GetInitialState()
    {
        return PlayerIdleState;
    }
}
