using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class LaserReceiver : MonoBehaviour
{
    public event Action<LaserReceiver> OnActivated;

    [Header("Color requirement")]
    [SerializeField] private ColorType _requiredColorPreset = ColorType.Red;
    [SerializeField] private float _activationRadius = 0.5f;

    [Header("Visual feedback")]
    [SerializeField] private Renderer _meshRenderer;
    [SerializeField] private Material _inactiveMaterial;
    [SerializeField] private Material _activeMaterial;

    private void Awake()
    {
        if (!TryGetComponent<Collider>(out var col))
            Debug.LogError($"{name} – нужен Collider для LaserReceiver", this);

        if (_meshRenderer != null && _inactiveMaterial == null)
            _inactiveMaterial = _meshRenderer.material;
        SetVisualState(false);
    }

    public void Activate(Color incomingColor)
    {
        Color required = _requiredColorPreset.GetColor();

        if (incomingColor.Equals(required))
        {
            SetVisualState(true);
            OnActivated?.Invoke(this);
        }
    }

    private void SetVisualState(bool active)
    {
        if (_meshRenderer == null) return;

        _meshRenderer.material = active ? _activeMaterial : _inactiveMaterial;
    }

    [ContextMenu("Reset")]
    public void ResetReceiver()
    {
        SetVisualState(false);
    }
    //[Header("Color requirement")]
    //[SerializeField] private ColorConstant _requiredColorPreset = ColorConstant.Red;
    //[SerializeField] private float _activationRadius = 0.5f;

    //[Header("Visual feedback")]
    //[SerializeField] private Renderer _meshRenderer;
    //[SerializeField] private Material _inactiveMaterial;
    //[SerializeField] private Material _activeMaterial;

    //private bool _isActive = false;
    //public bool IsActive => _isActive;

    //private void Awake()
    //{
    //    if (!TryGetComponent<Collider>(out var col))
    //        Debug.LogError($"{name} – нужен Collider для LaserReceiver", this);

    //    if (_meshRenderer != null && _inactiveMaterial == null)        
    //        _inactiveMaterial = _meshRenderer.material;
    //    SetVisualState(false);
    //}

    //public void Activate(Color incomingColor)
    //{
    //    if (_isActive) return; 

    //    var required = GetColorFromPreset(_requiredColorPreset);

    //    if (incomingColor.Equals(required))
    //    {
    //        _isActive = true;
    //        SetVisualState(true);
    //        LaserManager.Instance.NotifyReceiverActivated(this); 
    //    }
    //}

    //private void SetVisualState(bool active)
    //{
    //    if (_meshRenderer == null) return;

    //    _meshRenderer.material = active ? _activeMaterial : _inactiveMaterial;
    //}

    //[ContextMenu("Reset")]
    //public void ResetReceiver()
    //{
    //    _isActive = false;
    //    SetVisualState(false);
    //}

    //private static Color GetColorFromPreset(ColorConstant preset)
    //{
    //    return preset switch
    //    {
    //        ColorConstant.Red => Color.red,
    //        ColorConstant.Yellow => Color.yellow,
    //        ColorConstant.Blue => Color.blue,            
    //        _ => Color.white
    //    };
    //}

}
