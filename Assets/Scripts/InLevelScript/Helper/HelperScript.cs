using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperScript : MonoBehaviour
{
    protected List<int> undefinedCells = new List<int>(); //Cells that are not used by helper
    protected Direction direction;
    protected LevelScript level;
    protected SolutionScript solution;
    protected int value = 0, length = 0;
    protected bool FullHelpWasApplyed = false;
    public void SetInfo(LevelScript lev, SolutionScript s, Direction d, int v, int l)
    {
        level = lev;
        solution = s;
        direction = d;
        value = v;
        length = l;
    }
    public virtual void GetHelp(){}
}
