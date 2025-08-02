using System;
using UnityEngine;


[Serializable]
public sealed class SkittleConfig
{
    [field: SerializeField]
    public Transform SpawnPoint { get; private set; }

    [field: SerializeField, Range(1, 6)]
    public int Rows { get; private set; } = 6;

    [field: SerializeField, Range(1, 3)]
    public float SpacingOfRows { get; private set; } = 2.99f;
}
