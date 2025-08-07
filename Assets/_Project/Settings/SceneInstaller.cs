using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private TextView _textView;
    [SerializeField] private Dirt _dirtPrefab;
    [SerializeField] private int _poolSize = 20;
    [SerializeField] private Transform _parent;
    private Controls _controls;
    public override void InstallBindings()
    {
        _controls = new Controls();
        _controls.Enable();
        Container.Bind<Controls>()
            .FromInstance(_controls)
            .AsSingle()
            .NonLazy();
        Container.Bind<GameController>()
            .FromInstance(_gameController)
            .AsSingle()
            .NonLazy();
        Container.Bind<TextView>()
            .FromInstance(_textView)
            .AsSingle()
            .NonLazy();
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<LidarDetected>();
        Container.DeclareSignal<DirtCollected>();
        Container.Bind<ObjectPool<Dirt>>()
                 .FromMethod(ctx => new ObjectPool<Dirt>(_dirtPrefab,
                                                          _poolSize,
                                                          _parent.transform))
                 .AsSingle();
    }
}
