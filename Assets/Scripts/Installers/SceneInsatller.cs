using UnityEngine;
using Zenject;
public class SceneInstaller : MonoInstaller
{
    [SerializeField] Battlefield _battlefield;
    [SerializeField] PlayerController _playerController;
    private Controls _controls;
   
    public override void InstallBindings()
    {
        _controls = new Controls();
        _controls.Enable();        
        Container.Bind<Controls>().FromInstance(_controls).AsSingle().NonLazy();
        Container.Bind<Battlefield>().FromInstance(_battlefield).AsSingle().NonLazy();        
        Container.Bind<PlayerController>().FromInstance(_playerController).AsSingle().NonLazy();
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<PlayersMoveDone>();
        Container.DeclareSignal<SelectCancel>();
        Container.DeclareSignal<SelectInstall>();
        Container.DeclareSignal<SelectConfirm>();
        Container.DeclareSignal<ChangeSelectedUnit>();
        Container.DeclareSignal<TransferOfTurn>();
        Container.DeclareSignal<DebugSignal>();
    }
}