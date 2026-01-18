using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    [SerializeField] private Prefab _prefab;
    [SerializeField] private int _poolSize = 20;
    [SerializeField] private Transform _parent;
    public override void InstallBindings()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
        var poolContainer = new GameObject($"{_prefab.name}_Pool");
        poolContainer.transform.SetParent(_parent, false);

        Container.Bind<ObjectPool<Prefab>>()
                 .FromMethod(ctx => new ObjectPool<Prefab>(_prefab,
                                                          _poolSize,
                                                          poolContainer.transform))
                 .AsSingle()
                 .NonLazy();
    }

    //private void OnDestroy()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}
    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    var poolContainer = new GameObject($"{_prefab.name}_Pool");
    //    poolContainer.transform.SetParent(_parent, false);

    //    Container.Bind<ObjectPool<Prefab>>()
    //             .FromMethod(ctx => new ObjectPool<Prefab>(_prefab,
    //                                                      _poolSize,
    //                                                      poolContainer.transform))
    //             .AsSingle()
    //             .NonLazy();
    //}
}
