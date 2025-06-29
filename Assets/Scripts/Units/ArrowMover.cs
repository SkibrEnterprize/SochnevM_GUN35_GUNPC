using UnityEngine;

public class ArrowMover : MonoBehaviour
{
    // Создаем переменную для хранения значения масштаба по оси Y и величин максимального и минимального скейла
    [SerializeField] private float _minScale = 0.1f;
    [SerializeField] private float _maxScale = 0.7f;
    private float _deltaY;

    // Создаем переменную, которая определяет направление изменения масштаба
    private int direction = 1;

        // Задаем начальное значение масштаба в методе Start
    void Awake()
    {
        _deltaY = transform.localPosition.y;
    }

    void Update()
    {
        // Увеличиваем масштаб по оси Y
        if (_deltaY < _maxScale && direction == 1)
        {
            _deltaY += Time.deltaTime; // меняем значение масштаба
        }
        else
        {
            direction = -1; // меняем направление изменения масштаба
        }

        // Уменьшаем масштаб по оси Y
        if (_deltaY > _minScale && direction == -1)
        {
            _deltaY -= Time.deltaTime; // меняем значение масштаба
        }
        else
        {
            direction = 1; // меняем направление изменения масштаба
        }

        // Устанавливаем новый масштаб
        transform.localPosition = new Vector3(transform.localPosition.x, _deltaY, transform.localPosition.z);
       
    }
}