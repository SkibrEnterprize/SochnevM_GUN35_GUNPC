using GameECS;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace Game.GameEngine.Ecs
{
    public sealed class DestroySystem_HitPointsEmpty : IEcsFixedUpdate
    {
        private readonly EcsPool<HitPointsComponent> hitPointsPool;
        private readonly EcsPool<DeathComponent> deathPool;
        private readonly EcsPool<CombatComponent> combatPool;

        void IEcsFixedUpdate.FixedUpdate(int entity)
        {
            if (!this.hitPointsPool.HasComponent(entity)) return;
            if (this.deathPool.HasComponent(entity)) return;

            ref var hitPoints = ref this.hitPointsPool.GetComponent(entity);
            if (hitPoints.current <= 0)
            {   
                var deathTime = this.combatPool.GetComponent(entity).deathTime;
                this.deathPool.SetComponent(entity, new DeathComponent { deathTime = deathTime });
                Debug.Log("Death Component Added");
                hitPointsPool.RemoveComponent(entity);                 
            }
        }
    }
}
