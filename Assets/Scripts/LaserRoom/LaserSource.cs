using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(LineRenderer))]
public sealed class LaserSource : MonoBehaviour
{
    [Header("Laser settings")]
    [SerializeField] private ColorConstant _colorPreset = ColorConstant.Red;
    [SerializeField] private float _range = 100f;
    [SerializeField] private LayerMask _rayLayers;
    [SerializeField] private int _maxBounces = 10;

    [Header("Line renderer")]
    [SerializeField] private LineRenderer _lineRenderer;

    [Header("Gizmos")]
    [SerializeField, Tooltip("Показывать ли путь луча в редакторе?")]
    private bool _showPath = true;

    private Color _laserColor;        

    private void Awake()
    {
        if (_lineRenderer == null)
            _lineRenderer = GetComponent<LineRenderer>();

        _laserColor = GetColorFromPreset(_colorPreset);

        _lineRenderer.startColor = _laserColor;
        _lineRenderer.endColor = _laserColor;
        _lineRenderer.positionCount = 0;
    }

    private static Color GetColorFromPreset(ColorConstant preset)
    {
        return preset switch
        {
            ColorConstant.Red => Color.red,
            ColorConstant.Yellow => Color.yellow,
            ColorConstant.Blue => Color.blue,
            _ => Color.white
        };
    }

    private void Update()
    {
        if (_laserColor != GetColorFromPreset(_colorPreset))
        {
            _laserColor = GetColorFromPreset(_colorPreset);
            _lineRenderer.startColor = _laserColor;
            _lineRenderer.endColor = _laserColor;
        }

        Emit();
    }

    public bool Emit()
    {
        var origin = transform.position;
        var direction = transform.forward;

        var points = new List<Vector3> { origin };

        for (int i = 0; i < _maxBounces; ++i)
        {
            if (!Physics.Raycast(origin, direction, out RaycastHit hit,
                                 _range, _rayLayers))
                break;

            origin = hit.point;
            points.Add(origin);

            if (hit.collider.TryGetComponent<LaserReceiver>(out var receiver))
            {
                receiver.Activate(_laserColor);
                break;
            }

            if (hit.collider.TryGetComponent<LaserMirror>(out var mirror))
            {
                direction = Vector3.Reflect(direction, hit.normal);
                continue;
            }

            break;
        }

        _lineRenderer.positionCount = points.Count;
        _lineRenderer.SetPositions(points.ToArray());
        return true;
    }
    private void OnDrawGizmos()
    {
        if (!_showPath) return;          
               

        // Путь луча
        var points = new List<Vector3> { transform.position };
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        const int _maxBounces = 10;          // чтобы не зациклиться

        for (int i = 0; i < _maxBounces; ++i)
        {
            if (!Physics.Raycast(origin, direction, out RaycastHit hit,
                                 _range, _rayLayers))
                break;

            origin = hit.point;
            points.Add(origin);

            if (hit.collider.TryGetComponent<LaserMirror>(out var mirror))
            {
                direction = Vector3.Reflect(direction, hit.normal);
                continue;
            }

            break;
        }

        Gizmos.color = Color.cyan;   
        for (int i = 0; i < points.Count - 1; ++i)
            Gizmos.DrawLine(points[i], points[i + 1]);        
    }
}

