using Config;
using UnityEngine;

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
    }
}
