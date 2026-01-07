using Game.GameEngine.Ecs;
using SampleProject;
using UnityEngine;
using UnityEngine.AI;

namespace Entities
{
    // сущность для назначения в нее компонентов ECS и размещения на объекте Monobeh-е
    public sealed class CharacterEntity : Entity
    {
        [SerializeField]
        private CharacterConfig config; // скриптабл-объект для упрощения установки значений

        protected override void Init()
        {
            this.SetData(new SmoothRotationComponent()); // привязка компонента ECS

            this.SetData(new CombatComponent    //привязка компонента ECS и установка его значений из скрипт.объекта
            {
                damage = this.config.damage,
                minDistance = config.minDistance,
                animationTime = this.config.animationTime,
                timeBetweenAttack = this.config.timeBetweenAttack,
                damageType = this.config.damageType,
                deathTime = this.config.deathTime,
            });
            
            this.SetData(new AnimatorComponent  // привязка в компонент ECS компонента Monobeh-а 
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