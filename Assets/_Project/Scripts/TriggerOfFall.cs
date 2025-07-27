using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TriggerOfFall : MonoBehaviour
{
    private SignalBus _signalBus;
    private bool _isFalling = false;

    [Inject]
    private void Construct(
        SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_isFalling)
        {
            _signalBus.Fire<Hit>();
            _isFalling = true;
        }
    }
}
