using UnityEngine;

[CreateAssetMenu(menuName = "Path/Movement Path")]
public class MovementPath : ScriptableObject
{
    [field: SerializeField]
    public Vector3[] Points { get; private set; }
}