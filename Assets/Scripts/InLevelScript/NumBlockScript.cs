using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumBlockScript : MonoBehaviour
{
    private GameObject FullLineHelper, RandomOnLineHelper;
    private LevelScript level;
    private SolutionScript solution;
    private int value = 0;
    private bool direction; //false - width, true - height
    public void SetInfo(LevelScript l, SolutionScript ss,int v, bool d)
    {
        level = l;
        solution = ss;
        value = v;
        direction = d;

        SetInfoToHelperButton();
    }
    private void SetInfoToHelperButton()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<HelperScript>().SetInfo(level, solution, value, direction);
        }
        HideButton();
    }
    public void HideButton()
    {
        for (int i = 0; i < 2; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void ShowButton()
    {
        for (int i = 0; i < 2; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
