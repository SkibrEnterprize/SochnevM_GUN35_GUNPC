using GameECS;
using UnityEngine;
using UnityEngine.AI;

namespace Game.GameEngine.Ecs
{
    public sealed class NavMeshPatrolSystem : IEcsFixedUpdate
    {
        private readonly EcsPool<NavMeshAgentComponent> agentPool;
        private readonly EcsPool<PatrolData> patrolPool;

        void IEcsFixedUpdate.FixedUpdate(int entity)
        {
            if (!patrolPool.HasComponent(entity) || !agentPool.HasComponent(entity))
                return;

            ref var patrol = ref patrolPool.GetComponent(entity);
            ref var agentComp = ref agentPool.GetComponent(entity);

            NavMeshAgent agent = agentComp.agent;
            if (agent == null) return;

            if (patrol.points == null || patrol.points.Count == 0)
            {
                Debug.LogError($"Entity {entity} has no patrol points!");
                return;
            }

            Vector3 targetPoint = patrol.GetCurrentPoint();

            if (!agent.pathPending && Vector3.Distance(agent.transform.position, targetPoint) <= patrol.stoppingDistance)
            {
                patrol.MoveNext();
                targetPoint = patrol.GetCurrentPoint();  
            }

            agent.SetDestination(targetPoint);
        }
    }
}