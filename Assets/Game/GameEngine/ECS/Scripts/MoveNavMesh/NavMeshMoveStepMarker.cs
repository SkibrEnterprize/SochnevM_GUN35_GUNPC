using Game.GameEngine.Ecs;
using GameECS;
using UnityEngine;
using UnityEngine.AI;

namespace SampleProject
{
    public sealed class NavMeshMoveStepMarker : IEcsFixedUpdate
    {
        private readonly EcsPool<NavMeshAgentComponent> _agentPool;
        private readonly EcsPool<TransformComponent> _transformPool;
        private readonly EcsPool<MoveStepData> _stepPool;

        void IEcsFixedUpdate.FixedUpdate(int entity)
        {
            if (!_agentPool.HasComponent(entity) || !_transformPool.HasComponent(entity))
                return;

            ref var agentComp = ref _agentPool.GetComponent(entity);
            NavMeshAgent agent = agentComp.agent;
            if (agent == null) return;

            // Если агент в движении – ставим MoveStepData
            bool moving = !agent.pathPending && agent.remainingDistance > agent.stoppingDistance;

            if (moving)
            {
                
                Vector3 dir = (agent.destination - agent.transform.position).normalized;
                _stepPool.SetComponent(entity, new MoveStepData { direction = dir });
            }
            else
            {
                if (_stepPool.HasComponent(entity))
                    _stepPool.RemoveComponent(entity);
            }
        }

        
    }
}
