using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOnLineHelper : HelperScript
{
    public override void GetHelp()
    {
        if (undefinedCells.Count <= 0) return;

        CellScript cell;
        int n = Random.Range(0, undefinedCells.Count);
        int cellNum = undefinedCells[n];
        undefinedCells.RemoveAt(n);

        if (direction == false)
        {
            cell = level.cellObjArray[cellNum, value].GetComponent<CellScript>();
            cell.ApplyCellStateFromHelper(solution.solution[cellNum, value]);
            solution.ApplyCorrectValueForCell(cellNum, value);
        }
        else
        {
            cell = level.cellObjArray[value, cellNum].GetComponent<CellScript>();
            cell.ApplyCellStateFromHelper(solution.solution[value, cellNum]);
            solution.ApplyCorrectValueForCell(value, cellNum);
        }
    }
}
