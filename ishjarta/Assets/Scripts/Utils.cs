using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public static void UnloadReferences(IEnumerable<AssetReference> assetReferences)
    {
        foreach (var item in assetReferences)
        {
            item.ReleaseAsset();
        }
    }

    #endregion
    #region LoadingAssetsWithAssetPath
    public static T LoadAssetByPath<T>(string assetPath)
    {
        return Addressables.LoadAssetAsync<T>(assetPath).WaitForCompletion();
    }
    public static List<T> LoadMultipleAssetsByPath<T>(string assetPath, Action<T> callback = null, Addressables.MergeMode mergeMode = Addressables.MergeMode.Intersection)
    {
        BeforeMultipleLoading<T>(ref callback);
        return Addressables.LoadAssetsAsync<T>(assetPath,callback,mergeMode).WaitForCompletion().ToList();
    }
    #endregion
    #region LoadingAssetsWithAssetReferences

    public static T LoadAssetByGUID<T>(string assetReference)
    {
        return LoadAsset<T>(new AssetReference(assetReference));
    }
    public static T LoadAsset<T>(AssetReference assetReference)
    {
        return Addressables.LoadAssetAsync<T>(assetReference).WaitForCompletion();
    }
    public static IEnumerable<T> LoadMultipleAssets<T>(IEnumerable<AssetReference> assetReference,Action<T> callback = null, Addressables.MergeMode mergeMode = Addressables.MergeMode.Intersection)
    {
        BeforeMultipleLoading<T>(ref callback);
        return Addressables.LoadAssetsAsync<T>(assetReference,callback,mergeMode).WaitForCompletion();
    }
    public static IEnumerable<T> LoadMultipleAssets<T>(IEnumerable<string> assetReference, Action<T> callback = null, Addressables.MergeMode mergeMode = Addressables.MergeMode.Intersection)
    {
        BeforeMultipleLoading<T>(ref callback);
        return Addressables.LoadAssetsAsync<T>(assetReference, callback, mergeMode).WaitForCompletion();
    }
    #endregion
    #region LoadingTheIRessourceLocation
    public static IList<IResourceLocation> LoadIRessourceLocations<T>(string[] keys, Addressables.MergeMode mergeMode = Addressables.MergeMode.Intersection)
    {
        return LoadIRessourceLocations<T>(keys.ToList(), mergeMode);
    }
    public static IList<IResourceLocation> LoadIRessourceLocations<T>(IEnumerable<string> keys,Addressables.MergeMode mergeMode = Addressables.MergeMode.Intersection)
    {
        return Addressables.LoadResourceLocationsAsync(keys, mergeMode, typeof(T)).WaitForCompletion();
    }
    public static IList<IResourceLocation> LoadIRessourceLocations<T>(string keys, Addressables.MergeMode mergeMode = Addressables.MergeMode.Intersection)
    {
        return Addressables.LoadResourceLocationsAsync(keys, mergeMode, typeof(T)).WaitForCompletion();
    }
    #endregion
    #region LoadingObjectsWithAssetRefence
    public static IEnumerable<T> LoadMultipleObjects<T>(IEnumerable<AssetReference> resourceLoctation)
    {
        foreach (var item in resourceLoctation)
        {
            yield return Addressables.LoadAssetAsync<T>(item).WaitForCompletion();
        }
    }
    public static T LoadObject<T>(AssetReference resourceLoctation)
    {
        return Addressables.LoadAssetAsync<T>(resourceLoctation).WaitForCompletion();
    }
    public static TResult LoadObjectWithPredicate<TResult>(IEnumerable<AssetReference> ressourceLocation, Predicate<TResult> predicate)
    {
        return LoadMultipleObjects<TResult>(ressourceLocation).ToList().Find(predicate);
    }

    public static List<TResult> LoadMultipleObjectsWithPredicate<TResult>(IEnumerable<AssetReference> ressourceLocation, Predicate<TResult> predicate)
    {
        return LoadMultipleObjects<TResult>(ressourceLocation).ToList().FindAll(predicate);
    }
    public static GameObject LoadGameObjectByName(IEnumerable<AssetReference> ressourceLocation, string name)
    {
        return LoadMultipleObjects<GameObject>(ressourceLocation).ToList().Find(item => item.name == name);
    }
    #endregion
    #region LoadingObjectsWithIRessourceLocation
    public static IEnumerable<T> LoadMultipleObjects<T>(IList<IResourceLocation> resourceLoctation)
    {
        foreach (var item in resourceLoctation)
        {
            yield return Addressables.LoadAssetAsync<T>(item).WaitForCompletion();
        }
    }
    public static T LoadObject<T>(IResourceLocation resourceLoctation)
    {
        return Addressables.LoadAssetAsync<T>(resourceLoctation).WaitForCompletion();
    }
    public static TResult LoadObjectWithPredicate<TResult>(IList<IResourceLocation> ressourceLocation, Predicate<TResult> predicate)
    {
        return LoadMultipleObjects<TResult>(ressourceLocation).ToList().Find(predicate);
    }

    public static List<TResult> LoadMultipleObjectsWithPredicate<TResult>(IList<IResourceLocation> ressourceLocation, Predicate<TResult> predicate) 
    {
        return LoadMultipleObjects<TResult>(ressourceLocation).ToList().FindAll(predicate);
    }
    public static GameObject LoadGameObjectByName(IList<IResourceLocation> ressourceLocation, string name)
    {
        return LoadMultipleObjects<GameObject>(ressourceLocation).ToList().Find(item => item.name == name);
    }
    #endregion
    #region InstantiatingGameObjects
    public static GameObject InstantiateGameObject(IResourceLocation location, Vector3 postion, Quaternion quaternion)
    {
        return Addressables.InstantiateAsync(location, postion, quaternion).WaitForCompletion();
    }
    #endregion
    #endregion

    public static T LoadItemByName<T>(IList<IResourceLocation> resourceLocation, string name) where T:Item{
        return LoadMultipleObjects<T>(resourceLocation).ToList().Find(item => item.ItemName == name);
    }
    public static UsableItem GetCoinObject()
    {
        return LoadAssetByPath<GameObject>("UsableItem/Coin.prefab").GetComponent<UsableItem>();
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
