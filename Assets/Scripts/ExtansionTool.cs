
using System.Threading.Tasks.Sources;

namespace Tools
{
    public static class ExtansionTool
    {
        /// <summary>
        /// 图片类型
        /// </summary>
        public const string png = ".png";
        /// <summary>
        /// 音频类型
        /// </summary>
        public const string wav = ".wav";
        /// <summary>
        /// 字体类型
        /// </summary>
        public const string ttf = ".ttf";
        /// <summary>
        /// 字体类型
        /// </summary>
        public const string otf = ".otf";
        /// <summary>
        /// 文本类型
        /// </summary>
        public const string txt = ".txt";
        /// <summary>
        /// 脚本类型
        /// </summary>
        public const string cs = ".cs";
        /// <summary>
        /// 模型类型
        /// </summary>
        public const string fbx = ".fbx";
        /// <summary>
        /// 材质类型
        /// </summary>
        public const string shader = ".shader";


        /// <summary>
        /// 获取全部要打到AB包的资源扩展名
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllAssetExtansion()
        {
            return new string[] { png, wav, ttf, otf, txt }; 
        }
    }
}
