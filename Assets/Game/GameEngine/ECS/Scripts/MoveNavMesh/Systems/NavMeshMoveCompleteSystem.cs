using Game.GameEngine.Ecs;
using GameECS;
using UnityEngine.AI;

namespace SampleProject
{
    /// <summary>
    /// После того как NavMeshAgent дошёл до цели,
    /// помечаем MoveToPositionData как достигнутую.
    /// </summary>
    public sealed class NavMeshMoveCompleteSystem : IEcsFixedUpdate
    {
        private readonly EcsPool<NavMeshAgentComponent> _agentPool;
        private readonly EcsPool<MoveToPositionData> _movePool;

        void IEcsFixedUpdate.FixedUpdate(int entity)
        {
            if (!_agentPool.HasComponent(entity) || !_movePool.HasComponent(entity))
                return;

            ref var agentComp = ref _agentPool.GetComponent(entity);
            NavMeshAgent agent = agentComp.agent;
            if (agent == null) return;

            // Если агент не «ожидает» пути и расстояние до цели
            // меньше чем stoppingDistance – считаем цель достигнутой.
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                ref var moveData = ref _movePool.GetComponent(entity);
                moveData.isReached = true;
            }
        }
    }
}
