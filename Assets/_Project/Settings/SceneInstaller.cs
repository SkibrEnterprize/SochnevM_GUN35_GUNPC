using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Skittle _skittlePrefab;
    [SerializeField] private SkittleConfig _skittleConfig;
    [SerializeField] private GameController _gameController;
    [SerializeField] private UiController _uiController;
    [SerializeField] private ObjectPoolOfBall _ballPool;
    private Controls _controls;
    public override void InstallBindings()
    {

        _controls = new Controls();
        _controls.Enable();
        Container.Bind<Controls>().FromInstance(_controls).AsSingle().NonLazy();
        Container.Bind<GameController>().FromInstance(_gameController).AsSingle().NonLazy();
        Container.Bind<UiController>().FromInstance(_uiController).AsSingle().NonLazy();
        Container.Bind<ObjectPoolOfBall>().FromInstance(_ballPool).AsSingle().NonLazy();
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<Hit>();
        Container.DeclareSignal<Strike>();
        Container.DeclareSignal<Spare>();
        Container.DeclareSignal<InAction>();
        Container.DeclareSignal<EndAction>();

        Container.Bind<SkittleFactory>()
            .AsSingle()
            .WithArguments(_skittlePrefab)
            .NonLazy();

        Container.BindInterfacesAndSelfTo<SkittleSpawn>()
            .AsSingle()
            .WithArguments(_skittleConfig)
            .NonLazy();
    }
}
