using System.Collections;
using UnityEngine;
using Zenject;

public class EndOfTurnIndicator : MonoBehaviour
{
    [SerializeField] float _delay = 3;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(
        SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(EndOfTurn());
    }

    IEnumerator EndOfTurn()
    {
        yield return new WaitForSeconds(_delay);
        _signalBus.Fire<EndAction>();
    }
}
