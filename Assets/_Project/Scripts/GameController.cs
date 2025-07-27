using System;
using UnityEngine;
using UnityEngine.Scripting;
using Zenject;

public class GameController : MonoBehaviour
{
    [SerializeField] int _scoreOfHit;
    [SerializeField] int _bonusStrike;
    [SerializeField] int _bonusSpare;
    private Controls _controls;
    private SignalBus _signalBus;
    private int _totalScore;

    [Inject]
    private void Cunstruct(Controls controls, SignalBus signalBus)
    {
        _controls = controls;
        _signalBus = signalBus;
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<InAction>(DisableInput);
        _signalBus.Subscribe<Hit>(CalculateScore);
        _signalBus.Subscribe<EndAction>(EnableInput);
    }


    private void OnDisable()
    {
        _signalBus.Unsubscribe<InAction>(DisableInput);
        _signalBus.Unsubscribe<Hit>(CalculateScore);
        _signalBus.Unsubscribe<EndAction>(EnableInput);
    }

  
    private void CalculateScore()
    {
        _totalScore += _scoreOfHit;
        print($"TotalScore = {_totalScore}");
    }

    private void DisableInput()
    {
        _controls.Disable();
    }
    private void EnableInput()
    {
        _controls.Enable();
    }

}