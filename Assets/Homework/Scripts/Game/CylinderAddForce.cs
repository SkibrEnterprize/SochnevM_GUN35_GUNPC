using UnityEngine;

public class CylinderAddForce : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude = 50f;
    [SerializeField]
    private Color _goalColor = Color.green;    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Ball>() && other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            Vector3 direction = (other.transform.position - transform.position).normalized;
            rb.AddForce(direction * forceMagnitude, ForceMode.Impulse);
            GetComponent<Renderer>().material.color = _goalColor;            
            Invoke("ResetColor", 1f);
        }
    }
    void ResetColor()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}

