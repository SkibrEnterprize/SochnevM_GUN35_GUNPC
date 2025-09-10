// ¬ методы можно добавить свою логику отображени€
using TMPro;
using UnityEngine;

public sealed class PointView : MonoBehaviour
{
    [SerializeField] private TMP_Text _pointText;

    public void SetupPoint(string point)
    {
        _pointText.text = point;
    }
    public void ChangePoint(string point)
    {
        _pointText.text = point;
    }
    public void RemovePoint(string point)
    {
        _pointText.text = point;

    }
    public void AddPoint(string point)
    {
        _pointText.text = point;
    }
}
