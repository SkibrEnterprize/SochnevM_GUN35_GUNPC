using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private MeshRenderer _meshRendererFocus;
    private MeshRenderer _meshRendererSelect;
    private MeshRenderer _meshRendererAttack;
    private SignalBus _signalBus;

    [SerializeField] private float _raycastDistance = 2f;
    //private RaycastHit[] _hits;
    //[SerializeField] private int _angle = 45;
    //[SerializeField] private int _angleRight = 135;
    private Battlefield _battlefield;
    //private Cell _neighborsLeft;
    //private Cell _neighborsRight;



    private IPointClickEvent _pointClickEvent;
    [SerializeField] private Unit _currentUnit;
    public Unit CurrentUnit => _currentUnit;
    //public event Action<Cell> OnPointerClickEvent;
    [SerializeField] private bool _isSelected = false;
    public bool IsSelected => _isSelected;
    //[SerializeField] private Material _materialSelect;
    
    private CellState _state;
    public CellState State => _state;

    [Inject]
    public void Construct(
        IPointClickEvent pointClickEvent,
        Battlefield battlefield,
        SignalBus signalBus)
    {
        _battlefield = battlefield;
        _pointClickEvent = pointClickEvent;
        _signalBus = signalBus;
        //_signalBus.Subscribe<DebugSignal>(SignalTest);
    }

    private void Awake()
    {
        FindCurrentUnit();
        FindLinkMeshrendererInCild();
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
        _signalBus.Subscribe<PlayersMoveDone>(SetState);
    }
    private void OnDisable()
    {
        _signalBus.Unsubscribe<PlayersMoveDone>(FindCurrentUnit);
        _signalBus.Unsubscribe<PlayersMoveDone>(SetState);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click!!");
        FindCurrentUnit();
        _pointClickEvent.TriggerPointerClickEvent(this);
        if (!_isSelected && _currentUnit != null)
        {
            SetSelect();
            _battlefield.SelectNeighborsCheck(this);
            //CurrentUnit = FindCurrentUnit();
            //_battlefield.ResetSelectAll();                

        }
        else
        {
            //_neighborsLeft = null;
            //_neighborsRight = null;
            //_battlefield.FindNeighborsCheck(this);
            //_currentUnit.ChangeSelectedStatus();
            ResetSelect();
            _battlefield.ResetSelectAll();
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
        //_meshRendererSelect.material = _materialSelect;
        _isSelected = true;
    }

    public void ResetSelect()
    {
        //FindCurrentUnit();
        _meshRendererSelect.enabled = false;
        _meshRendererAttack.enabled = false;
        _isSelected = false;
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

    private void FindLinkMeshrendererInCild()
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

            else
            {
                Debug.Log("Not find any children whith MeshRenderer");
            }
        }
    }

    //public void ResetSelectAll()
    //{
    //    ResetSelect();
    //}
    //public void SetSelectAll()
    //{
    //    SetSelect();
    //}
    public void SignalTest()
    {
        Debug.Log("SIGNAL TEST!!!!!!!!!");
    }

    private void OnDestroy()
    {
        //_signalBus.Unsubscribe<DebugSignal>(SignalTest);
    }
    //void OnDrawGizmos()
    //{

    //    Vector3 origin = transform.position;
    //    //Vector3 direction = new Vector3(Mathf.Cos(_angle * Mathf.Deg2Rad), 0, Mathf.Sin(_angle * Mathf.Deg2Rad)); // Угол 45 градусов
    //    Vector3 direction = Vector3.up;

    //    Gizmos.color = Color.green;  // Цвет Raycast в редакторе
    //    Gizmos.DrawRay(origin, direction * _raycastDistance);
    //}
}
