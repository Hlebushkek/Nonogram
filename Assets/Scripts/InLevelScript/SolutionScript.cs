using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionScript : MonoBehaviour
{
    private CellState[,] solution;
    private CellState[,] decision;
    private int width = 0, height = 0;
    public void GenerateSolutionArrays(CellStruct[,] ar, int w, int h)
    {
        width = w;
        height = h;
        solution = new CellState[width, height];
        decision = new CellState[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                solution[i, j] = ar[i, j].state;
                decision[i, j] = CellState.Empty;
            }
        }
    }
    public CellState GetSolutionForCell(int x, int y)
    {
        return solution[x, y];
    }
    public CellState GetDecisionForCell(int x, int y)
    {
        return decision[x, y];
    }
    public void TakeChanges(CellInfo cellinfo)
    {
        decision[cellinfo.x, cellinfo.y] = cellinfo.state;
        Check(cellinfo.x, cellinfo.y);
    }
    public void ApplyCorrectValueForCell(int x, int y)
    {
        decision[x, y] = solution[x, y];
        Check(x, y);
    }
    private void Check(int x, int y)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (decision[i, j] == solution[i, j] || (decision[i, j] == CellState.Crossed && solution[i, j] == CellState.Empty))
                {
                    continue;
                } else return;
            }
        }
        Debug.Log("WIN");
        this.gameObject.GetComponent<LevelScript>().SetColorToCell();
    }
    public void PrintCheck()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (decision[i, j] != solution[i, j] && decision[i, j] != CellState.Crossed)
                {
                    Debug.Log("Error in " + i + " " + j);
                }
            }
        }
    }
}
