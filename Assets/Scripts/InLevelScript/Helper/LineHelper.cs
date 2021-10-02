using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHelper : HelperScript
{
    public override void GetHelp()
    {
        if (FullHelpWasApplyed) return;

        for (int i = 0; i < length; i++)
        {
            CellScript cell;
            CellState state;
            if (direction == Direction.Horizontal)
            {
                cell = level.GetCell(i, value);
                state = solution.GetSolutionForCell(i, value);
                if (state == CellState.Empty) state = CellState.Crossed;
                cell.ApplyCellStateFromHelper(state);
                solution.ApplyCorrectValueForCell(i, value);
            }
            else
            {
                cell = level.GetCell(value, i);
                state = solution.GetSolutionForCell(value, i);
                if (state == CellState.Empty) state = CellState.Crossed;
                cell.ApplyCellStateFromHelper(state);
                solution.ApplyCorrectValueForCell(value, i);
            }
        }
        undefinedCells.Clear();
        FullHelpWasApplyed = true;
    }
}
