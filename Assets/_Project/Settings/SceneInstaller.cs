using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private UIController _uiContriller;
    private Controls _controls;
    public override void InstallBindings()
    {
        _controls = new Controls();
        _controls.Enable();
        Container.Bind<Controls>().FromInstance(_controls).AsSingle().NonLazy();
        Container.Bind<GameController>().FromInstance(_gameController).AsSingle().NonLazy();
        Container.Bind<UIController>().FromInstance(_uiContriller).AsSingle().NonLazy();
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<LidarDetected>();
        Container.DeclareSignal<DirtCollected>();
    }
}
