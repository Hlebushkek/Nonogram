using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionScript : MonoBehaviour
{
    public CellState[,] solution {get; private set;}
    private CellState[,] decision;
    int width = 0, height = 0;
    public void GenerateSolutionArrays(CellStruct[,] ar, int width, int height)
    {
        this.width = width;
        this.height = height;
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
