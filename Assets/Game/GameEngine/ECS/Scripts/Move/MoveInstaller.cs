using GameECS;
using UnityEngine;

namespace Game.GameEngine.Ecs
{
    [CreateAssetMenu(
        fileName = "New Installer «Move»",
        menuName = "Game/GameEngine/Ecs/New Installer «Move»"
    )]
    public sealed class MoveInstaller : EcsInstaller
    {
        public override void Install(EcsWorld world)
        {
            // Компоненты
            world.DeclareComponent<NavMeshAgentComponent>();

            // Системы
            world.DeclareSystem<NavMeshMoveSystem>();
            world.DeclareSystem<NavMeshDestinationSystem>();
            world.DeclareSystem<NavMeshPatrolSystem>();

            //old

            //привязка компонентов с данными
            world.DeclareComponent<MoveSpeedComponent>();

            world.DeclareComponent<MoveStepData>();
            world.DeclareComponent<MoveToPositionData>();
            world.DeclareComponent<PatrolData>();

            //привязка систем для обработки компонентов

            world.DeclareSystem<MoveStepSystem>();
            world.DeclareSystem<MoveToPositionSystem>();
            world.DeclareSystem<PatrolPointsSystem>();
        }
    }
}