using System;
public class OnPlayersMoveEvent : IGameEvent
{
    public event Action OnEventTriggered;

    public void TriggerEvent()
    {
        OnEventTriggered?.Invoke();
    }
}
