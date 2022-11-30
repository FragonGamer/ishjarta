using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public static class Utils
{
    #region Addressables
    public static void BeforeMultipleLoading<T>(ref Action<T> callback)
    {
        if (callback == null)
        {
            callback = a => { };
        }
    }
    #region UnloadingReferences
    public static void UnloadReferences(AssetReference assetReference)
    {
        assetReference.ReleaseAsset();
    }
    public static void UnloadReferences(ICollection<AssetReference> assetReferences)
    {
        foreach (var item in assetReferences)
        {
            item.ReleaseAsset();
        }
    }
    public static void UnloadReferences(IResourceLocation resourceLocation)
    {
        Addressables.Release(resourceLocation);
    }
    public static void UnloadReferences(ICollection<IResourceLocation> resourceLocation)
    {
        foreach (var item in resourceLocation)
        {
            Addressables.Release(item);
        }
    }
    #endregion
    #region LoadingAssetsWithAssetPath
    public static AsyncOperationHandle<T> LoadAssetByPath<T>(string assetPath)
    {
        return Addressables.LoadAssetAsync<T>(assetPath);
    }
    public static AsyncOperationHandle<IList<T>> LoadMultipleAssetsByPath<T>(string assetPath, Action<T> callback = null)
    {
        BeforeMultipleLoading<T>(ref callback);
        return Addressables.LoadAssetsAsync<T>(assetPath,callback);
    }
    #endregion
    #region LoadingAssetsWithAssetReferences

    public static AsyncOperationHandle<T> LoadAssetByGUID<T>(string assetReference)
    {
        return LoadAsset<T>(new AssetReference(assetReference));
    }
    public static AsyncOperationHandle<T> LoadAsset<T>(AssetReference assetReference)
    {
        return Addressables.LoadAssetAsync<T>(assetReference);
    }
    public static AsyncOperationHandle<IList<T>> LoadMultipleAssets<T>(ICollection<AssetReference> assetReference,Action<T> callback = null)
    {
        BeforeMultipleLoading<T>(ref callback);
        return Addressables.LoadAssetsAsync<T>(assetReference,callback);
    }
    #endregion
    #region LoadingTheIRessourceLocation
    public static AsyncOperationHandle<IList<IResourceLocation>> LoadIRessourceLocations<T>(List<string> keys,Addressables.MergeMode mergeMode = Addressables.MergeMode.Intersection)
    {
        return Addressables.LoadResourceLocationsAsync(keys, mergeMode, typeof(T));
    }
    public static AsyncOperationHandle<IList<IResourceLocation>> LoadIRessourceLocations<T>(string keys, Addressables.MergeMode mergeMode = Addressables.MergeMode.Intersection)
    {
        return Addressables.LoadResourceLocationsAsync(keys, mergeMode, typeof(T));
    }
    #endregion
    #region LoadingAssetsWithIRessourceLocation
    public static AsyncOperationHandle<IList<T>> LoadMultipleAssets<T>(ICollection<IResourceLocation> resourceLoctation, Action<T> callback = null)
    {
        BeforeMultipleLoading<T>(ref callback);
        return Addressables.LoadAssetsAsync(resourceLoctation, callback);
    }
    public static AsyncOperationHandle<T> LoadAsset<T>(IResourceLocation resourceLoctation)
    {
        return Addressables.LoadAssetAsync<T>(resourceLoctation);
    }
    #endregion
    #region LoadingObjects
    public static T LoadObject<T>(AsyncOperationHandle<T> asyncOperationHandle)
    {
        return asyncOperationHandle.Result;
    }
    public static List<TResult> LoadAllObjects<T,TResult>(AsyncOperationHandle<IList<T>> asyncOperationHandle)
    {
        return (List<TResult>)asyncOperationHandle.Result;
    }
    public static TResult LoadObjectWithPredicate<T, TResult>(AsyncOperationHandle<IList<T>> asyncOperationHandle, Predicate<TResult> predicate)
    {
        return LoadAllObjects<T, TResult>(asyncOperationHandle).Find(predicate);
    }

    public static List<TResult> LoadMultipleObjectsWithPredicate<T, TResult>(AsyncOperationHandle<IList<T>> asyncOperationHandle, Predicate<TResult> predicate)
    {
        return LoadAllObjects<T, TResult>(asyncOperationHandle).FindAll(predicate);
    }
    #endregion
    #endregion
    #region Addressables
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetReference"></param>
    /// <returns></returns>
    public static T LoadAssetFromAddressablesByReference<T>(AssetLabelReference assetReference)
    {
        return Addressables.LoadAssetAsync<T>(assetReference).WaitForCompletion();
    }
    /// <summary>
    /// Loads all Assets with the same Label.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stringLabel"></param>
    /// <param name="loadHandle">Please use this handler to Release the loaded Assets after using those with "Addressables.Release(loadHandle)" </param>
    /// <returns></returns>
    public static T[] LoadAssetsFromAddressablesByLabel<T>(string stringLabel)
    {
        return LoadAssetsFromAddressablesByLabel<T>(new List<string> { stringLabel });
    }
    /// <summary>
    /// Loads all Assets with the same Label.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stringLabel"></param>
    /// <param name="loadHandle">Please use this handler to Release the loaded Assets after using those with "Addressables.Release(loadHandle)" </param>
    /// <returns></returns>
    public static T[] LoadAssetsFromAddressablesByLabel<T>(List<AssetLabelReference> assetLabelReference)
    {
        foreach (var item in assetLabelReference)
        {
            if (!item.RuntimeKeyIsValid())
                return default(T[]);
        }
        List<T> assets = new List<T>();

        Addressables.LoadAssetsAsync<T>(assetLabelReference, (asset) => { assets.Add(asset); }, Addressables.MergeMode.Intersection).WaitForCompletion();
        return assets.ToArray();
    }

    /// <summary>
    /// Loads all Assets with the same Label.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stringLabel"></param>
    /// <param name="loadHandle">Please use this handler to Release the loaded Assets after using those with "Addressables.Release(loadHandle)" </param>
    /// <returns></returns>
    public static T[] LoadAssetsFromAddressablesByLabel<T>(List<string> stringLabels)
    {
        List<T> assets = new List<T>();
        Addressables.LoadAssetsAsync<T>(stringLabels, (asset) => { assets.Add(asset); }, Addressables.MergeMode.Intersection).WaitForCompletion();
        return assets.ToArray();
    }
    public static T[] LoadAssetsFromAddressablesByLabel<T>(string[] stringLabels)
    {
        var labels = stringLabels.ToList();
        return LoadAssetsFromAddressablesByLabel<T>(labels);
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
    /// <typeparam name="T"></typeparam>
    /// <param name="assetReference"></param>
    /// <returns></returns>
    public static T[] LoadAssetsFromAddressablesByReference<T>(IResourceLocation[] assetReference)
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
    public static GameObject LoadGameObjectFromAddressablesByReferenceWithName(AssetReference[] assetReference, string name)
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
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject LoadGameObjectFromAddressablesByReferenceWithName(IResourceLocation[] assetReference, string name)
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
    public static GameObject InsantiateFromAddressablesByReference(IResourceLocation assetReference)
    {

        return Addressables.InstantiateAsync(assetReference).WaitForCompletion();

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
        return LoadObject(LoadAssetByPath<GameObject>("UsableItem/Coin.prefab")).GetComponent<UsableItem>();
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
