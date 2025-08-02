using System;
using TMPro;
using UnityEngine;


[Serializable]
public sealed class UIConfig
{
    [field: SerializeField]
    public TextMeshProUGUI TotalScoreText { get; private set; }

    [field: SerializeField]
    public TextMeshProUGUI TotalThrowsText { get; private set; }

    [field: SerializeField]
    public TextMeshProUGUI TotalAttemptsText { get; private set; }

    [field: SerializeField]
    public TextMeshProUGUI TotalSkittleText { get; private set; }
}
