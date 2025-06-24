using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
    private List<Cell> _cellsForMove = new List<Cell>();
    private int _raycastDistance = 2;
    private int _angleLeft = 135;
    private int _angleRight = 45;

    private Cell _currentCell;
    private Cell _neighborsLeft;
    private Cell _neighborsRight;

    public void SelectNeighborsCheck(Cell cell)
    {
        //_currentCell = cell;
        FindCellInDirection(cell, _angleLeft, _neighborsLeft);
        FindCellInDirection(cell, _angleRight, _neighborsRight);
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
        neighbors = FindNeighbors(cell, angle);
        if (neighbors != null && neighbors.CurrentUnit == null)
        {
            _cellsForMove.Add(neighbors);
        }
        else if (neighbors != null && neighbors.CurrentUnit != null)
        {
            neighbors = FindNeighbors(neighbors, angle);
            if (neighbors != null) _cellsForMove.Add(neighbors);
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
        _cellsForMove.Clear();
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
                Debug.Log($"Name of hit {hit.collider.gameObject.name}");
                //return hit.collider.gameObject.GetComponent<Cell>();
                return hitCell;
                //hit.collider.gameObject.GetComponent<Cell>().SetSelect();
            }
        }
        return null;
    }
}
