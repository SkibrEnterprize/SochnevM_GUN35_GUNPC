using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameEngine.Ecs
{
    [Serializable]
    public struct PatrolData
    {
        public List<Vector3> points;
        public int pointer;
        
        public float stoppingDistance;

        public Vector3 GetCurrentPoint()
        {
            if (points == null || points.Count == 0)
                throw new InvalidOperationException(
                    $"PatrolData.points is empty for entity. " +
                    $"Add at least one point before calling GetCurrentPoint().");

            // Если по какой‑то причине pointer вышел за пределы, сбрасываем его
            if (pointer >= points.Count) pointer = 0;

            return points[pointer];
            //return this.points[this.pointer];
        }

        public void MoveNext()
        {
            this.pointer = (this.pointer + 1) % this.points.Count;
        }
    }
}