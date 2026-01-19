using Game.GameEngine.Ecs;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SampleProject
{
    //TEST
    public sealed class CommandController : MonoBehaviour
    {
        [SerializeField]
        private Entity entity;
        private Outline _outline;


        void Awake()
        {
            _outline = gameObject.GetComponent<Outline>();   // добавляем скрипт Outline
        }

        [Button]
        public void MoveToPosition(Transform point)
        {
            this.entity.SetData(new CommandRequest
            {
                type = CommandType.MOVE_TO_POSITION,
                args = point.position,
                status = CommandStatus.IDLE
            });
        }
        public void MoveToPosition(Vector3 position)
        {
            this.entity.SetData(new CommandRequest
            {
                type = CommandType.MOVE_TO_POSITION,
                args = position,
                status = CommandStatus.IDLE
            });
        }

        [Button]
        public void AttackTarget(Entity target)
        {
            this.entity.SetData(new CommandRequest
            {
                type = CommandType.ATTACK_TARGET,
                args = target,
                status = CommandStatus.IDLE
            });
        }

        [Button]
        public void GatherResource(Entity resource)
        {
            this.entity.SetData(new CommandRequest
            {
                type = CommandType.GATHER_RESOURCE,
                args = resource,
                status = CommandStatus.IDLE
            });
        }

        [Button]
        public void Patrol(Transform[] points)
        {
            var positions = points.Select(t => t.position).ToList();
            this.entity.SetData(new CommandRequest
            {
                type = CommandType.PATROL_BY_POINTS,
                args = positions,            // List<Vector3>
                //args = points.Select(it => it.position).ToList(),
                status = CommandStatus.IDLE
            });
        }

        [Button]
        public void Stop()
        {
            this.entity.RemoveData<CommandRequest>();
        }

        [Button]
        public void SetHighlighted()
        {
            _outline.enabled = !_outline.enabled;
        }
    }




}