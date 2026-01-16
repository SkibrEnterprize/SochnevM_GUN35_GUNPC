using Game.GameEngine.Ecs;
using GameECS;
using UnityEngine;
using UnityEngine.AI;

namespace SampleProject
{
    /// <summary>
    /// —инхронизирует NavMeshAgent со Ђмаркеромї MoveStepData,
    /// который затем используетс€ CharacterAnimatorSystem.
    /// </summary>
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

            // ≈сли агент в движении Ц ставим MoveStepData
            bool moving = !agent.pathPending && agent.remainingDistance > agent.stoppingDistance;

            if (moving)
            {
                // —оздаЄм Ђпсевдо?шагї дл€ анимации.
                // ѕуть не важен, нужна только информаци€ о том,
                // что юнит двигаетс€. ƒл€ простоты будем ставить
                // направление к цели NavMeshAgent.
                Vector3 dir = (agent.destination - agent.transform.position).normalized;
                _stepPool.SetComponent(entity, new MoveStepData { direction = dir });
            }
            else
            {
                // ќстановились Ц убираем маркер
                if (_stepPool.HasComponent(entity))
                    _stepPool.RemoveComponent(entity);
            }
        }

        
    }
}
