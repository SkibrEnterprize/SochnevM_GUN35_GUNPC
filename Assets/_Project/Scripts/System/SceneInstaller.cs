using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private int _startPoint;
    private Controls _controls;
    public override void InstallBindings()
    {
        _controls = new Controls();
        _controls.Enable();
        Container.Bind<Controls>()
            .FromInstance(_controls)
            .AsSingle()
            .NonLazy();
        Container.Bind<ClassForGameLogic>()
            .AsSingle()
            .WithArguments(_startPoint)
            .NonLazy();
    }
}
