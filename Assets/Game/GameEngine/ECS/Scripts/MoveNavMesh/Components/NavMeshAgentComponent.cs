// 1)  омпонент хранит ссылку на Unity NavMeshAgent
using System;
using UnityEngine.AI;

namespace Game.GameEngine.Ecs
{
    [Serializable]
    public struct NavMeshAgentComponent
    {
        public NavMeshAgent agent;   // ссылка, которую мы будем ставить через инспектор или фабрику
    }
}