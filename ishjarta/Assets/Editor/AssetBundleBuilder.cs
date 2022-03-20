using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class AssetBundleBuilder
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
       
        string assetBundleDirectory = $"Assets/StreamingAssets";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                        BuildAssetBundleOptions.None,
                                        BuildTarget.StandaloneWindows);
        AssetDatabase.Refresh();
    }
    public class GetAssetBundleNames
    {
        [MenuItem("Assets/Get Asset Bundle names")]
        static void GetNames()
        {
            var names = AssetDatabase.GetAllAssetBundleNames();
            foreach (string name in names)
                Debug.Log("Asset Bundle: " + name);
        }
    }
    
}
