using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;
public class SceneInstaller : MonoInstaller
{
    private Controls _controls;
    public override void InstallBindings()
    {
        _controls = new Controls();
        _controls.Enable();        
        ////Container.Bind<Player>().FromInstance(_player).AsSingle().NonLazy();
        Container.Bind<Controls>().FromInstance(_controls).AsSingle().NonLazy();
        //Container.BindInterfacesAndSelfTo<Controls>().AsSingle().NonLazy();
        Container.Bind<IPointClickEvent>().To<PointClickEvent>().AsSingle().NonLazy();
        Container.Bind<IGameEvent>().To<OnPlayersMoveEvent>().AsSingle().NonLazy();
        Container.Bind<IGameEvent1>().To<OnPlayersMoveDoneEvent>().AsSingle().NonLazy();
        //Container.Bind<PlayerController>().AsSingle().NonLazy();

        //Controls controls = Container.Resolve<Controls>(); 
        //controls.Enable();
    }
}