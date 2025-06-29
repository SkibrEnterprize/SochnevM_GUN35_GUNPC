using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private IPointClickEvent _pointClickEvent;
    private Battlefield _battlefield;
    private SignalBus _signalBus;
    [SerializeField] private Team _team = Team.White;
    [SerializeField] private UnitType _unitType = UnitType.Check;
    [SerializeField] private bool _isSelected = false;
    [SerializeField] private float _moveSpeed = 5f;
    public Team Team => _team;
    public UnitType UnitType => _unitType;
    private float _raycastDistance = 2f;
    private Cell _currentCell;
    private Cell _targetCell;
    private PlayerController _playerController;

    [Inject]
    public void Construct(
        IPointClickEvent pointClickEvent,
        Battlefield battlefield,
       SignalBus signalBus,
       PlayerController playerController
        )
    {
        _pointClickEvent = pointClickEvent;
        _battlefield = battlefield;
        _signalBus = signalBus;
        _playerController = playerController;
    }

    private void Awake()
    {
        FindCurrentCell();
    }
    private void OnEnable()
    {
        _pointClickEvent.OnPointerClickEvent += TakeTargetCell;
        _signalBus.Subscribe<SelectConfirm>(Move);
        _signalBus.Subscribe<SelectCancel>(CancelEvent);

    }

    public void CancelEvent()
    {
        _isSelected = false;
        _targetCell = null;
    }

    private void OnDisable()
    {
        _pointClickEvent.OnPointerClickEvent -= TakeTargetCell;
        _signalBus.Unsubscribe<SelectConfirm>(Move);
        _signalBus.Unsubscribe<SelectCancel>(CancelEvent);
    }
    //private void Update()
    //{
    //    if (_targetCell != null && _isSelected)
    //    {
    //        Move();
    //    }
    //}
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_playerController.CurrentPlayingTeam == _team)
        {
            _currentCell?.OnPointerClick(eventData);
            if (_isSelected)
            {
                ResetSelectedStatus();
            }
            else
            {
                SetSelectedStatus();
            }
        }
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
        if (_targetCell != null && _isSelected && _targetCell.CurrentUnit == null)
        {
            _signalBus.Fire<PlayersMove>();
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
        ResetSelectedStatus();
        _currentCell = _targetCell;
        _targetCell = null;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            float distCovered = (Time.time - startTime) * _moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            yield return null;
        }
        transform.position = targetPosition;
        _signalBus.Fire<PlayersMoveDone>();

    }

    private void TakeTargetCell(Cell cell)
    {
        if (_currentCell != cell && _isSelected && cell.IsSelected)
        {
            _targetCell = cell;
        }
    }
    public void ResetTargetCell() => _targetCell = null;
    public void SetSelectedStatus() => _isSelected = true;
    public void ResetSelectedStatus() => _isSelected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Unit>(out Unit unit) && unit.Team != _playerController.CurrentPlayingTeam)
        {
            Debug.Log("TriggerCustomEvent!!");
            Destroy(other.gameObject);
        }
    }
}
