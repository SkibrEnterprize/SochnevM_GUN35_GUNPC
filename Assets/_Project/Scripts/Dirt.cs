using UnityEngine;

public class Dirt : MonoBehaviour
{
    private ObjectPool<Dirt> _poolDirt;

    public void SetPool(ObjectPool<Dirt> pool)
    {
        _poolDirt = pool;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<CleanerMover>(out CleanerMover mover)) _poolDirt.Return(this);
    }
}
