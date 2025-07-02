using UnityEngine;

public class ArrowMover : MonoBehaviour
{
    [SerializeField] private float _minScale = 0.1f;
    [SerializeField] private float _maxScale = 0.7f;

    private float _deltaY;

    private int direction = 1;

    void Awake()
    {
        _deltaY = transform.localPosition.y;
    }

    void Update()
    {
        if (_deltaY < _maxScale && direction == 1)
        {
            _deltaY += Time.deltaTime;
        }
        else
        {
            direction = -1;
        }

        if (_deltaY > _minScale && direction == -1)
        {
            _deltaY -= Time.deltaTime;
        }
        else
        {
            direction = 1;
        }
        transform.localPosition = new Vector3(transform.localPosition.x, _deltaY, transform.localPosition.z);

    }
}