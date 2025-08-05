using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class CleanerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _timeChangeRotation = 5f;
    [SerializeField] private Lidar _lidarLeft;
    [SerializeField] private Lidar _lidarCenter;
    [SerializeField] private Lidar _lidarRight;
    [SerializeField] private LayerMask _obstacleLayer;
    private Vector3 _targetPosition;


    private Rigidbody _rb;
    private bool _isMovingForward = true;
    private SignalBus _signalBus;   

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(ChangeRotation(_timeChangeRotation));
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
            RotateObject(Quaternion.Euler(0, RandomRotate(), 0));
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

    private IEnumerator ChangeRotation(float timeChangeRotation)
    {
        while (true)
        {
            RotateObject(Quaternion.Euler(0, RandomRotate(), 0));
            yield return new WaitForSeconds(timeChangeRotation);
        }
    }
    private int RandomRotate()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Dirt>(out Dirt dirt)) _signalBus.Fire<DirtCollected>();
    }
}


