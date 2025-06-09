using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private MeshRenderer _meshRendererFocus;
    private MeshRenderer _meshRendererSelect;

    public Unit Unit { get; set; }
    public bool IsEmpty => Unit == null;
    public static Action<Cell> OnPointerClickEvent;
    private bool _isSelected = false;
    public bool IsSelected => _isSelected;
    [SerializeField]
    private Material _materialSelect;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Focus>(out Focus focus))
            {
                child.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer);
                _meshRendererFocus = meshRenderer;
            }
            else if (child != null && child.TryGetComponent<Select>(out Select select))
            {
                child.TryGetComponent<MeshRenderer>(out MeshRenderer _meshRenderer);
                _meshRendererSelect = _meshRenderer;
            }
            else
            {
                Debug.Log("Not find any children whith MeshRenderer");
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickEvent?.Invoke(this);
        if (!_isSelected)
        {
            SetSelect(_materialSelect);            
        }
        else
        {
            ResetSelect();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _meshRendererFocus.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _meshRendererFocus.enabled = false;
    }
    public void SetSelect(Material material)
    {
        _meshRendererSelect.enabled = true;
        _meshRendererSelect.material = material;
        _isSelected = true;
    }

    public void ResetSelect()
    {
        _meshRendererSelect.enabled = false;
        _isSelected = false;
    }
    
}
