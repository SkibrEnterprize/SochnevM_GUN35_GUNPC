using UnityEngine;
using UnityEngine.UI;

public class PathSelector : MonoBehaviour
{
    [SerializeField] private Dropdown _dropdown;         
    [SerializeField] private CharacterMoverDOTween _mover;          

    void Awake()
    {
        // Заполняем список вариантов
        _dropdown.options.Clear();
        for (int i = 0; i < _mover.Paths.Length; i++)
            _dropdown.options.Add(new Dropdown.OptionData($"Путь {i}"));

        _dropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    private void OnDropdownChanged(int index)
    {
        _mover.SetPath(index);          
    }
}