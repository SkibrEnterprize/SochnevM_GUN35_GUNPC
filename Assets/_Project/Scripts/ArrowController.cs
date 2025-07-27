using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private float _swingSpeed = 2f; // Скорость колебания
    [SerializeField] private float _maxAngle = 30f; // Максимальный угол

    [SerializeField] private float _angle;
    private float _initX;
    private float _initY;
    private float _initZ;

    private void Start()
    {
        _initX = transform.rotation.eulerAngles.x;
        _initY = transform.rotation.eulerAngles.y;
        _initZ = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        _angle = Mathf.Sin(Time.time * _swingSpeed) * _maxAngle; // Вычисление угла
        transform.rotation = Quaternion.Euler(_initX, _initY, _initZ + _angle); // Установка вращения
    }

    public Vector3 GetDirection()
    {
        return transform.up;
    }
}
