using UnityEngine;

[CreateAssetMenu(fileName = "New Cell Palette Settings", menuName = "Custom/Cell")]
public class CellPaletteSettings : ScriptableObject
{
    [SerializeField] Material _cellIsSelect;
    [SerializeField] Material _cellIsAccessForAction;
}
