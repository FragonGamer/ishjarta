using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class Utils
{
    private static string assetsDir = Application.dataPath + "/AssetBundles";
    public static string GetAssetsDirectory()
    {
        return assetsDir;
    }
    public static void PrintMatrix<T>(T[,] matrix)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                stringBuilder.Append(matrix[i, j]);
                stringBuilder.Append(" ");
               
            }
            stringBuilder.Append("\n");
        }

        Debug.Log(stringBuilder.ToString());
    }
    public static void PrintPosMatrix(GridPosdataType[,] matrix)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                stringBuilder.Append($"{matrix[i, j].xPos}/{matrix[i, j].yPos}");
                stringBuilder.Append(" ");

            }
            stringBuilder.Append("\n");
        }

        Debug.Log(stringBuilder.ToString());
    }
}
