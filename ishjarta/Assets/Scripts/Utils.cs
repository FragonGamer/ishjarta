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
    public static T[] LoadAssetsFromAddressablesByLabel<T>(string stringLabel)
    {
        return LoadAssetsFromAddressablesByLabel<T>(new string[] { stringLabel });
    }
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
    /// <param name="AddressablePath"></param>
    /// <returns></returns>
    public static T[] LoadAssetsFromAddressablesByPath<T>(string AddressablePath)
    {
        List<T> assets = new List<T>();
        Addressables.LoadAssetsAsync<T>(AddressablePath, (asset) => { assets.Add(asset); }).WaitForCompletion();
        return assets.ToArray();
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
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetReference"></param>
    /// <returns></returns>
    public static T[] LoadAssetsFromAddressablesByReference<T>(AssetReference[] assetReference)
    {
        List<T> assets = new List<T>();
        foreach (var item in assetReference)
        {
            assets.Add(Addressables.LoadAssetAsync<T>(item).WaitForCompletion());
        }
        return assets.ToArray();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="assetReference"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static ScriptableObject LoadScriptableObjectFromAddressablesByReferenceWithName(AssetReference[] assetReference, string name)
    {
        List<ScriptableObject> assets = new List<ScriptableObject>();
        foreach (var item in assetReference)
        {
            assets.Add(Addressables.LoadAssetAsync<ScriptableObject>(item).WaitForCompletion());
        }
        foreach (var item in assets)
        {
            if (item.name == name)
                return item;
        }
        return default(ScriptableObject);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="assetReference"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject LoadGameObjectFromAddressablesByReferenceWithName(AssetReference[] assetReference,string name)
    {
        List<GameObject> assets = new List<GameObject>();
        foreach (var item in assetReference)
        {
            assets.Add(Addressables.LoadAssetAsync<GameObject>(item).WaitForCompletion());
        }
        foreach (var item in assets)
        {
            if (item.name == name)
                return item;
        }
        return default(GameObject);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="assetReference"></param>
    /// <returns></returns>
    public static GameObject InsantiateFromAddressablesByReference(AssetReference assetReference)
    {
        if (!assetReference.RuntimeKeyIsValid())
        {
            Debug.Log("AssetReference is invalid");
            return null;
        }
        return Addressables.InstantiateAsync(assetReference).WaitForCompletion();

    }
    public static void UnloadAssetReferences(AssetReference[] assetReference)
    {
        foreach (var item in assetReference)
        {
            item.ReleaseAsset();
        }
    }
    #endregion

    public static void UnloadAssetReferences(AssetBundle asset) { }
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
