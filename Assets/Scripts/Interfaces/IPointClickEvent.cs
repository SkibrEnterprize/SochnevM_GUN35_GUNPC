using System;

public interface IPointClickEvent
{
    event Action<Cell> OnPointerClickEvent;
    void TriggerPointerClickEvent(Cell cell);
}
