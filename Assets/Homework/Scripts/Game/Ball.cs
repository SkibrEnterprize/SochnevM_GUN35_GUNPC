using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude = 50f;

    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<ObstacleColorChanger>())
        {
            Vector3 direction = (other.transform.position - transform.position).normalized;
            _rb.AddForce(direction * forceMagnitude, ForceMode.Impulse);
        }        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Gates>())
        {
            Destroy(gameObject);
        }
    }
}
