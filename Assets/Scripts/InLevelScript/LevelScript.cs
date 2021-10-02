using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    [SerializeField] private GameObject Tile;
    [SerializeField] private GameObject NumberTextBlockHorizontal, NumberTextBlockVertical;
    [SerializeField] private Sprite NTBHorizontalLight, NTBVerticalLight, NTBHorizontalGray, NTBVerticalGray;
    private Texture2D img;
    private SolutionScript solution;
    private CellStruct[,] imgArray;
    private CellScript[,] cellObjArray;
    private int width, height;
    private float startX, startY;
    private void Awake()
    {
        img = TransferDataScript.levelTexture;

        TextScript.CreateDictionary(); //need rework

        imgArray = ImageAnalyze.CreateCellStructArray(img);
        width = imgArray.GetLength(0);
        height = imgArray.GetLength(1);
        cellObjArray = new CellScript[width, height];
        solution = this.GetComponent<SolutionScript>();
        solution.GenerateSolutionArrays(imgArray, width, height);
        CreateGridRepresent();
        InstantiateNumbers();
    }
    private void CreateGridRepresent()
    {
        startX = (1 - width + 2) / 2.0f;
        startY = (1 - height - 2) / 2.0f;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var obj = Instantiate(Tile, new Vector3(startX + i, startY + j, 0), Quaternion.identity, this.transform);
                cellObjArray[i, j] = obj.GetComponent<CellScript>();
                cellObjArray[i, j].SetCoord(i, j);
            }
        }
    }
    private void InstantiateNumbers()
    {
        //width
        for (int i = 0; i < height; i++)
        {
            var textObj = Instantiate(NumberTextBlockHorizontal, new Vector3(startX - 1.5f, startY + i, -1), Quaternion.identity);
            
            SpriteRenderer blockRend = NumberTextBlockHorizontal.GetComponent<SpriteRenderer>();
            if (i % 2 == 1) blockRend.sprite = NTBHorizontalLight;
            else blockRend.sprite = NTBHorizontalGray;

            textObj.GetComponent<NumBlockScript>().CalculateNums(this, solution, Direction.Horizontal, i, width);
        }
        //Height
        for (int i = 0; i < width; i++)
        {
            var textObj = Instantiate(NumberTextBlockVertical, new Vector3(startX + i, startY + height + 0.5f, -1), Quaternion.identity);
            
            SpriteRenderer blockRend = NumberTextBlockVertical.GetComponent<SpriteRenderer>();
            if (i % 2 == 1) blockRend.sprite = NTBVerticalLight;
            else blockRend.sprite = NTBVerticalGray;
            
            textObj.GetComponent<NumBlockScript>().CalculateNums(this, solution, Direction.Vertical, i, height);
        }
    }
    public void SetColorToCell() //When Win
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (solution.GetSolutionForCell(i, j) == CellState.Empty)
                {
                    cellObjArray[i, j].ApplyCellStateFromHelper(CellState.Empty);
                }
                cellObjArray[i, j].GetComponent<SpriteRenderer>().color = imgArray[i, j].color;
            }
        }
    }
    public CellScript GetCell(int x, int y)
    {
        return cellObjArray[x, y];
    }
}
