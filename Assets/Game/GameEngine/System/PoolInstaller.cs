using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PoolInstaller : MonoInstaller
{    
    [SerializeField]
    private PoolSettings[] _pools;
    public override void InstallBindings()
    {

        foreach (var pool in _pools)
        {

            var poolContainer = new GameObject($"{pool.prefab.name}_Pool");
            poolContainer.transform.SetParent(pool.parent, false);

            Container.Bind<ObjectPool<Prefab>>()
                     .FromMethod(ctx => new ObjectPool<Prefab>(pool.prefab,
                                                              pool.poolSize,
                                                              poolContainer.transform))
                     .AsSingle()                                     
                     .NonLazy();
        }

    }
}
