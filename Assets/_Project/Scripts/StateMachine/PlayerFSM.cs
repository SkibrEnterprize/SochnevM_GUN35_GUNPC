using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerFSM : StateMachine
{
    public readonly float hitDistance = 0.9f;

    [HideInInspector] public PlayerIdleState PlayerIdleState;
    [HideInInspector] public PlayerIdleState PlayerMovingState;
    [HideInInspector] public PlayerHitState PlayerHitState;
    [HideInInspector] public PlayerStunnedState PlayerStunnedState;
    [HideInInspector] public Controls InputController;
    public PlayerMovementController MovementController { get; private set; }

    [Inject]
    public void Construct(Controls inputController)
    {
       InputController = inputController;
    }
}
