using UnityEngine.EventSystems;
public class CellClickCommand : ICommand
{
    private Cell _cell;
    public CellClickCommand(Cell cell)
    {
        _cell = cell;
    }
    public void Execute(PointerEventData eventData)
    {
        _cell?.OnPointerClick(eventData);
    }
}