using UnityEngine;

[System.Serializable]
public class PoolSettings
{
    [Space]
    public Prefab prefab;
    public int poolSize = 20;
    public Transform parent;
}
