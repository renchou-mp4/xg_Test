
using System.Threading.Tasks.Sources;

namespace Tools
{
    public static class ExtansionTool
    {
        /// <summary>
        /// ͼƬ����
        /// </summary>
        public const string png = ".png";
        /// <summary>
        /// ��Ƶ����
        /// </summary>
        public const string wav = ".wav";
        /// <summary>
        /// ��������
        /// </summary>
        public const string ttf = ".ttf";
        /// <summary>
        /// ��������
        /// </summary>
        public const string otf = ".otf";
        /// <summary>
        /// �ı�����
        /// </summary>
        public const string txt = ".txt";
        /// <summary>
        /// �ű�����
        /// </summary>
        public const string cs = ".cs";
        /// <summary>
        /// ģ������
        /// </summary>
        public const string fbx = ".fbx";
        /// <summary>
        /// ��������
        /// </summary>
        public const string shader = ".shader";


        /// <summary>
        /// ��ȡȫ��Ҫ��AB������Դ��չ��
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllAssetExtansion()
        {
            return new string[] { png, wav, ttf, otf, txt }; 
        }
    }
}
