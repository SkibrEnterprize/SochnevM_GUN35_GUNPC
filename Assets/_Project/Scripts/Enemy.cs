using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CharacterController))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _fovRadius;
    [SerializeField, Range(0, 360)] private float _angle;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _reachDistance = 2f;
    private Transform _player;
    private Transform _targetPosition;
    private CharacterController _characterController;
    private Vector3 _randomPosition;
    private bool _canSeePlayer;

    public float FovRadius => _fovRadius;
    public float Angle => _angle;
    public bool CanSeePlayer => _canSeePlayer;
    public Transform Player => _player;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        StartCoroutine(FovRoutine());
        GenerateRandomPosition();
    }

    private IEnumerator FovRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void Update()
    {
        if (_canSeePlayer)
        {
            Move(_player.position);
        }
        else
        {
            if (Vector3.Distance(transform.position, _randomPosition) <= _reachDistance)
            {
                GenerateRandomPosition();
            }
            Move(_randomPosition);
        }
    }
    private void Move(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction = direction.normalized;
        _characterController.Move(direction * _moveSpeed * Time.deltaTime);
        transform.LookAt(targetPosition);
    }
    private void GenerateRandomPosition()
    {
        float randomX = Random.Range(-20f, 20f);
        float randomZ = Random.Range(-20f, 20f);
        _randomPosition = new Vector3(randomX, transform.position.y, randomZ);

    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _fovRadius, _targetMask);

        if (rangeChecks.Length > 0)
        {
            _targetPosition = rangeChecks[0].transform;
            Vector3 directionToTarget = (_targetPosition.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, _targetPosition.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstacleMask))
                {
                    _canSeePlayer = true;
                    _player = rangeChecks[0].transform;
                }
                else
                {
                    _canSeePlayer = false;
                    _player = null;
                }
            }
            else
            {
                _canSeePlayer = false;
                _player = null;
            }
        }
        else if (_canSeePlayer)
        {
            _canSeePlayer = false;
            _player = null;
        }
    }
}
