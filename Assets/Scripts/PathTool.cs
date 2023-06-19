using Config;
using UnityEngine;

namespace Tools
{
    /// <summary>
    /// ·��������
    /// </summary>
    public class PathTool
    {
        /// <summary>
        /// ����Assets�ļ���·��
        /// </summary>
        /// <returns>.../Assets</returns>
        public static string GetAssetsPath()
        {
            return Application.dataPath;
        }

        /// <summary>
        /// ����Editor��Bundles��·��
        /// </summary>
        /// <returns></returns>
        public static string GetEditorBundlePath()
        {
            return GetAssetsPath() + AssetBundleConfig.m_BundlePath;
        }
    }
}
