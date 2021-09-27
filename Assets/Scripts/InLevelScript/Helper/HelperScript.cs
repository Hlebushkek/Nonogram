using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperScript : MonoBehaviour
{
    protected LevelScript level;
    protected SolutionScript solution;
    protected List<int> undefinedCells = new List<int>(); //Cells that are not used by helper
    protected int value = 0;
    protected bool direction; //false - width, true - height
    protected bool FullHelpWasApplyed = false;
    public void SetInfo(LevelScript l, SolutionScript ss,int v, bool d)
    {
        level = l;
        solution = ss;
        value = v;
        direction = d;
    }
    public virtual void GetHelp(){}
}
