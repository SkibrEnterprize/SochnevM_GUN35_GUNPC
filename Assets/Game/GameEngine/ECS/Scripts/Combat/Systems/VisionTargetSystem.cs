using GameECS;
using UnityEngine;

namespace Game.GameEngine.Ecs
{
    public sealed class VisionTargetSystem : IEcsFixedUpdate
    {
        private readonly EcsPool<TransformComponent> transformPool;
        private readonly EcsPool<AttackTarget> targetPool;
        private const int enemyLayerIndex = 8;

        void IEcsFixedUpdate.FixedUpdate(int entity)
        {
            ref var trans = ref transformPool.GetComponent(entity).value;

            if (targetPool.HasComponent(entity)) return;   // уже в процессе атаки

            LayerMask enemyMask = 1 << enemyLayerIndex;
            Collider[] hits = Physics.OverlapSphere(trans.position, 3f, enemyMask);

            if (hits.Length == 0) return;

            var targetObj = hits[0].transform.gameObject;
            var targetEntity = targetObj.GetComponent<Entity>();
            var enemyComp = targetObj.GetComponent<EnemyComponent>();

            // Получаем GameObject сущности, к которой привязан TransformComponent
            var selfObj = trans.gameObject;
            var selfEntity = selfObj.GetComponent<Entity>();
            var selfEnemyComp = selfObj.GetComponent<EnemyComponent>();

            if (targetEntity != null && enemyComp != null && selfEnemyComp == null)
            {
                selfEntity.SetData(new CommandRequest
                {
                    type = CommandType.ATTACK_TARGET,
                    args = targetObj.GetComponent<Entity>(),
                    status = CommandStatus.IDLE
                });
            }
        }
       
    }
}
