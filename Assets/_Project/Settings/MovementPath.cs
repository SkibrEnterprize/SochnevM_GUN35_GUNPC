using UnityEngine;

[CreateAssetMenu(menuName = "Path/Movement Path")]
public class MovementPath : ScriptableObject
{
    public Transform[] points;
}