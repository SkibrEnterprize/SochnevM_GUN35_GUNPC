using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{    
    private SignalBus _signalBus;
    private UIController _uiController;
    private int _totalDirtCollect;

    [Inject]
    public void Construct(SignalBus signalBus, UIController uiController)
    {
        _signalBus = signalBus;
        _uiController = uiController;
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<DirtCollected>(AddScore);
    }

    private void AddScore()
    {
        _totalDirtCollect++;
        _uiController.UpdateTotalDirtCollect(_totalDirtCollect.ToString());
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<DirtCollected>(AddScore);
    }
}
