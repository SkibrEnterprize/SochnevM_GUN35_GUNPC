using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    [SerializeField] private Prefab _prefab;
    [SerializeField] private int _poolSize = 20;
    [SerializeField] private Transform _parent;
    public override void InstallBindings()
    {
        Container.Bind<ObjectPool<Prefab>>()
                 .FromMethod(ctx => new ObjectPool<Prefab>(_prefab,
                                                          _poolSize,
                                                          _parent.transform))
                 .AsSingle()
                 .NonLazy();
    }
}
