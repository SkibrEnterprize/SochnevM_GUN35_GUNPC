using UnityEngine;

public class Primitives : MonoBehaviour
{
    public enum NeighbourType
    {
        Left,
        Right,
        Back,
        Forward,
        LeftBack,
        RightBack,
        LeftForward,
        RightForward,
    }

    public enum Team
    {
        Player1,
        Player2,
    }
}
