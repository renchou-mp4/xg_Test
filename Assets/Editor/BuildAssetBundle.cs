using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildAssetBundle
{
    [MenuItem("Tools/Bundle/BuildAssetBundle")]
    public static void BuildAB()
    {
        BuildAssetBundleOptions options = BuildAssetBundleOptions.ChunkBasedCompression;//使用LZ4压缩，相比于LZMA和None，压缩体积中等，加载速度中等
        options |= BuildAssetBundleOptions.DeterministicAssetBundle;//构建资产包时，使用一个哈希值作为存储在资产包中的对象的ID。
        options |= BuildAssetBundleOptions.DisableWriteTypeTree;
        options |= BuildAssetBundleOptions.DisableLoadAssetByFileName;
        options |= BuildAssetBundleOptions.DisableLoadAssetByFileNameWithExtension;
    }

    private void GetBundleSetting()
    {
        BuildPipeline.BuildAssetBundles(,)
    }
}
