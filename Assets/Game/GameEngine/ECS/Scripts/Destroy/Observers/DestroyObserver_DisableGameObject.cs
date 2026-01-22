using GameECS;

namespace Game.GameEngine.Ecs
{
    public sealed class DestroyObserver_DisableGameObject : IEcsObserver<DestroyEvent>
    {
        private readonly EcsPool<GameObjectComponent> gameObjectPool;
        private readonly EcsPool<HitPointsComponent> hitPointsPool;


        void IEcsObserver<DestroyEvent>.Handle(int entity, DestroyEvent destroyEvent)
        {            
            hitPointsPool.RemoveComponent(entity);
            ref var goComponent = ref this.gameObjectPool.GetComponent(entity);
            goComponent.value.SetActive(false);
        }
    }
}