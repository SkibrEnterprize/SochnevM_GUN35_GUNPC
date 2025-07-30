using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameController : MonoBehaviour
{
    [SerializeField] private int _scoreOfHit = 1;
    [SerializeField] private int _bonusStrike = 30;
    [SerializeField] private int _bonusSpare = 20;
    [SerializeField] private int _attempts = 3;
    [SerializeField] TextMeshProUGUI _totalScoreTMP;
    [SerializeField] TextMeshProUGUI _totalThrowsTMP;
    [SerializeField] TextMeshProUGUI _totalAttemptsTMP;
    [SerializeField] TextMeshProUGUI _totalSkittleTMP;

    private Controls _controls;
    private SignalBus _signalBus;
    private SkittleSpawn _spawn;
    private int _totalScore = 0;
    private int _throwNumber = 0;



    [Inject]
    private void Cunstruct(Controls controls, SignalBus signalBus, SkittleSpawn skittleSpawn)
    {
        _controls = controls;
        _signalBus = signalBus;
        _spawn = skittleSpawn;
    }
    private void Start()
    {
        _totalAttemptsTMP.text = _attempts.ToString();
        _totalSkittleTMP.text = _spawn.SkittleTotal.ToString();
    }
    void OnEnable()
    {
        _signalBus.Subscribe<InAction>(StartOfTurn);
        _signalBus.Subscribe<Hit>(CalculateScore);
        _signalBus.Subscribe<EndAction>(EndOfTurn);
    }

    void OnDisable()
    {
        _signalBus.Unsubscribe<InAction>(StartOfTurn);
        _signalBus.Unsubscribe<Hit>(CalculateScore);
        _signalBus.Unsubscribe<EndAction>(EndOfTurn);
    }
    private void CalculateScore()
    {
        UpdateScoreDisplay();
        _totalScore += _scoreOfHit;
    }
    private void CalculateBonus()
    {
        if (_throwNumber == 1 && _totalScore == _spawn.SkittleTotal) _totalScore += _bonusStrike;
        if (_throwNumber == 2 && _totalScore == _spawn.SkittleTotal) _totalScore += _bonusSpare;
    }
    private void StartOfTurn()
    {
        if (_throwNumber != _attempts)
        {
            _controls.Disable();
            _throwNumber++;
            UpdateScoreDisplay();
        }
        else
        {
            print($"!!!The number of throw attempts has ended!!!");
            print($"Your score - {_totalScore}");
            ReloadScene();
        }

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

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

}