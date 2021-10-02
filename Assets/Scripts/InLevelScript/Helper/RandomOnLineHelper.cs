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

        if (direction == Direction.Horizontal)
        {
            cell = level.GetCell(cellNum, value);
            state = solution.GetSolutionForCell(cellNum, value);
            if (state == CellState.Empty) state = CellState.Crossed;
            cell.ApplyCellStateFromHelper(state);
            solution.ApplyCorrectValueForCell(cellNum, value);
        }
        else
        {
            cell = level.GetCell(value, cellNum);
            state = solution.GetSolutionForCell(value, cellNum);
            if (state == CellState.Empty) state = CellState.Crossed;
            cell.ApplyCellStateFromHelper(state);
            solution.ApplyCorrectValueForCell(value, cellNum);
        }
    }
    private void ReloadUndefinedCellList()
    {
        undefinedCells.Clear();
        if (direction == Direction.Horizontal)
        {
            for (int i = 0; i < length; i++)
            {
                if (level.GetCell(i, value).GetCanBeChanged())
                    undefinedCells.Add(i);
            }
        }
        else
        {
            for (int i = 0; i < length; i++)
            {
                if (level.GetCell(value, i).GetCanBeChanged())
                    undefinedCells.Add(i);
            }
        }
    }
}
