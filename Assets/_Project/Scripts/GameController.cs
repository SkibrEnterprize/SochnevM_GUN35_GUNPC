using TMPro;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [SerializeField] private SkittleSet _skittleSet;
    [SerializeField] private int _scoreOfHit = 1;
    [SerializeField] private int _bonusStrike = 30;
    [SerializeField] private int _bonusSpare = 20;
    [SerializeField] TextMeshProUGUI _totalScoreTMP;
    [SerializeField] TextMeshProUGUI _totalThrowsTMP;

    private Controls _controls;
    private SignalBus _signalBus;
    private int _totalScore = 0;
    private int _throwNumber = 0;
    private int _skittlesTotal;


    [Inject]
    private void Cunstruct(Controls controls, SignalBus signalBus)
    {
        _controls = controls;
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _skittlesTotal = _skittleSet.SkittleCount;
    }
    private void OnEnable()
    {
        _signalBus.Subscribe<InAction>(StartOfTurn);
        _signalBus.Subscribe<Hit>(CalculateScore);
        _signalBus.Subscribe<EndAction>(EndOfTurn);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<InAction>(StartOfTurn);
        _signalBus.Unsubscribe<Hit>(CalculateScore);
        _signalBus.Unsubscribe<EndAction>(EndOfTurn);
    }
    private void CalculateScore()=>_totalScore += _scoreOfHit;        
    private void CalculateBonus()
    {
        if (_throwNumber == 1 && _totalScore == _skittlesTotal) _totalScore += _bonusStrike;
        if (_throwNumber == 2 && _totalScore == _skittlesTotal) _totalScore += _bonusSpare;
    }
    private void StartOfTurn()
    {
        _controls.Disable();
        _throwNumber++;
    }
    private void EndOfTurn()
    {
        _controls.Enable();
        CalculateBonus();
        UpdateScoreDisplay();
    }
    private void UpdateScoreDisplay()
    {
        _totalScoreTMP.text = _totalScore.ToString();
        _totalThrowsTMP.text = _throwNumber.ToString();
    }        

}