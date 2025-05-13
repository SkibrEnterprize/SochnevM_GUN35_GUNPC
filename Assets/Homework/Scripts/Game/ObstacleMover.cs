using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObstacleMover : MonoBehaviour
{
    [SerializeField]
    private Vector3 _start = new Vector3(0, 0, 0);
    [SerializeField]
    private Vector3 _end = new Vector3(10f, 0, 0);
    [SerializeField, Min(0.1f)]
    private float _speed = 5f;
    [SerializeField, Min(0.1f)]
    private float _delay = 1f;

    private Rigidbody _rb;
    private Vector3 _newPosition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _start = transform.position;
    }
    private IEnumerator Start()
    {
        while (true)
        {
            _newPosition = Vector3.MoveTowards(transform.position, _end, _speed);

            if (_newPosition == _end)
            {
                yield return new WaitForSecondsRealtime(_delay);
                Vector3 temp = _start;
                _start = _end;
                _end = temp;
            }
            _rb.MovePosition(_newPosition);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_start, _end);
        Gizmos.DrawSphere(_start, 0.5f);
        Gizmos.DrawSphere(_end, 0.5f);
    }
}


