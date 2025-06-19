using System;

public interface IGameEvent
{
    event Action OnEventTriggered;
    void TriggerEvent();
}
