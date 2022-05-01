using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class Utils
{
    private static string assetsDir = Application.streamingAssetsPath;
    public static string GetAssetsDirectory()
    {
        return assetsDir;
    }
    public static GameObject[] LoadAllAssetsOfAssetPack(AssetBundle assetBundle)
    {
        var prefabs = assetBundle.LoadAllAssets<GameObject>();
        return prefabs;
    }
    public static GameObject loadAssetFromAssetPack(AssetBundle assetBundle, string asset)
    {
        var prefab = assetBundle.LoadAsset<GameObject>(asset);
        return prefab;
    }
    public static AssetBundle loadAssetPack(string assetPack)
    {
        var myLoadedAssetBundle
            = AssetBundle.LoadFromFile(Path.Combine(Utils.GetAssetsDirectory(), assetPack));
        if (myLoadedAssetBundle == null)
        {
            throw new System.Exception("Failed to load AssetBundle!");
        }

        return myLoadedAssetBundle;
    }

    public static Item loadItemFromAssetPack(AssetBundle assetBundle, string asset)
    {
        var item = assetBundle.LoadAsset<Item>(asset);
        return item;
    }
    public static void UnloadAssetPack(AssetBundle assetBundle)
    {
        assetBundle.Unload(false);
    }
    public static void PrintGridPosDataTypeMatrix(GridPosdataType[,] matrix)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                sb.Append(matrix[i, j].roomId + "  |  ");

            }
            sb.Append("\n");
            for (int e = 0; e < matrix.GetLength(1); e++)
            {
                sb.Append("-");
            }
            sb.AppendLine();
        }
        Debug.Log(sb.ToString());
    }

}
