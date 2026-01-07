using GameECS;

namespace Game.GameEngine.Ecs
{
    public sealed class DestroyObserver_DisableGameObject : IEcsObserver<DestroyEvent>
    {
        private readonly EcsEmitter<DestroyEvent> destroyEmitter;
        private readonly EcsPool<GameObjectComponent> gameObjectPool;
        private readonly EcsPool<HitPointsComponent> hitPointsPool;
        private readonly EcsPool<AttackTarget> attackPool;
        private readonly EcsWorld world;


        void IEcsObserver<DestroyEvent>.Handle(int entity, DestroyEvent destroyEvent)
        {            
            hitPointsPool.RemoveComponent(entity);
            ref var goComponent = ref this.gameObjectPool.GetComponent(entity);
            goComponent.value.SetActive(false);


            //attackPool.RemoveComponent(entity);
        }
    }
}