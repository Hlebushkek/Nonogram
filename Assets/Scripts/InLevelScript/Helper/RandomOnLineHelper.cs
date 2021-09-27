using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOnLineHelper : HelperScript
{
    public override void GetHelp()
    {
        ReloadUndefinedCellList();
        if (undefinedCells.Count <= 0) return;

        CellScript cell;
        CellState state;
        int n = Random.Range(0, undefinedCells.Count);
        int cellNum = undefinedCells[n];

        if (direction == false)
        {
            cell = level.cellObjArray[cellNum, value];
            state = solution.solution[cellNum, value];
            if (state == CellState.Empty) state = CellState.Crossed;
            cell.ApplyCellStateFromHelper(state);
            solution.ApplyCorrectValueForCell(cellNum, value);
        }
        else
        {
            cell = level.cellObjArray[value, cellNum];
            state = solution.solution[value, cellNum];
            if (state == CellState.Empty) state = CellState.Crossed;
            cell.ApplyCellStateFromHelper(state);
            solution.ApplyCorrectValueForCell(value, cellNum);
        }
    }
    private void ReloadUndefinedCellList()
    {
        undefinedCells.Clear();
        int length;
        if (direction == false)
        {
            length = level.cellObjArray.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                if (level.cellObjArray[i, value].GetCanBeChanged())
                    undefinedCells.Add(i);
            }
        }
        else
        {
            length = level.cellObjArray.GetLength(1);
            for (int i = 0; i < length; i++)
            {
                if (level.cellObjArray[value, i].GetCanBeChanged())
                    undefinedCells.Add(i);
            }
        }
    }
}
