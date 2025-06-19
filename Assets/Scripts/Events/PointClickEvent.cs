using System;
public class PointClickEvent : IPointClickEvent
{
    public event Action<Cell> OnPointerClickEvent;

    public void TriggerPointerClickEvent(Cell cell)
    {
        OnPointerClickEvent?.Invoke(cell);
    }
}
