using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InLevelInputHandle : MonoBehaviour
{
    private static int InputMode = 1;
    private SolutionScript solution;
    private bool isSwiping = false, canClick = true;
    private CellState swipingMode;
    private NumBlockScript selectedNumBlock = null;
    private void Start()
    {
        solution = FindObjectOfType<SolutionScript>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            CastRay();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedNumBlock != null)
            {
                CastHelperRay();
                selectedNumBlock.HideButton();
                selectedNumBlock = null;
            }
            CastRay();
            isSwiping = false;
            canClick = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (InputMode == 1) InputMode = 0;
            else if (InputMode == 0) InputMode = 1;
        }
    }
    private void CastRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        CellScript cell;
        NumBlockScript numBlock;
        if (hit && hit.transform.TryGetComponent<CellScript>(out cell))
        {
            CellInfo cellInfo = cell.GetCell();
            if (!isSwiping)
            {
                isSwiping = true;
                swipingMode = cellInfo.state;
            }
            if (isSwiping && cellInfo.state == swipingMode)
            {
                solution.TakeChanges(cell.ApplyCellState(InputMode));
            }
        }
        else if (hit && hit.transform.TryGetComponent<NumBlockScript>(out numBlock) && canClick)
        {
            numBlock.ShowButton();
            selectedNumBlock = numBlock;
            canClick = false;
        }
    }
    private void CastHelperRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        HelperScript helper;
        if (hit && hit.transform.TryGetComponent<HelperScript>(out helper))
        {
            helper.GetHelp();
            canClick = false;
        }
    }
}
