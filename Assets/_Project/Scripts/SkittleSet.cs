using UnityEngine;
using Zenject;

public class SkittleSet : MonoBehaviour
{
    private Skittle[] _skittles;
    private int _skittlesStopped = 0;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(
        SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    private void Awake()
    {
        _skittles = GetComponentsInChildren<Skittle>();
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<EndAction>(FreezeObjects);
    }


    private void OnDisable()
    {
        _signalBus.Unsubscribe<EndAction>(FreezeObjects);
    }

    private void FreezeObjects()
    {
        foreach (Skittle skittle in _skittles)
        {
            Rigidbody rb = skittle.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
        }
    }
}
