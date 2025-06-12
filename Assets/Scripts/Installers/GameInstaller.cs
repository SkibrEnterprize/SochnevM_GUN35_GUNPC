using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;
public class GameInstaller : MonoInstaller
{
    [SerializeField] private Player _player;

    //private Controls _controls;    

    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(_player).AsSingle().NonLazy();
        //Container.Bind<Controls>().FromInstance(_controls).AsSingle().NonLazy();
    }
}
