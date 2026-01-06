using GameECS;
using UnityEngine;

namespace Game.GameEngine.Ecs
{
    public sealed class DeathTimerSystem : IEcsFixedUpdate
    {
        private readonly EcsPool<DeathComponent> deathPool;
        private readonly EcsPool<CombatComponent> combatPool;
        private readonly EcsEmitter<DestroyEvent> destroyEmitter;

        void IEcsFixedUpdate.FixedUpdate(int entity)
        {
            if (!this.deathPool.HasComponent(entity)) return;

            ref var combatComponent = ref this.combatPool.GetComponent(entity);
            float deathTime = combatComponent.deathTime;
            deathTime -= Time.fixedDeltaTime;   // уменьшаем по времени

            if (deathTime <= 0f)
            {
                this.destroyEmitter.SendEvent(entity, new DestroyEvent());
               
                // Можно удалить DeathComponent, чтобы не проверять его дальше
                this.deathPool.RemoveComponent(entity);
            }
        }
    }
}