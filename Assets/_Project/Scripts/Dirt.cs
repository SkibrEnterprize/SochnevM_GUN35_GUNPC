using UnityEngine;
using Zenject;

public class Dirt : MonoBehaviour
{
    private ObjectPool<Dirt> _poolDirt;
    private SignalBus _signalBus;    

    public void SetPool(ObjectPool<Dirt> pool, SignalBus signalBus)
    {
        _poolDirt = pool;
        _signalBus = signalBus;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<CharacterAI>(out CharacterAI characterAI)
            && characterAI.CurrentState == characterAI.Collect)
        {
            _poolDirt.Return(this);
            if (_signalBus!=null) _signalBus.Fire<DirtCollected>();
        }
    }
}
