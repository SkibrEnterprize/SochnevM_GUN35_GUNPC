using Game.GameEngine.Ecs;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private int _startPoint;
    [SerializeField] private string _inputControllerPrefabPath = "Prefabs/RTSInputController";
    [SerializeField] private string _ecsModulePrefabPath = "Prefabs/ECSModule";
    private PlayerInputActions _playerInput;
    public override void InstallBindings()
    {
        // привязка и включение управления
        _playerInput = new PlayerInputActions();
        _playerInput.Enable();
        Container.Bind<PlayerInputActions>()
            .FromInstance(_playerInput)
            .AsSingle()
            .NonLazy();

        // привязка RTSInputController
        var prefab = Resources.Load<GameObject>(_inputControllerPrefabPath);
        if (prefab == null)
            Debug.Log($"Не удалось найти префаб по пути {_inputControllerPrefabPath}");
        var instance = Instantiate(prefab);
        var controller = instance.GetComponent<RTSInputController>();
        Container.BindInstance(controller).AsSingle();

        // привязка ECSModule
        var prefabECS = Resources.Load<GameObject>(_ecsModulePrefabPath);
        if (prefabECS == null)
            Debug.Log($"Не удалось найти префаб по пути {_ecsModulePrefabPath}");
        var instanceECS = Instantiate(prefabECS);
        var ecsModule = instanceECS.GetComponent<EcsModule>();
        Container.BindInstance(ecsModule).AsSingle();

        //// привязка пула Enemy
        //var prefabPoolEnemy = Resources.Load<GameObject>(_ecsModulePrefabPath);
        //if (prefabECS == null)
        //    Debug.Log($"Не удалось найти префаб по пути {_ecsModulePrefabPath}");
        //var instanceECS = Instantiate(prefabECS);
        //var ecsModule = instanceECS.GetComponent<EcsModule>();
        //Container.BindInstance(ecsModule).AsSingle();

        //Container.Bind<ClassForGameLogic>()
        //    .AsSingle()
        //    .WithArguments(_startPoint)
        //    .NonLazy();

    }
}
