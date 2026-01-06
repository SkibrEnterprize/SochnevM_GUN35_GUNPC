using GameECS;
using UnityEngine;

namespace Game.GameEngine.Ecs
{
    [CreateAssetMenu(
        fileName = "New Installer «Destroy»",
        menuName = "Game/GameEngine/Ecs/New Installer «Destroy»"
    )]
    public sealed class DestroyInstaller : EcsInstaller
    {
        public override void Install(EcsWorld world)
        {
            world.DeclareComponent<DeathComponent>();

            world.DeclareSystem<DestroySystem_HitPointsEmpty>();
            world.DeclareSystem<DeathTimerSystem>();
            world.DeclareObserver<DestroyEvent, DestroyObserver_DisableGameObject>();
        }
    }
}