using UnityEngine;
using Zenject;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Controls _controls;
    //private IGameEvent _onPlayersMoveEvent;
    //private IGameEvent _onPlayersMoveDoneEvent;
    private SignalBus _signalBus;

    [SerializeField] private Team _currentPlayingTeam;
    [SerializeField] private CanvasRenderer _imageTurnWhite;
    [SerializeField] private CanvasRenderer _imageTurnBlack;
    public Team CurrentPlayingTeam => _currentPlayingTeam;

    private void Awake()
    {
        VisualizeCurrentPlayer();
    }
    [Inject]
    private void Construct(
        Controls controls,
        //[Inject(Id = "onPlayersMove")] IGameEvent onPlayersMoveEvent,
        //[Inject(Id = "onPlayersMoveDone")] IGameEvent onPlayersMoveDoneEvent,
        SignalBus signalBus)
    {
        _controls = controls;
        //_onPlayersMoveEvent = onPlayersMoveEvent;
        //_onPlayersMoveDoneEvent = onPlayersMoveDoneEvent;
        _signalBus = signalBus;
    }

    //void Awake()
    //{
    //    //_currentPlayingTeam = (Team)UnityEngine.Random.Range(0, 2); // Выбираем случайный элемент из Enum

    //    // Теперь randomPlayer содержит либо Team.Black, либо Team.White
    //}

    private void OnEnable()
    {
        _signalBus.Subscribe<PlayersMoveDone>(TransferOfTurn);
        //_signalBus.Subscribe<PlayersMoveSignal>(DisableInput);
        //_signalBus.Subscribe<PlayersMoveDoneSignal>(EnableInput);
        //_onPlayersMoveEvent.OnEventTriggered += PlayersMove;
        //_onPlayersMoveDoneEvent.OnEventTriggered += PlayersMoveDone;
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<PlayersMoveDone>(TransferOfTurn);
        //_signalBus.Unsubscribe<PlayersMoveSignal>(DisableInput);
        //_signalBus.Unsubscribe<PlayersMoveDoneSignal>(EnableInput);
        //_onPlayersMoveEvent.OnEventTriggered -= PlayersMove;
        //_onPlayersMoveDoneEvent.OnEventTriggered -= PlayersMoveDone;
    }
    [ContextMenu("ChangeTeam")]
    private void TransferOfTurn()
    {
        _currentPlayingTeam = (Team)(((int)_currentPlayingTeam + 1) % 2);
        VisualizeCurrentPlayer();
    }

    private void VisualizeCurrentPlayer()
    {
        if (_currentPlayingTeam == Team.White)
        {
            _imageTurnBlack.SetAlpha(0.2f);
            _imageTurnWhite.SetAlpha(1f);
        }
        else
        {
            _imageTurnBlack.SetAlpha(1f);
            _imageTurnWhite.SetAlpha(0.2f);
        }
    }
    private void DisableInput()
    {
        _controls.Disable();
        Debug.Log("Controls DISABLE");
    }
    private void EnableInput()
    {
        _controls.Enable();
        Debug.Log("Controls ENABLE");
    }
}
