using GameECS;
using UnityEngine.AI;

namespace Game.GameEngine.Ecs
{
    public sealed class NavMeshDestinationSystem : IEcsFixedUpdate
    {
        private readonly EcsPool<NavMeshAgentComponent> agentPool;
        private readonly EcsPool<MoveToPositionData> movePool;   // используем старый тип, чтобы не менять логику дальше

        void IEcsFixedUpdate.FixedUpdate(int entity)
        {
            if (!movePool.HasComponent(entity) || !agentPool.HasComponent(entity))
                return;

            ref var moveData = ref movePool.GetComponent(entity);
            ref var agentComp = ref agentPool.GetComponent(entity);

            NavMeshAgent agent = agentComp.agent;
            if (agent == null) return;

            //Если цель ещё не достигнута, ставим её
            if (!moveData.isReached)
                agent.SetDestination(moveData.destination);
        }
    }
}