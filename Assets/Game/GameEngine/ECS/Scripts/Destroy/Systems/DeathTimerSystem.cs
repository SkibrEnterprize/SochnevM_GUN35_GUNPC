using GameECS;
using UnityEngine;

namespace Game.GameEngine.Ecs
{
    public sealed class DeathTimerSystem : IEcsFixedUpdate
    {
        private readonly EcsPool<DeathComponent> deathPool;
        private readonly EcsEmitter<DestroyEvent> destroyEmitter;

        void IEcsFixedUpdate.FixedUpdate(int entity)
        {
            if (!this.deathPool.HasComponent(entity)) return;

            ref var deathPool = ref this.deathPool.GetComponent(entity);
            deathPool.deathTime -= Time.fixedDeltaTime; 
            if (deathPool.deathTime <= 0f)
            {
                this.destroyEmitter.SendEvent(entity, new DestroyEvent());
                this.deathPool.RemoveComponent(entity);
            }
        }
    }
}