using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    private static string assetsDir = Application.dataPath + "/AssetBundles";
    public static string GetAssetsDirectory()
    {
        return assetsDir;
    }
}
