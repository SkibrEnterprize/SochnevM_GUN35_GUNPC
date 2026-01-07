using GameECS;
using UnityEngine.AI;

namespace Game.GameEngine.Ecs
{
    public sealed class NavMeshMoveSystem : IEcsFixedUpdate
    {
        private readonly EcsPool<NavMeshAgentComponent> agentPool;
        private readonly EcsPool<TransformComponent> transformPool;

        void IEcsFixedUpdate.FixedUpdate(int entity)
        {
            if (!agentPool.HasComponent(entity)) return;

            ref var agentComp = ref agentPool.GetComponent(entity);
            NavMeshAgent agent = agentComp.agent;

            // Если агент уже в пути, ничего не делаем – он сам обновит позицию.
            // Но можно добавить проверку «достиг ли цель» и удалить компонент/запустить следующий шаг.
        }
    }
}