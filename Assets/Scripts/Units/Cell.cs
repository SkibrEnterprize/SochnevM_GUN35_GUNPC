using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private MeshRenderer _meshRendererFocus;
    private MeshRenderer _meshRendererSelect;
    private MeshRenderer _meshRendererAttack;
    private GameObject _arrow;
    private SignalBus _signalBus;

    private float _raycastDistance = 2f;
    private Battlefield _battlefield;
    private Unit _currentUnit;
    public Unit CurrentUnit => _currentUnit;
    private bool _isSelected = false;
    public bool IsSelected => _isSelected;

    private CellState _state;
    public CellState State => _state;   

    [Inject]
    public void Construct(
        //IPointClickEvent pointClickEvent,
        Battlefield battlefield,
        SignalBus signalBus)
    {
        _battlefield = battlefield;
        //_pointClickEvent = pointClickEvent;
        _signalBus = signalBus;
    }

    private void Awake()
    {
        FindCurrentUnit();
        FindLinksInCild();
        SetState();

    }

    public void SetState()
    {
        if (_currentUnit != null) { _state = CellState.Occupied; }
        else if (_currentUnit == null) { _state = CellState.Empty; }
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<PlayersMoveDone>(FindCurrentUnit);
        _signalBus.Subscribe<PlayersMoveDone>(CancelEvent);
        _signalBus.Subscribe<PlayersMoveDone>(SetState);
        _signalBus.Subscribe<SelectCancel>(CancelEvent);
    }
    private void OnDisable()
    {
        _signalBus.Unsubscribe<PlayersMoveDone>(FindCurrentUnit);
        _signalBus.Unsubscribe<PlayersMoveDone>(CancelEvent);
        _signalBus.Unsubscribe<PlayersMoveDone>(SetState);
        _signalBus.Unsubscribe<SelectCancel>(CancelEvent);
    }

    private void CancelEvent()
    {
        ResetSelect();
        _battlefield.ResetSelectAll();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        FindCurrentUnit();
        _signalBus.Fire(new SelectInstall(this));
        if (!_isSelected && _currentUnit != null)
        {
            _currentUnit.SetSelectedStatus();
            _battlefield.ResetSelectAll();
            SetSelect();
            _battlefield.SelectNeighborsCheck(this);
        }
        else if (_isSelected && _currentUnit == null)
        {
            _battlefield.ResetNeighborsArrow();
            _arrow.SetActive(true);
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
    public void SetSelect()
    {
        _meshRendererSelect.enabled = true;
        if (_isSelected)
        {
            _arrow.SetActive(true);
        }
        else
        {
            _isSelected = true;
        }
    }

    public void ResetSelect()
    {
        _meshRendererSelect.enabled = false;
        _meshRendererAttack.enabled = false;
        _isSelected = false;
        _arrow.SetActive(false);
    }

    public void SetAttack() => _meshRendererAttack.enabled = true;
    public void ResetAttack() => _meshRendererAttack.enabled = false;


    [ContextMenu("FindCurrentUnit")]
    private void FindCurrentUnit()
    {
        if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, _raycastDistance))
        {
            Unit unit = hit.collider.GetComponent<Unit>();
            if (unit == null)
            {
                _currentUnit = null;
            }
            else
            {
                _currentUnit = hit.collider.gameObject.GetComponent<Unit>();
            }
        }
        else
        {
            _currentUnit = null;
        }
    }

    private void FindLinksInCild()
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
                child.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer);
                _meshRendererSelect = meshRenderer;
            }
            else if (child != null && child.TryGetComponent<Attack>(out Attack attack))
            {
                child.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer);
                _meshRendererAttack = meshRenderer;
            }
            else if (child.TryGetComponent<ArrowMover>(out ArrowMover arrowMover))
            {
                _arrow = child.GameObject();
            }
        }
    }

    public void ResetArrow() => _arrow.SetActive(false);

    public void SignalTest()
    {
        Debug.Log("SIGNAL TEST!!!!!!!!!");
    }
}
