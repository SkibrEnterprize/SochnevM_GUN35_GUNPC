using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{    
    private void OnEnable()
    {
        Cell.OnPointerClickEvent += HandleCellClicked;
    }

    private void OnDisable()
    {
        Cell.OnPointerClickEvent -= HandleCellClicked;
    }

    private void HandleCellClicked(Cell cell)
    {
        Debug.Log("Click!!!");
    }
}
