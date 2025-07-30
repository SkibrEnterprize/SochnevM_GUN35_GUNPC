using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(GateMover))]
public class GateController : MonoBehaviour
{
    [SerializeField] float _delay = 3;
    private GateMover _gateMover;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(
        SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    private void Awake()
    {
        _gateMover = GetComponent<GateMover>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Ball>(out Ball ball)) StartCoroutine(EndOfTurn());
        if (other.gameObject.TryGetComponent<Skittle>(out Skittle skittle) && skittle.GetStatusOfFall() == true && _gateMover.enabled)
        {
            _signalBus.Fire<Hit>();
            Destroy(other.gameObject);
        }
    }
    IEnumerator EndOfTurn()
    {
        yield return new WaitForSeconds(_delay);
        _gateMover.SetActive(true);
    }
}
