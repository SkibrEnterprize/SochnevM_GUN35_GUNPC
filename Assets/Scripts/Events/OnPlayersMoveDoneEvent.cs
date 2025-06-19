using System;
public class OnPlayersMoveDoneEvent : IGameEvent1
{
    public event Action OnEventTriggered;

    public void TriggerEvent()
    {
        OnEventTriggered?.Invoke();
    }
}
