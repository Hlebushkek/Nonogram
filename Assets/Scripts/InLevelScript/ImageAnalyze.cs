using UnityEngine;

public static class ImageAnalyze
{
    public static CellStruct[,] CreateCellStructArray(Texture2D img)
    {
        var imgArray = new CellStruct[img.width,img.height];

        for (int y = 0; y < img.height; y++)
        {
            for (int x = 0; x < img.width; x++)
            {
                Color c = img.GetPixel(x, y);
                //Debug.Log(x + " " + y + " " + c);
                CellState s = CellState.Empty;
                if (c != Color.white && c.a != 0) s = CellState.Filled;
                imgArray[x, y] = new CellStruct(c, s);
            }
        }
        //PrintImgArray(imgArray);
        return imgArray;
    }
    private static void PrintImgArray(CellStruct[,] imgArray)
    {
        for (int i = 0; i < imgArray.GetLength(0); i++)
        {
            for (int j = 0; j < imgArray.GetLength(1); j++)
            {
                Debug.Log(imgArray[i, j].color + " " + imgArray[i, j].state);
            }
        }
    }
}
