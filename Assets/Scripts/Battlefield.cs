using System.Collections.Generic;
using Unity.VisualScripting;
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

    private void Awake()
    {
        Debug.Log("Battlefield ID is " + this.GetInstanceID());
    }


    public void SelectNeighborsCheck(Cell cell)
    {

        if (_currentCell == null)
        {
            _currentCell = cell;
            //_currentUnit = _currentCell.CurrentUnit;
        }
        else
        {
            _previousUnit = _currentUnit;            
            _previousUnit.ResetSelectedStatus();
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
            //_neighborsRight = FindNeighbors(cell, _angleRight);
            //if (_neighborsRight != null) _cellsForMove.Add(_neighborsRight);
            foreach (Cell cellNeighbors in _cellsForMove)
        {
            cellNeighbors.SetSelect();
            //_neighborsLeft?.SetSelect();
            //_neighborsRight?.SetSelect();
        }
    }

    private void FindCellInDirection(Cell cell, int angle, Cell neighbors)
    {
        //Team CellTeam = cell.gameObject.GetComponent<Unit>().Team;
        //Team neighborsTeam = neighbors.gameObject.GetComponent<Unit>().Team;
        neighbors = FindNeighbors(cell, angle); //ищем соседнюю клетку
        if (neighbors != null && neighbors.State == CellState.Empty) // если там она есть и на ней пусто
        {
            _cellsForMove.Add(neighbors); // добавляем в массив для подсветки
        }
        else if (neighbors != null && neighbors.State == CellState.Occupied) // если она есть и кем-то занята
        {
            if (neighbors.CurrentUnit.Team != _playerController.CurrentPlayingTeam) // проверяем кем - если не из нашей команды
            {
                neighbors.SetAttack();
                neighbors = FindNeighbors(neighbors, angle); // находим следующую за ним ячейку
                if (neighbors != null && neighbors.State == CellState.Empty)
                {
                    _cellsForMove.Add(neighbors); // если там пусто - можем туда пойти и Атаковать
                    //neighbors.State = CellState.Occupied;
                    // 
                    // Логика Атаки!!!
                    //
                }
                else return;
            }
        }
    }

    public void ResetSelectAll()
    {
        foreach (Cell cellNeighbors in _cellsForMove)
        {
            cellNeighbors.ResetSelect();
            //_neighborsLeft?.SetSelect();
            //_neighborsRight?.SetSelect();
        }
        //_currentCell.CurrentUnit.ChangeSelectedStatus();
        //_currentCell.ResetSelect();
        _cellsForMove.Clear();
        //_currentUnit.ChangeSelectedStatus();
        //_currentCell.ResetSelect();

    }
    public void FindNeighborsQween()
    { }
    public Cell FindNeighbors(Cell cell, int angle)
    {
        Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)); // Угол 45 градусов       
        Vector3 origin = cell.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, _raycastDistance))
        {
            Cell hitCell = hit.collider.GetComponent<Cell>();
            // Проверяем, что цель находится в зоне действия и является клеткой.
            if (hitCell != null)
            //if (hit.collider.gameObject.GetComponent<Cell>() != null)
            {
                //return hit.collider.gameObject.GetComponent<Cell>();
                return hitCell;
                //hit.collider.gameObject.GetComponent<Cell>().SetSelect();
            }
        }
        return null;
    }
}
