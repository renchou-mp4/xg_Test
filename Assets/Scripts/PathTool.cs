using Config;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

namespace Tools
{
    /// <summary>
    /// 路径工具类
    /// </summary>
    public static class PathTool
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

        /// <summary>
        /// 根据路径返回文件名称(可带扩展名)
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="includeExtension">是否包含扩展名</param>
        /// <returns></returns>
        public static string GetFileNameByPath(string path, bool includeExtension = false)
        {
            if(includeExtension)
            {
                return path.Substring(path.LastIndexOf("/") + 1);
            }
            else
            {
                string temp = path.Substring(path.LastIndexOf("/") + 1);
                return temp.Remove(temp.LastIndexOf("."));
            }
        }
    }
}
