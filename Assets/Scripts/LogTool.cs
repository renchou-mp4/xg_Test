using System;
using System.IO;
using UnityEngine;


namespace Tools
{
    #region 自定义输出日志到文件
    /// <summary>
    /// 使用文件日志Handler，会将日志信息写到指定文件中
    /// </summary>
    /// 相当于将Debug.unityLogger.logHandler添加了StreamWriter和FileStream后再封装
    public class FileLogHandler : ILogHandler
    {
        //C:/Users/1223/AppData/LocalLow/DefaultCompany/xg_Test/MyLogs.txt
        private string m_FilePath = Application.persistentDataPath + "/MyLogs.txt";

        private ILogHandler m_LogHandler = Debug.unityLogger.logHandler;
        private StreamWriter m_FileWriter;
        private FileStream m_FileStream;


        /// <summary>
        /// 构造，初始化FileStream和StreamWriter
        /// </summary>
        public FileLogHandler()
        {
            m_FileStream = new FileStream(m_FilePath, System.IO.FileMode.Append, FileAccess.Write);
            m_FileWriter = new StreamWriter(m_FileStream, System.Text.Encoding.UTF8);

            //执行后，m_LogHandler = 原来的UnityLogger，Debug.unityLogger.logHandler = FileLogHandler。
            //那么再调用Debug.unityLogger.logHandler的时候就等同于是调用原来的UnityLogger的再封装，即FileLogHandler
            Debug.unityLogger.logHandler = this;

            Application.logMessageReceived += LogCallback;
            AppDomain.CurrentDomain.UnhandledException += OnUnresolvedExceptionHandler;
        }


        public void LogException(Exception exception, UnityEngine.Object context)
        {
            m_LogHandler.LogException(exception, context);
        }


        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            m_FileWriter.WriteLine(String.Format(format, args));
            //不等待之后的写入，先将数据放到文件中
            m_FileWriter.Flush();
            //这里的m_LogHandler指向原来的Debug.unityLogger.logHandler，和UnityDebug等同
            m_LogHandler.LogFormat(logType, context, format, args);
        }


        public void LogDispose()
        {
            m_FileWriter.Dispose();
            m_FileStream.Dispose();
        }


        /// <summary>
        /// condition为日志信息，stackTrace为堆栈跟踪，type为日志类型
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="stackTrace"></param>
        /// <param name="type"></param>
        private void LogCallback(string condition, string stackTrace, LogType type)
        {
            m_FileWriter.Write("\r\n日志信息：\r\n");
            m_FileWriter.Write(condition);
            m_FileWriter.Write("\r\n堆栈跟踪：\r\n");
            m_FileWriter.Write(stackTrace);
            m_FileWriter.Flush();
        }


        /// <summary>
        /// 可以捕获Unity无法捕获的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void OnUnresolvedExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 输出日志到文件中
    /// </summary>
    public static class FileLogTool
    {
        private static FileLogHandler m_FileLogHandler = new FileLogHandler();

        //使用这个特性可以用宏定义来控制是否编译为中间语言，用于方法时，只能用于返回值为void的方法。
        //在unity的文件中使用#define不起作用。在ProjectSetting-》Player-》OtherSettings-》ScriptingDefineSymbols中设置即可
        [System.Diagnostics.Conditional("Develop_Log")]
        public static void Log(string msg)
        {
            m_FileLogHandler.LogFormat(LogType.Log, null, msg);
        }


        [System.Diagnostics.Conditional("Develop_Log")]
        public static void LogFormat(string msg, params object[] args)
        {
            m_FileLogHandler.LogFormat(LogType.Log, null, msg, args);
        }


        [System.Diagnostics.Conditional("Develop_Log")]
        public static void LogException(Exception exception, UnityEngine.Object content)
        {
            m_FileLogHandler.LogException(exception, content);
        }


        /// <summary>
        /// 释放StreamWriter的占用，可以查看文本文档的内容，否则占用无法查看。只在运行最后调用，执行后无法再输出log
        /// </summary>
        [System.Diagnostics.Conditional("Develop_Log")]
        public static void LogDispose()
        {
            m_FileLogHandler.LogDispose();
        }
    }
    #endregion


    #region 自定义输出日志
    public class CustomLogHandler : ILogHandler
    {
        private ILogHandler m_LogHandler = Debug.unityLogger.logHandler;


        public CustomLogHandler() { }


        public void LogException(Exception exception, UnityEngine.Object context)
        {
            m_LogHandler.LogException(exception, context);
        }


        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            m_LogHandler.LogFormat(logType, context, format, args);
        }
    }


    /// <summary>
    /// 只有常用的log，有需要再添加对应的重载方法
    /// </summary>
    public static class LogTool
    {
        private static Logger m_Logger = new Logger(new CustomLogHandler());

        //也可以写个开关控制是否输出，也可以使用这种方式。这种方式若条件不满足，不会被编译成中间语言
        [System.Diagnostics.Conditional("Develop_Log")]
        public static void Log(object message)
        {
            m_Logger.Log(message);
        }


        [System.Diagnostics.Conditional("Develop_Log")]
        public static void LogFormat(string format, params object[] args)
        {
            m_Logger.LogFormat(LogType.Log, format, args);
        }


        [System.Diagnostics.Conditional("Develop_Log")]
        public static void LogWarning(string format, params object[] args)
        {
            m_Logger.LogFormat(LogType.Warning, format, args);
        }


        [System.Diagnostics.Conditional("Develop_Log")]
        public static void LogError(string format, params object[] args)
        {
            m_Logger.LogFormat(LogType.Error, format, args);
        }


        [System.Diagnostics.Conditional("Develop_Log")]
        public static void LogException(Exception exception)
        {
            m_Logger.LogException(exception);
        }
    }
    #endregion
}
