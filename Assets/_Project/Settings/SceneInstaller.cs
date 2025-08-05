using Zenject;

public class SceneInstaller : MonoInstaller
{
    private Controls _controls;
    public override void InstallBindings()
    {

        _controls = new Controls();
        _controls.Enable();
        Container.Bind<Controls>().FromInstance(_controls).AsSingle().NonLazy();
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<LidarDetected>();
    }
}
