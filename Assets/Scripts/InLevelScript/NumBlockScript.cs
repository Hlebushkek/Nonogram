using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumBlockScript : MonoBehaviour
{
    private GameObject FullLineHelper, RandomOnLineHelper;
    private List<int> values;
    public void CalculateNums(LevelScript lev, SolutionScript s, Direction direction, int value, int length)
    {
        ReadLine(s, direction, value, length);
        CreateBlockContent(direction);
        SetInfoToHelperButton(lev, s, direction, value, length);
    }
    private void ReadLine(SolutionScript s, Direction d, int v, int length)
    {
        values = new List<int>();
        CellState prevState = CellState.Empty;

        if (d == Direction.Horizontal)
        {
            for (int i = length - 1; i >= 0; i--)
            {
                CellState curState = s.GetSolutionForCell(i, v);
                prevState = ReadCell(prevState, curState);
            }
        }
        else if (d == Direction.Vertical)
        {
            for (int i = 0; i < length; i++)
            {
                CellState curState = s.GetSolutionForCell(v, i);
                prevState = ReadCell(prevState, curState);
            }
        }
    }
    private CellState ReadCell(CellState prevState, CellState curState)
    {
        if (curState == CellState.Filled && prevState == CellState.Empty)
        {
            values.Add(1);
            prevState = CellState.Filled;
        }
        else if (curState == CellState.Filled && prevState == CellState.Filled)
        {
            values[values.Count - 1]++;
        }
        else if (curState == CellState.Empty)
        {
            prevState = CellState.Empty;
        }
        return prevState;
    }
    private void CreateBlockContent(Direction d)
    {
        for (int i = 0; i < values.Count; i++)
        {
            Transform letterObj = CreateLetter(values[i]);
            letterObj.SetParent(transform);

            if (d == Direction.Vertical)
            {
                letterObj.localPosition = new Vector3(0, -0.6f + i * 0.5f, -1);
            } else
            {
                letterObj.localPosition = new Vector3(0.6f - i * 0.4f, 0, -1);
            }
        }
    }
    private Transform CreateLetter(int v)
    {
        var letterObj = new GameObject();
        string num = v.ToString();
        SpriteRenderer letterRend = letterObj.AddComponent<SpriteRenderer>();
        letterRend.sprite = TextScript.GetLetterObj(num[0]);
        letterRend.color = Color.black;

        return letterObj.transform;
    }
    private void SetInfoToHelperButton(LevelScript lev, SolutionScript s, Direction d, int v, int l)
    {
        for (int i = 0; i < 2; i++)
        {
            transform.GetChild(i).GetComponent<HelperScript>().SetInfo(lev, s, d, v, l);
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
