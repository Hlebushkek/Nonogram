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
    public CellScript[,] cellObjArray {get; private set;}
    private List<List<int>> widthValues = new List<List<int>>();
    private List<List<int>> heightValues = new List<List<int>>();
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

        CreateGridRepresent();
        ReadNumbers();
        InstantiateNumbers();
        solution.GenerateSolutionArrays(imgArray, width, height);
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
    private void ReadNumbers()
    {
        //Width
        for(int i = 0; i < height; i++)
        {
            int prev = 0; //0 - white, 1 - color
            List<int> values = new List<int>();
            for (int j = width - 1; j >= 0; j--)
            {
                Color curColor = imgArray[j, i].color;
                if (curColor != Color.white && prev == 0)
                {
                    values.Add(1);
                    prev = 1;
                }
                else if (curColor != Color.white && prev == 1)
                {
                    values[values.Count - 1]++;
                }
                else if (curColor == Color.white)
                {
                    prev = 0;
                }
            }
            widthValues.Add(values);
        }
        //PrintWidthNum();
        //Height
        for(int i = 0; i < width; i++)
        {
            int prev = 0; //0 - white, 1 - color
            List<int> values = new List<int>();
            for (int j = 0; j < height; j++)
            {
                Color curColor = imgArray[i, j].color;
                if (curColor != Color.white && prev == 0)
                {
                    values.Add(1);
                    prev = 1;
                }
                else if (curColor != Color.white && prev == 1)
                {
                    values[values.Count - 1]++;
                }
                else if (curColor == Color.white)
                {
                    prev = 0;
                }
            }
            heightValues.Add(values);
        }
        //PrintHeightNum();
    }
    private void InstantiateNumbers()
    {
        //width
        int i = 0;
        foreach (var line in widthValues)
        {
            var textObj = Instantiate(NumberTextBlockHorizontal, new Vector3(startX - 1.5f, startY + i, -1), Quaternion.identity);
            
            SpriteRenderer blockRend = NumberTextBlockHorizontal.GetComponent<SpriteRenderer>();
            if (i % 2 == 1) blockRend.sprite = NTBHorizontalLight;
            else blockRend.sprite = NTBHorizontalGray;

            textObj.GetComponent<NumBlockScript>().SetInfo(this, solution, i, false);

            int j = 0;
            foreach (var v in line)
            {
                Transform letterObj = CreateLetter(v);
                letterObj.SetParent(textObj.transform);
                letterObj.localPosition = new Vector3(0.75f - j * 0.4f, 0, -1);
                j++;
            }
            i++;
        }
        //Height
        i = 0;
        foreach (var line in heightValues)
        {
            var textObj = Instantiate(NumberTextBlockVertical, new Vector3(startX + i, startY + height + 0.5f, -1), Quaternion.identity);
            
            SpriteRenderer blockRend = NumberTextBlockVertical.GetComponent<SpriteRenderer>();
            if (i % 2 == 1) blockRend.sprite = NTBVerticalLight;
            else blockRend.sprite = NTBVerticalGray;
            
            textObj.GetComponent<NumBlockScript>().SetInfo(this, solution, i, true);
            
            int j = 0;
            foreach (var v in line)
            {
                Transform letterObj = CreateLetter(v);
                letterObj.SetParent(textObj.transform);
                letterObj.localPosition = new Vector3(0, -0.6f + j * 0.5f, -1);
                j++;
            }
            i++;
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
    public void SetColorToCell() //When Win
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (solution.solution[i, j] == CellState.Empty)
                {
                    cellObjArray[i, j].ApplyCellStateFromHelper(CellState.Empty);
                }
                cellObjArray[i, j].GetComponent<SpriteRenderer>().color = imgArray[i, j].color;
            }
        }
    }
    // /////////////////////////////////////////////////////////////
    private void PrintWidthNum()
    {
        foreach (var line in widthValues)
        {
            Debug.Log("----");
            foreach (var v in line)
            {
                Debug.Log(v);
            }
        }
    }
    private void PrintHeightNum()
    {
        foreach (var line in heightValues)
        {
            Debug.Log("----");
            foreach (var v in line)
            {
                Debug.Log(v);
            }
        }
    }
}
