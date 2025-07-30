using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private SkittleSpawn _skittleSpawn;
    private Controls _controls;
    public override void InstallBindings()
    {

        _controls = new Controls();
        _controls.Enable();
        Container.Bind<Controls>().FromInstance(_controls).AsSingle().NonLazy();
        Container.Bind<GameController>().FromInstance(_gameController).AsSingle().NonLazy();
        Container.Bind<SkittleSpawn>().FromInstance(_skittleSpawn).AsSingle().NonLazy();
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<Hit>();
        Container.DeclareSignal<Strike>();
        Container.DeclareSignal<Spare>();
        Container.DeclareSignal<InAction>();
        Container.DeclareSignal<EndAction>();
    }
}
