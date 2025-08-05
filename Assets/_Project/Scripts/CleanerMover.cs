using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class CleanerMover : MonoBehaviour
{
    [SerializeField] private Lidar _lidarLeft;
    [SerializeField] private Lidar _lidarCenter;
    [SerializeField] private Lidar _lidarRight;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private LayerMask _obstacleLayer;

    private Rigidbody _rb;
    private bool _isMovingForward = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckForObstacles();
    }

    void FixedUpdate()
    {
        if (_isMovingForward)
            MoveForward();
        else
            RotateToAvoidObstacle();
    }

    private void CheckForObstacles()
    {
        if (_lidarCenter.IsRayCollision())
        {
            _isMovingForward = false;
            return;
        }
    }

    private void MoveForward()
    {
        _rb.MovePosition(transform.position + transform.forward * _moveSpeed * Time.fixedDeltaTime);
    }

    private void RotateToAvoidObstacle()
    {
        if (!_lidarLeft.IsRayCollision() && !_lidarRight.IsRayCollision())
        {
            //transform.Rotate(0, -90, 0);
            RotateObject(Quaternion.Euler(0, RandomY(), 0));
            _isMovingForward = true;
            return;
        }

        if (!_lidarLeft.IsRayCollision())
        {
            RotateObject(Quaternion.Euler(0, 90, 0));
            _isMovingForward = true;
            return;
        }

        if (!_lidarRight.IsRayCollision())
        {
            RotateObject(Quaternion.Euler(0, 90, 0));
            _isMovingForward = true;
            return;
        }

        if (!_lidarCenter.IsRayCollision())
        {
            _isMovingForward = true;
        }
    }

        private int RandomY()
    {
        int randomChoice = Random.Range(0, 2);

        if (randomChoice == 0)
        {
            return 90;
        }
        else
        {
            return -90;
        }
    }
    void RotateObject(Quaternion rotation) => _rb.MoveRotation(_rb.rotation * rotation);
    //[SerializeField] private float _speed = 5f;
    //[SerializeField] private Lidar _lidarLeft;
    //[SerializeField] private Lidar _lidarCenter;
    //[SerializeField] private Lidar _lidarRight;
    //private Rigidbody _rb;
    //[SerializeField] private Vector3 _moveDirection;
    //private SignalBus _signalBus;
    //[SerializeField] private Quaternion _targetRotation = Quaternion.Euler(0, 90, 0);
    //private float _rotationSpeed = 5f;

    //[Inject]
    //public void Construct(SignalBus signalBus)
    //{
    //    _signalBus = signalBus;
    //}

    //void Start()
    //{
    //    _rb = GetComponent<Rigidbody>();
    //    _moveDirection = transform.forward;
    //    _targetRotation = transform.rotation;
    //}
    //private void OnEnable()
    //{
    //    _signalBus.Subscribe<LidarDetected>(ChangeDirection);
    //}
    //private void OnDisable()
    //{
    //    _signalBus.Unsubscribe<LidarDetected>(ChangeDirection);
    //}

    //private void ChangeDirection()
    //{
    //    if (_lidarCenter.IsDetected && !_lidarLeft.IsDetected && !_lidarRight.IsDetected)
    //    {
    //        RotateObject(Quaternion.Euler(0,90,0));
    //    }
    //    else if(_lidarCenter.IsDetected && !_lidarLeft.IsDetected && _lidarRight.IsDetected)
    //    {
    //        RotateObject(Quaternion.Euler(0, -90, 0));
    //    }
    //    else if (_lidarCenter.IsDetected && _lidarLeft.IsDetected && !_lidarRight.IsDetected)
    //    {
    //        RotateObject(Quaternion.Euler(0, 90, 0));
    //    }
    //    else if (_lidarCenter.IsDetected && _lidarLeft.IsDetected && _lidarRight.IsDetected)
    //    {
    //        RotateObject(Quaternion.Euler(0, 180, 0));
    //    }
    //}

    //[ContextMenu("Rotate")]
    //void RotateObject(Quaternion rotation) => _rb.MoveRotation(_rb.rotation * rotation);

    //void FixedUpdate()
    //{
    //    Move();
    //}

    //private void Move()
    //{
    //    // Получаем направление, в котором объект смотрит
    //    Vector3 direction = transform.forward; // Вперед по локальной оси объекта
    //    // Вычисляем новую позицию
    //    Vector3 newPosition = _rb.position + direction * _speed * Time.fixedDeltaTime;
    //    // Перемещаем объект
    //    _rb.MovePosition(newPosition);
    //}
}


