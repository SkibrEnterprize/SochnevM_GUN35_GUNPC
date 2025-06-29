using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Battlefield : MonoBehaviour
{
    private List<Cell> _cellsForMove = new List<Cell>();
    private int _raycastDistance = 2;
    private int _angleLeftWhite = 135;
    private int _angleLeftBlack = 225;
    private int _angleRightWhite = 45;
    private int _angleRightBlack = 315;
    private SignalBus _signalBus;
    private PlayerController _playerController;

    private Cell _currentCell;
    private Cell _attackCell;
    private Cell _neighborsLeft;
    private Cell _neighborsRight;
    private Unit _currentUnit;
    private Unit _previousUnit;

    [Inject]
    private void Construct(
        SignalBus signalBus,
        PlayerController playerController)
    {
        _signalBus = signalBus;
        _playerController = playerController;
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<PlayersMoveDone>(ResetSelectAll);
    }
    private void OnDisable()
    {
        _signalBus.Unsubscribe<PlayersMoveDone>(ResetSelectAll);        
    }
    public void SelectNeighborsCheck(Cell cell)
    {
        if (_currentCell == null)
        {
            _currentCell = cell;
        }
        else
        {            
            _previousUnit = _currentUnit;
            _previousUnit.CancelEvent();
            //_previousUnit.ResetSelectedStatus();
            _currentCell.ResetSelect();
            ResetSelectAll();
            _currentCell = cell;
        }

        _currentUnit = cell.CurrentUnit;
        if (_currentUnit.Team == Team.White)
        {
            FindCellInDirection(cell, _angleRightWhite, _neighborsLeft);
            FindCellInDirection(cell, _angleLeftWhite, _neighborsRight);
        }
        else
        {
            FindCellInDirection(cell, _angleRightBlack, _neighborsLeft);
            FindCellInDirection(cell, _angleLeftBlack, _neighborsRight);
        }
        foreach (Cell cellNeighbors in _cellsForMove)
        {
            cellNeighbors.SetSelect();
        }
    }

    private void FindCellInDirection(Cell cell, int angle, Cell neighbors)
    {
        switch (cell.CurrentUnit.UnitType)
        {
            case UnitType.Check:
                neighbors = FindNeighbors(cell, angle);
                if (neighbors != null && neighbors.State == CellState.Empty)
                {
                    _cellsForMove.Add(neighbors);
                }
                else if (neighbors != null && neighbors.State == CellState.Occupied)
                {
                    if (neighbors.CurrentUnit.Team != _playerController.CurrentPlayingTeam)
                    {
                        _attackCell = neighbors;
                        neighbors = FindNeighbors(neighbors, angle);
                        if (neighbors != null && neighbors.State == CellState.Empty)
                        {
                            _cellsForMove.Add(neighbors);
                            _attackCell.SetAttack();
                        }
                        else return;
                    }
                }
                break;
            case UnitType.Qween:
                break;
        }
    }

    public void ResetNeighborsArrow()
    {
        foreach (Cell cellNeighbors in _cellsForMove)
        {
            cellNeighbors.ResetArrow();
        }
        //_cellsForMove.Clear();
    }
    public void ResetSelectAll()
    {
        foreach (Cell cellNeighbors in _cellsForMove)
        {
            cellNeighbors.ResetSelect();
        }
        _cellsForMove.Clear();
        _attackCell?.ResetAttack();
    }
    public void FindNeighborsQween()
    { }
    public Cell FindNeighbors(Cell cell, int angle)
    {
        Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));
        Vector3 origin = cell.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, _raycastDistance))
        {
            Cell hitCell = hit.collider.GetComponent<Cell>();
            if (hitCell != null)
            {
                return hitCell;
            }
        }
        return null;
    }
}
