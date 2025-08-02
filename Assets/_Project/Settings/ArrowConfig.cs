using System;
using UnityEngine;


[Serializable]
public sealed class ArrowConfig
{
    [field: SerializeField]
    public float SwingSpeed = 2f;

    [field: SerializeField]
    public float MaxAngle = 30f;

    [field: SerializeField]
    public float MoveSpeed = 5f;

    [field: SerializeField]
    public float Distance = 3f;
}
