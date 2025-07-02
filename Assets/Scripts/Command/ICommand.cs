using UnityEngine.EventSystems;
public interface ICommand
{
    void Execute(PointerEventData eventData);
}