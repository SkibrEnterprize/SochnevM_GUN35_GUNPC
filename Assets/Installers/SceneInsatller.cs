using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;
public class SceneInstaller : MonoInstaller
{   
   
    

    public override void InstallBindings()
    {
        //Container.Bind<Player>().FromInstance(_player).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<Controls>().AsSingle().NonLazy();
    }
}