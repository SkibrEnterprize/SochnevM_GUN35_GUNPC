using UnityEngine;
using Zenject;

public sealed class UiInstaller : MonoInstaller
{
    [SerializeField] private PointView _pointView;
    public override void InstallBindings()
    {
        Container
            .BindInterfacesTo<PointPresenter>()
            .AsSingle()
            .WithArguments(_pointView)
            .NonLazy();
    }
}
