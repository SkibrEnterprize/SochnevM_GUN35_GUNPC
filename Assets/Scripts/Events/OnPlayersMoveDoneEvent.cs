using System;
public class OnPlayersMoveDoneEvent : IGameEvent
{
    public event Action OnEventTriggered;

    public void TriggerEvent()
    {
        OnEventTriggered?.Invoke();
    }
}
