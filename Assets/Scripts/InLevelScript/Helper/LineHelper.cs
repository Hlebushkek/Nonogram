using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHelper : HelperScript
{
    public override void GetHelp()
    {
        if (FullHelpWasApplyed) return;

        int n = 0;
        if (direction == false) n = level.cellObjArray.GetLength(0);
        else n = level.cellObjArray.GetLength(1);
        for (int i = 0; i < n; i++)
        {
            CellScript cell;
            CellState state;
            if (direction == false)
            {
                cell = level.cellObjArray[i, value];
                state = solution.solution[i, value];
                if (state == CellState.Empty) state = CellState.Crossed;
                cell.ApplyCellStateFromHelper(state);
                solution.ApplyCorrectValueForCell(i, value);
            }
            else
            {
                cell = level.cellObjArray[value, i];
                state = solution.solution[value, i];
                if (state == CellState.Empty) state = CellState.Crossed;
                cell.ApplyCellStateFromHelper(state);
                solution.ApplyCorrectValueForCell(value, i);
            }
        }
        undefinedCells.Clear();
        FullHelpWasApplyed = true;
    }
}
