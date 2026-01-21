using Game.GameEngine.Ecs;
using SampleProject;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Entities
{
    
    public sealed class CharacterEntity : Entity
    {
        [SerializeField]
        private CharacterConfig config;

        protected override void Init()
        {
            this.SetData(new SmoothRotationComponent()); 

            this.SetData(new CombatComponent  
            {
                damage = this.config.damage,
                minDistance = config.minDistance,
                animationTime = this.config.animationTime,
                timeBetweenAttack = this.config.timeBetweenAttack,
                damageType = this.config.damageType,
                deathTime = this.config.deathTime,
            });

            this.SetData(new AnimatorComponent  
            {
                value = this.GetComponentInChildren<AnimatorMachine>()
            });

            this.SetData(new HitPointsComponent
            {
                max = this.config.hitPoints,
                current = this.config.hitPoints
            });

            this.SetData(new MoveSpeedComponent
            {
                value = this.config.moveSpeed
            });

            this.SetData(new TransformComponent
            {
                value = this.transform,
                radius = this.config.radius
            });

            var agent = this.gameObject.GetComponent<NavMeshAgent>();
            agent.speed = this.config.moveSpeed;
            

            this.SetData(new NavMeshAgentComponent { agent = agent });

            this.SetData(new GameObjectComponent
            {
                value = this.gameObject
            });

            this.SetData(new RigidbodyComponent
            {
                value = this.GetComponent<Rigidbody>()
            });

            this.SetData(new RendererComponent
            {
                value = this.GetComponentInChildren<Renderer>()
            });
        }

    }
}