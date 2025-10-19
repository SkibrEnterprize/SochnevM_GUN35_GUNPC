using DG.Tweening;
using TMPro;
using UnityEngine;

public sealed class LaserMirror : MonoBehaviour
{

    [Header("Rotation settings")]
    [SerializeField]
    private float _rotateAngleDeg = 90f;
    [SerializeField]
    private float _duration = 0.4f;

    [SerializeField,
     Tooltip("Тип easing (ускорение/замедление)")]
    private Ease _easeType = Ease.OutCubic;   

    public void RotateMirror()
    {
        transform.DOKill();

        transform.DORotate(new Vector3(0f, _rotateAngleDeg, 0f), _duration,
                           RotateMode.LocalAxisAdd)
                  .SetEase(_easeType);
    }
    private void OnMouseDown()
    {
        RotateMirror();
    }
}