using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private IPointClickEvent _pointClickEvent;
    private Battlefield _battlefield;
    //private IGameEvent _onPlayersMoveEvent;
    //private IGameEvent _onPlayersMoveDoneEvent;
    private SignalBus _signalBus;

    [SerializeField]
    private float _moveSpeed = 5f;    
    private float _raycastDistance = 2f;
    private Cell _currentCell;
    private Cell _targetCell;
    private bool _isSelected = false;

    [Inject]
    public void Construct(
        IPointClickEvent pointClickEvent,
        Battlefield battlefield,
       SignalBus signalBus)
    {
        _pointClickEvent = pointClickEvent;
        _battlefield = battlefield;
        _signalBus = signalBus;
        
    }

    private void Awake()
    {
        FindCurrentCell();
    }
    private void OnEnable()
    {
        _pointClickEvent.OnPointerClickEvent += TakeTargetCell;
    }
    private void OnDisable()
    {
        _pointClickEvent.OnPointerClickEvent -= TakeTargetCell;
    }
    private void Update()
    {
        if (_targetCell != null && _isSelected)
        {                        
            Move();            
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _currentCell?.OnPointerClick(eventData);
        ChangeSelectedStatus();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _currentCell?.OnPointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _currentCell?.OnPointerExit(eventData);
    }

    private void FindCurrentCell()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _raycastDistance))
        {
            if (hit.collider.gameObject.GetComponent<Cell>() != null)
            {
                _currentCell = hit.collider.gameObject.GetComponent<Cell>();
            }
        }
    }
    public void Move()
    {
        if (_targetCell.CurrentUnit == null)
        {
            _signalBus.Fire<OnPlayersMoveSignal>();
            //_onPlayersMoveEvent.TriggerEvent();
            StartCoroutine(MoveToTarget());
            
        } 
        else
        {
            Debug.Log("Cell is not empty!");
        }
    }

    IEnumerator MoveToTarget()
    {
        _currentCell.ResetSelect();
        Vector3 offset = _currentCell.transform.position - transform.position;
        Vector3 startPosition = _currentCell.transform.position - offset;
        Vector3 targetPosition = _targetCell.transform.position - offset;
        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;
        ChangeSelectedStatus();
        _currentCell = _targetCell;        
        _targetCell = null;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            float distCovered = (Time.time - startTime) * _moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            yield return null;
        }
            _signalBus.Fire<OnPlayersMoveDoneSignal>();
        transform.position = targetPosition;
        //_currentCell.ResetSelect();
        //_onPlayersMoveDoneEvent.TriggerEvent();
        
    }

    private void TakeTargetCell(Cell cell)
    {
        Debug.Log("Try Take Target Cell");
        if (_currentCell != cell && _isSelected && cell.IsSelected)
        {
            _targetCell = cell;
        }
    }

    private void ChangeSelectedStatus()
    {
        _isSelected = !_isSelected;
    }

    void OnDrawGizmos()
    {

        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;

        Gizmos.color = Color.red;  // Цвет Raycast в редакторе
        Gizmos.DrawRay(origin, direction * _raycastDistance);
    }
}
