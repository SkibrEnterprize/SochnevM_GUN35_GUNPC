using System;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private Controls _controls;
    private IGameEvent _onPlayersMoveEvent;
    private IGameEvent1 _onPlayersMoveDoneEvent;
    
    [Inject]
    private void Construct(Controls controls, IGameEvent onPlayersMoveEvent, IGameEvent1 onPlayersMoveDoneEvent)
    {
        _controls = controls;
        _onPlayersMoveEvent = onPlayersMoveEvent;
        _onPlayersMoveDoneEvent = onPlayersMoveDoneEvent;

    }

    private void OnEnable()
    {
        _onPlayersMoveEvent.OnEventTriggered += PlayersMove;
        _onPlayersMoveDoneEvent.OnEventTriggered += PlayersMoveDone;
    }

    private void OnDisable()
    {
        _onPlayersMoveEvent.OnEventTriggered -= PlayersMove;
        _onPlayersMoveDoneEvent.OnEventTriggered -= PlayersMoveDone;
    }

    private void PlayersMove()
    {
        _controls.Disable();
        Debug.Log("Controls DISABLE");
    }
    private void PlayersMoveDone()
    {
        _controls.Enable();
        Debug.Log("Controls ENABLE");
    }
}
