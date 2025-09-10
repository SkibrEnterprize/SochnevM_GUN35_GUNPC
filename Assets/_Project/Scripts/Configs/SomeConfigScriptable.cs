// Assets/Scripts/Configs/SomeConfig.cs
using UnityEngine;

[CreateAssetMenu(menuName = "Game Configs/SomeConfigScriptable")]
public class SomeConfigScriptable : ScriptableObject
{
    [Header("ConfigZone1")]
    public int Int1;
    public float Float1;
    public Transform Transform1;

    [Header("ConfigZone2")]

    public int Int2;
    public float Float2;
    public Transform Transform2;    
}