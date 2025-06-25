using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private Controls _controls;
    //private IGameEvent _onPlayersMoveEvent;
    //private IGameEvent _onPlayersMoveDoneEvent;
    private SignalBus _signalBus;

    [SerializeField] private Team _currentPlayingTeam;
    public Team CurrentPlayingTeam => _currentPlayingTeam;


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

    private void OnEnable()
    {
        //_signalBus.Subscribe<PlayersMoveSignal>(DisableInput);
        //_signalBus.Subscribe<PlayersMoveDoneSignal>(EnableInput);
        //_onPlayersMoveEvent.OnEventTriggered += PlayersMove;
        //_onPlayersMoveDoneEvent.OnEventTriggered += PlayersMoveDone;
    }

    private void OnDisable()
    {
        //_signalBus.Unsubscribe<PlayersMoveSignal>(DisableInput);
        //_signalBus.Unsubscribe<PlayersMoveDoneSignal>(EnableInput);
        //_onPlayersMoveEvent.OnEventTriggered -= PlayersMove;
        //_onPlayersMoveDoneEvent.OnEventTriggered -= PlayersMoveDone;
    }
    [ContextMenu("ChangeTeam")]
    private void TransferOfTurn()
    {
        _currentPlayingTeam = (Team)(int)_currentPlayingTeam + 1 % 2;
        Debug.Log(_currentPlayingTeam.ToString());
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
