using System;
using TMPro;
using UnityEngine;


[Serializable]
public sealed class UIConfig
{
    [field: SerializeField]
    public TextMeshProUGUI TotalDirtCollect { get; private set; }
}
