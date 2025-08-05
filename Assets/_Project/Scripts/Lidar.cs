using System;
using UnityEngine;
using Zenject;

public class Lidar : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private bool _isDrawGizmos = true;
    private Ray _ray;
    private Vector3 _localForward;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    public bool IsRayCollision()
    {
        _localForward = transform.TransformDirection(Vector3.forward);
        _ray = new Ray(transform.position, _localForward * _distance);
        return Physics.Raycast(_ray, _distance, _layerMask);
    }

    void OnDrawGizmos()
    {
        IsRayCollision();
        if (_isDrawGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_ray.origin, _localForward * _distance);
        }
    }


}
