using Config;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

namespace Tools
{
    /// <summary>
    /// 路径工具类
    /// </summary>
    public class PathTool
    {
        /// <summary>
        /// 返回Assets文件夹路径
        /// </summary>
        /// <returns>.../Assets</returns>
        public static string GetAssetsPath()
        {
            return Application.dataPath;
        }

        /// <summary>
        /// 返回Editor下Bundles的路径
        /// </summary>
        /// <returns></returns>
        public static string GetEditorBundlePath()
        {
            return GetAssetsPath() + AssetBundleConfig.m_BundlePath;
        }


        public static string[] GetFileNameByPath(string path,bool includeExtension = false)
        {
            if(includeExtension)
            {
                return System.IO.Directory.GetFiles(path,);
            }
        }
    }
}
