using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRaycast : MonoBehaviour
{
    [SerializeField] private float _raycastDistance;
    private RaycastHit[] _hits;
    [SerializeField] private int _angle = 45;

    private Cell _cell;
    private void Awake()
    {
        _cell = GetComponent<Cell>();
    }
    [ContextMenu("Raycast hit log")]
    public void SeeTarget()
    {
        RaycastHit hit;
        Vector3 direction = direction = new Vector3(Mathf.Cos(_angle * Mathf.Deg2Rad), 0, Mathf.Sin(_angle * Mathf.Deg2Rad)); // ”гол 45 градусов
        Vector3 origin = transform.position;
        //Ray ray = new Ray(origin, direction);
        //_hits = Physics.RaycastAll(ray, _raycastDistance);

        //foreach (RaycastHit hit in _hits)
        //{
        //    Debug.Log($"Name of hit {hit.collider.gameObject.name}");
        //}

        if (Physics.Raycast(origin, direction, out hit, _raycastDistance))
        {
            // ѕровер€ем, что цель находитс€ в зоне действи€ и €вл€етс€ клеткой.
            if (hit.collider.gameObject.GetComponent<Cell>() != null)
            {
                Debug.Log($"Name of hit {hit.collider.gameObject.name}");
                _cell.SetSelect();
            }
        }
    }
    void OnDrawGizmos()
    {
        Vector3 origin = transform.position;

        // ¬ычисл€ем направление под углом 45 градусов относительно горизонтальной оси пол€.
        Vector3 direction = new Vector3(Mathf.Cos(_angle * Mathf.Deg2Rad), 0, Mathf.Sin(_angle * Mathf.Deg2Rad));

        Gizmos.color = Color.red;  // ÷вет Raycast в редакторе
        Gizmos.DrawRay(origin, direction * _raycastDistance);
    }
}
