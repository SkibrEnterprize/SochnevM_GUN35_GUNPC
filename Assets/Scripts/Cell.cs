using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private MeshRenderer _meshRendererFocus;
    private MeshRenderer _meshRendererSelect;
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child != null && child.TryGetComponent<Focus>(out Focus focus))
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
        _meshRendererFocus.enabled = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _meshRendererFocus.enabled = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _meshRendererFocus.enabled = true;

    }
}
