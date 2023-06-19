using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 构建AB包
/// </summary>
public class BuildAssetBundle
{
    private AssetBundleBuild[] m_ABBuilds;



    /// <summary>
    /// 在编辑器中构建AB包
    /// </summary>
    [MenuItem("Tools/Bundle/BuildAssetBundle")]
    public static void BuildAB()
    {

        //BuildPipeline.BuildAssetBundles(,)
    }


    /// <summary>
    /// 获取构建AB设置
    /// </summary>
    private static BuildAssetBundleOptions GetBundleSetting()
    {
        BuildAssetBundleOptions options = BuildAssetBundleOptions.ChunkBasedCompression;//使用LZ4压缩，相比于LZMA和None，压缩体积中等，加载速度中等
        options |= BuildAssetBundleOptions.DeterministicAssetBundle;//构建资产包时，使用一个哈希值作为存储在资产包中的对象的ID。
        options |= BuildAssetBundleOptions.DisableWriteTypeTree;//打包时不写入类型信息，可以减小包体积，让加载更快
        options |= BuildAssetBundleOptions.DisableLoadAssetByFileName;//禁用使用文件名称查找资源
        options |= BuildAssetBundleOptions.DisableLoadAssetByFileNameWithExtension;//禁用使用带扩展名的文件名称查找资源
        //默认情况下，可通过三种方式查找资源包中的相同资源：完整资源路径、资源文件名称和带扩展名的资源文件名称。
        //完整路径在资源包中进行了序列化，而文件名称以及带扩展名的文件名称是在从文件中加载资源包时生成的。
        //禁用此选项可以优化内存和加载性能
        return options;
    }


    /// <summary>
    /// 获取构建AB的目标平台
    /// </summary>
    /// <returns></returns>
    private static BuildTarget GetBuildTarget()
    {
#if UNITY_STANDALONE_WIN
        return BuildTarget.StandaloneWindows64;
#endif

#if UNITY_ANDROID
        return BuildTarget.Androidl
#endif

#if UNITY_IOS
        return BuildTarget.IOS;
#endif
    }


    private static void GetAssetBundleBuilds()
    {
        AssetBundleBuild a;
        
    }
}
