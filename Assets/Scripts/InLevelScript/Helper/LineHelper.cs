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
            if (direction == false)
            {
                cell = level.cellObjArray[i, value].GetComponent<CellScript>();
                cell.ApplyCellStateFromHelper(solution.solution[i, value]);
                solution.ApplyCorrectValueForCell(i, value);
            }
            else
            {
                cell = level.cellObjArray[value, i].GetComponent<CellScript>();
                cell.ApplyCellStateFromHelper(solution.solution[value, i]);
                solution.ApplyCorrectValueForCell(value, i);
            }
        }
        undefinedCells.Clear();
        FullHelpWasApplyed = true;
    }
}
