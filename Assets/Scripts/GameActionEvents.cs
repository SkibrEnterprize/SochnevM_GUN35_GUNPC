using System;
using UnityEngine;

public class GameActionEvents : MonoBehaviour
{
    public static Action<Cell> OnPointerClickEvent;
    public static Action OnPlayersMoveEvent;
    public static Action OnPlayersMoveDoneEvent;
}

