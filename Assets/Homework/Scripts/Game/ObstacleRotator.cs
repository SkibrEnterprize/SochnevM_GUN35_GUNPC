using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObstacleRotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotate;
    private Rigidbody _rigidbody;
    private IEnumerator Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        while (true)
        {
            Quaternion rotation = Quaternion.Euler(_rotate.x*Time.deltaTime, _rotate.y*Time.deltaTime, _rotate.z*Time.deltaTime);
            _rigidbody.MoveRotation(_rigidbody.rotation * rotation);
            yield return new WaitForFixedUpdate();
        }
    }
}
