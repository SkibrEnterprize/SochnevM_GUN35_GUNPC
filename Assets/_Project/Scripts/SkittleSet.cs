using UnityEngine;
using Zenject;

public class SkittleSet : MonoBehaviour
{
    private Skittle[] _skittles;
    private SignalBus _signalBus;
    public int SkittleCount => _skittles.Length;

    private void Awake()
    {
        _skittles = GetComponentsInChildren<Skittle>();
    }

    [Inject]
    private void Construct(
        SignalBus signalBus)
    {
        _signalBus = signalBus;
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
        _skittles = GetComponentsInChildren<Skittle>();
        foreach (Skittle skittle in _skittles)
        {
            Rigidbody rb = skittle.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
        }
    }
}
