using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;
using static UnityEditor.Experimental.GraphView.GraphView;
public class SceneInstaller : MonoInstaller
{
    [SerializeField] Battlefield _battlefield;
    [SerializeField] PlayerController _playerController;
    private Controls _controls;
   
    public override void InstallBindings()
    {
        _controls = new Controls();
        _controls.Enable();        
        ////Container.Bind<Player>().FromInstance(_player).AsSingle().NonLazy();
        Container.Bind<Controls>().FromInstance(_controls).AsSingle().NonLazy();
        //Container.BindInterfacesAndSelfTo<Controls>().AsSingle().NonLazy();
        Container.Bind<IPointClickEvent>().To<PointClickEvent>().AsSingle().NonLazy();
        //Container.Bind<IGameEvent>().WithId("onPlayersMove").To<OnPlayersMoveEvent>().AsSingle().NonLazy();
        //Container.Bind<IGameEvent>().WithId("onPlayersMoveDone").To<OnPlayersMoveDoneEvent>().AsSingle().NonLazy();
        Container.Bind<Battlefield>().FromInstance(_battlefield).AsSingle().NonLazy();
        Container.Bind<PlayerController>().FromInstance(_playerController).AsSingle().NonLazy();
        //Container.Bind<SignalBus>().AsSingle().NonLazy();
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<PlayersMove>();
        Container.DeclareSignal<PlayersMoveDone>();
        Container.DeclareSignal<SelectCancel>();
        Container.DeclareSignal<SelectConfirm>();
        Container.DeclareSignal<ChangeSelectedUnit>();
        Container.DeclareSignal<TransferOfTurn>();
        Container.DeclareSignal<DebugSignal>();


        //Controls controls = Container.Resolve<Controls>(); 
        //controls.Enable();
    }
}