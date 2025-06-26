using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private Controls _controls;
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
        
        SignalBus signalBus)
    {
        _controls = controls;
        _signalBus = signalBus;
    }
    private void OnEnable()
    {
        _signalBus.Subscribe<PlayersMoveDone>(TransferOfTurn);        
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<PlayersMoveDone>(TransferOfTurn);
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
