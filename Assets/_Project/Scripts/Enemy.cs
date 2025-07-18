using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _fovRadius;
    [SerializeField, Range(0, 360)] private float _angle;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;
    private Transform _player;
    private Transform _targetPosition;

    private bool _canSeePlayer;
    public float FovRadius => _fovRadius;
    public float Angle => _angle;
    public bool CanSeePlayer => _canSeePlayer;
    public Transform Player => _player;

    private void Start()
    {
        StartCoroutine(FovRoutine());
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
