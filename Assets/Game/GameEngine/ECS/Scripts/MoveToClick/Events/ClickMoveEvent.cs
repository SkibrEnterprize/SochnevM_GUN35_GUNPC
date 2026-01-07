using UnityEngine;

namespace Game.GameEngine.Ecs
{
    public struct ClickMoveEvent
    {
        public int entity;          // id сущности (если нужно). Может быть 0, если событие глобальное.
        public Vector3 destination;
    }
}