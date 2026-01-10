using Game.GameEngine.Ecs;
using GameECS;
using System.Diagnostics;
using UnityEngine;

namespace SampleProject
{
    public sealed class CharacterAnimatorSystem : IEcsUpdate
    {
        private EcsPool<AnimatorComponent> animatorPool;

        private EcsPool<MoveStepData> moveStep;
        private EcsPool<HitDuration> attackPool;
        private EcsPool<GatherDuration> gatherPool;
        private EcsPool<DeathComponent> deathPool;

        void IEcsUpdate.Update(int entity)
        {
            ref var animator = ref this.animatorPool.GetComponent(entity).value;
            var animatorState = this.ResolveState(entity);
            animator.ChangeState(animatorState);

        }

        private int ResolveState(int entity)
        {
            if (this.deathPool.HasComponent(entity))
            {                
                return AnimatorStateId.DEATH;     // если сущность «умирает», ставим death
            }

            if (this.attackPool.HasComponent(entity))
            {
                return AnimatorStateId.ATTACK;
            }

            if (this.gatherPool.HasComponent(entity))
            {
                return AnimatorStateId.GATHERING;
            }

            if (this.moveStep.HasComponent(entity))
            {
                return AnimatorStateId.MOVE;
            }

            return AnimatorStateId.IDLE;
        }
    }
}