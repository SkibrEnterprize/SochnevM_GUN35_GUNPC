using UnityEngine;
using DG.Tweening;

public enum LightMode
{
    Dynamic,
    Day,
    Naght
}

[RequireComponent(typeof(Transform))]
public class SunRotator : MonoBehaviour
{
    [SerializeField] private LightMode _lightMode = LightMode.Dynamic;
    [SerializeField] private float _speedDegPerSec = 90f;  
    [SerializeField] private Vector3 _rotationAxis = Vector3.left;

    void Awake()
    {
        switch (_lightMode)
        {
            case LightMode.Dynamic:
                StartRotating();
                break;
            case LightMode.Day:
                transform.rotation = Quaternion.Euler(_rotationAxis * 90f);
                break;
            case LightMode.Naght:
                transform.rotation = Quaternion.Euler(_rotationAxis * 270);
                break;
        }
    }
    
    private void StartRotating()
    {

        if (_speedDegPerSec <= 0f) return;
        float duration = 360f / Mathf.Abs(_speedDegPerSec);

        transform.DORotate(
            new Vector3(
                _rotationAxis.x * 360f,
                _rotationAxis.y * 360f,
                _rotationAxis.z * 360f
            ),
            duration,
            RotateMode.FastBeyond360
        )
        .SetLoops(-1, LoopType.Restart)
        .SetEase(Ease.Linear);
    }
}