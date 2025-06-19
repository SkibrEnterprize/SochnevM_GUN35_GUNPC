using System;

public interface IGameEvent1
{
    event Action OnEventTriggered;
    void TriggerEvent();
}
