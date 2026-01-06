using GameECS;
using static UnityEngine.EventSystems.EventTrigger;

namespace Game.GameEngine.Ecs
{
    public sealed class DestroySystem_HitPointsEmpty : IEcsFixedUpdate
    {
        private readonly EcsPool<HitPointsComponent> hitPointsPool;
        private readonly EcsEmitter<DestroyEvent> destroyEmitter;
        private readonly EcsPool<DeathComponent> deathPool;
       

       
        void IEcsFixedUpdate.FixedUpdate(int entity)
        {
            if (!this.hitPointsPool.HasComponent(entity)) return;
            if (this.deathPool.HasComponent(entity)) return;


            ref var hitPoints = ref this.hitPointsPool.GetComponent(entity);
            if (hitPoints.current <= 0)
            {
                this.deathPool.SetComponent(entity, new DeathComponent());                
            }
        }
    }
}
