using UnityEngine;

public class Prefab : MonoBehaviour
{
    private ObjectPool<Prefab> _poolPrefab;

    public void SetPool(ObjectPool<Prefab> pool)
    {
        _poolPrefab = pool;
    }
}
