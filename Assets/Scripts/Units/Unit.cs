using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
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
    private GameObject _signOfAQween;

    [Inject]
    public void Construct(
       Battlefield battlefield,
       SignalBus signalBus,
       PlayerController playerController
       )
    {
        _signalBus = signalBus;
        _playerController = playerController;
    }

    private void Awake()
    {
        FindSignOfAQween();
        FindCurrentCell();
        if (_unitType == UnitType.Qween) TransformToQween();
    }
    private void OnEnable()
    {
        _signalBus.Subscribe<SelectInstall>(TakeTargetCell);
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
        _signalBus.Unsubscribe<SelectInstall>(TakeTargetCell);
        _signalBus.Unsubscribe<SelectConfirm>(Move);
        _signalBus.Unsubscribe<SelectCancel>(CancelEvent);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_playerController.CurrentPlayingTeam == _team)
        {
            ICommand command = new CellClickCommand(_currentCell);
            command.Execute(eventData);
            SetSelectedStatus();
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
        if (_targetCell != null && _isSelected && _targetCell.State == CellState.Empty)
        {
            StartCoroutine(MoveToTarget());
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

    private void TakeTargetCell(SelectInstall selectInstall)
    {
        if (_currentCell != selectInstall.Cell && _isSelected && selectInstall.Cell.IsSelected)
        {
            _targetCell = selectInstall.Cell;
        }
    }
    public void ResetTargetCell() => _targetCell = null;
    public void SetSelectedStatus() => _isSelected = true;
    public void ResetSelectedStatus() => _isSelected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Unit>(out Unit unit) && unit.Team != _playerController.CurrentPlayingTeam)
        {
            Destroy(other.gameObject);
        }
        else if (other.TryGetComponent<EndOfField>(out EndOfField endOfField) && _team == endOfField.EndOfTeamFor)
        {
            TransformToQween();
            Debug.Log("Is A Qween!!!");
        }
    }

    private void FindSignOfAQween()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<SignOfAQween>(out SignOfAQween signOfAQween))
            {
                _signOfAQween = child.gameObject;
            }
        }
    }
    private void TransformToQween()
    {
        _unitType = UnitType.Qween;
        _signOfAQween.SetActive(true);
    }
}
