using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class Utils
{
    #region Addressables


    /// <summary>
    /// Loads all Assets with the same Label.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stringLabel"></param>
    /// <param name="loadHandle">Please use this handler to Release the loaded Assets after using those with "Addressables.Release(loadHandle)" </param>
    /// <returns></returns>
    public static T[] LoadAssetsFromAddressablesByLabel<T>(AssetLabelReference[] assetLabelReference)
    {
        List<T> assets = new List<T>();
        Addressables.LoadAssetsAsync<T>(assetLabelReference, (asset) => { assets.Add(asset); }).WaitForCompletion();
        return assets.ToArray();
    }

    /// <summary>
    /// Loads all Assets with the same Label.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stringLabel"></param>
    /// <param name="loadHandle">Please use this handler to Release the loaded Assets after using those with "Addressables.Release(loadHandle)" </param>
    /// <returns></returns>
    public static T[] LoadAssetsFromAddressablesByLabel<T>(string[] stringLabel)
    {
        List<T> assets = new List<T>();
        Addressables.LoadAssetsAsync<T>(stringLabel, (asset) => { assets.Add(asset); }).WaitForCompletion();
        return assets.ToArray();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="AddressablePath"></param>
    /// <returns></returns>
    public static T LoadAssetFromAddressablesByPath<T>(string AddressablePath)
    {
        return Addressables.LoadAssetAsync<T>(AddressablePath).WaitForCompletion();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetReference"></param>
    /// <returns></returns>
    public static T LoadAssetFromAddressablesByReference<T>(AssetReference assetReference)
    {
        return Addressables.LoadAssetAsync<T>(assetReference).WaitForCompletion();
    }

    public static GameObject InsantiateFromAddressablesByReference(AssetReference assetReference)
    {
        if (!assetReference.RuntimeKeyIsValid())
        {
            Debug.Log("AssetReference is invalid");
            return null;
        }
        return Addressables.InstantiateAsync(assetReference).WaitForCompletion();

    }
    #endregion
    #region oldAssets
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
            throw new System.Exception($"Failed to load AssetBundle! -> {assetPack}");
        }

        return myLoadedAssetBundle;
    }

    public static Item loadItemFromAssetPack(AssetBundle assetBundle, string asset)
    {
        var item = assetBundle.LoadAsset<Item>(asset);
        return item;
    }
    public static EnemyLootDropTable[] loadEnemyLootDropTableFromAssetPack(AssetBundle assetBundle, string asset)
    {
        var item = assetBundle.LoadAssetWithSubAssets<EnemyLootDropTable>(asset);
        return item;
    }

    public static void UnloadAssetPack(AssetBundle assetBundle)
    {
        assetBundle.Unload(false);
    }
    #endregion
    public static UsableItem GetCoinObject()
    {
        return Utils.LoadAssetFromAddressablesByPath<UsableItem>("UsableItem/Coin.prefab");
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
