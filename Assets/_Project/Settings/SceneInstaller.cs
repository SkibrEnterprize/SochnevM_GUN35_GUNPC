using Netologia.Quest;
using Netologia.Quest.Characters.Player;
using Netologia.Quest.Interfaces;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private LaserManager _laserManager;
    [SerializeField] private Director _director;
    [SerializeField] private QuestAssistant _questAssistant;
    private Controls _controls;
    public override void InstallBindings()
    {

        _controls = new Controls();
        _controls.Enable();
        Container.Bind<Controls>().FromInstance(_controls).AsSingle().NonLazy();
        Container.Bind<LaserManager>().FromInstance(_laserManager).AsSingle().NonLazy();
        Container.Bind<Director>().FromInstance(_director).AsSingle().NonLazy();
        Container.Bind<QuestAssistant>().FromInstance(_questAssistant).AsSingle().NonLazy();

    }
}
