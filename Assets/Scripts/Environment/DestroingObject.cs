using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroingObject : MonoBehaviour
{


    [Header("Physics")]
    [SerializeField, Range(0f, 10f)]
    private float _explosionForce = 5f;

    [SerializeField]
    private float _radius = 2f;


    private Transform _transform;

    void Awake() => _transform = transform;

    [ContextMenu("Exploded")]
    public void Exploded()
    {
        var parentCollider = GetComponent<Collider>();
        if (parentCollider != null)
            parentCollider.enabled = false;
        var children = GetComponentsInChildren<Rigidbody>();

        foreach (var rb in children)
        {
            if (rb.gameObject == gameObject) continue;

            Vector3 dir = (rb.transform.position - _transform.position).normalized;

            dir += Random.insideUnitSphere * 0.5f;
            rb.isKinematic = false;

            rb.AddForce(dir * _explosionForce, ForceMode.VelocityChange);
        }
    }
}
