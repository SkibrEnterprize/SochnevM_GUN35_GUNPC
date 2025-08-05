using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 50f;

    void Update()
    {        
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }
}