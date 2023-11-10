using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;

namespace Tools
{

    #region　基于UnityAPI实现的日志输出
    /*
    #region 自定义输出日志到文件
    /// <summary>
    /// 使用文件日志Handler，会将日志信息写到指定文件中
    /// </summary>
    /// 相当于将Debug.unityLogger.logHandler添加了StreamWriter和FileStream后再封装
    public class FileLogHandler : ILogHandler
    {
        //C:/Users/1223/AppData/LocalLow/DefaultCompany/xg_Test/MyLogs.txt
        private string m_FilePath = Application.persistentDataPath + "/MyLogs.txt";

        private ILogHandler m_LogHandler = UnityEngine.Debug.unityLogger.logHandler;
        private StreamWriter m_FileWriter;


        /// <summary>
        /// 构造，初始化FileStream和StreamWriter
        /// </summary>
        public FileLogHandler()
        {
            m_FileWriter = new StreamWriter(m_FilePath, true, System.Text.Encoding.UTF8);

            //执行后，m_LogHandler = 原来的UnityLogger，Debug.unityLogger.logHandler = FileLogHandler。
            //那么再调用Debug.unityLogger.logHandler的时候就等同于是调用原来的UnityLogger的再封装，即FileLogHandler
            UnityEngine.Debug.unityLogger.logHandler = this;

            Application.logMessageReceived += LogCallback;
            AppDomain.CurrentDomain.UnhandledException += OnUnresolvedExceptionHandler;
        }


        public void LogException(Exception exception, UnityEngine.Object context)
        {
            //这里的m_LogHandler指向原来的Debug.unityLogger.logHandler，和UnityDebug等同
            m_LogHandler.LogException(exception, context);
        }


        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            m_FileWriter.WriteLine(CurrentTime() + "\r\n" + String.Format(format, args));
            //不等待之后的写入，先将数据放到文件中
            m_FileWriter.Flush();
            //这里的m_LogHandler指向原来的Debug.unityLogger.logHandler，和UnityDebug等同
            m_LogHandler.LogFormat(logType, context, format, args);
        }


        /// <summary>
        /// condition为日志信息，stackTrace为堆栈跟踪，type为日志类型
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="stackTrace"></param>
        /// <param name="type"></param>
        /// 此方法只有在主线程中才会被触发，如果需要访问在主线程中受限的API，则使用它。
        private void LogCallback(string condition, string stackTrace, LogType type)
        {
            m_FileWriter.Write(
                CurrentTime()
                + "\r\n日志信息：\r\n"
                + condition
                + "\r\n堆栈跟踪：\r\n"
                + stackTrace
                + "--------------------------------------------------------------------------------------------\r\n");
            m_FileWriter.Flush();
        }


        /// <summary>
        /// 可以捕获Unity无法捕获的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnUnresolvedExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }


        private string CurrentTime()
        {
            return System.DateTime.Now.ToString();
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
    }
    #endregion


    #region 自定义输出日志
    public class CustomLogHandler : ILogHandler
    {
        private ILogHandler m_LogHandler = UnityEngine.Debug.unityLogger.logHandler;


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
    public static class CustomLogTool
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
    */
    #endregion


    #region 基于C#API实现的日志输出，不依赖于Unity

    public delegate void LogDelegate(string outputLevel, string message);

    /// <summary>
    /// 输出端类型
    /// </summary>
    public enum OutputType
    {
        /// <summary>
        /// 文件输出
        /// </summary>
        File,
        /// <summary>
        /// PC端GUI输出
        /// </summary>
        GUI,
        /// <summary>
        /// 移动GUI输出
        /// </summary>
        MobileGUI,
        /// <summary>
        /// 控制台输出（unity默认输出）
        /// </summary>
        Console,
        /// <summary>
        /// 窗口输出
        /// </summary>
        Window,
    }

    /// <summary>
    /// 输出级别
    /// </summary>
    public enum OutputLevel
    {
        /// <summary>
        /// 打开日志信息
        /// </summary>
        On,
        /// <summary>
        /// 打印运行信息
        /// </summary>
        Debug,
        /// <summary>
        /// 打印感兴趣的信息
        /// </summary>
        Info,
        /// <summary>
        /// 打印可能会出错的信息
        /// </summary>
        Warning,
        /// <summary>
        /// 打印出错的信息
        /// </summary>
        Error,
        /// <summary>
        /// 关闭日志信息
        /// </summary>
        Off,
    }

    /// <summary>
    /// 日志数据
    /// </summary>
    public struct LogData
    {
        public int LogID;
        public object LogMessage;
        public string LogTrace;
        public DateTime LogTime;
        public OutputLevel LogLevel;
    }

    /// <summary>
    /// 日志信息接口，通过实现该接口来自定义输出方式
    /// </summary>
    public interface ILogging
    {
        public void Log(LogData logData);
    }

    public class Logging
    {
        /// <summary>
        /// 根据输出类型存放日志输出控制
        /// </summary>
        private Dictionary<OutputType, ILogging> m_LogHandlers;
        /// <summary>
        /// 堆栈追踪，跳过前3个堆栈帧
        /// </summary>
        private StackTrace StackTrace { get { return new StackTrace(3, true); } }
        /// <summary>
        /// 忽略的输出级别，包含的不输出
        /// </summary>
        public List<OutputLevel> IgnoreLevel { get; set; }
        /// <summary>
        /// 输出级别控制，低于此级别的不输出
        /// </summary>
        public OutputLevel LogLevel { get; set; } = OutputLevel.On;
        /// <summary>
        /// 日志输出开关
        /// </summary>
        public bool LogSwitch { get; set; } = true;
        /// <summary>
        /// 日志输出回调
        /// </summary>
        public LogDelegate LogCallBack { get; set; }

        public Logging()
        {
            m_LogHandlers = new Dictionary<OutputType, ILogging>();
            IgnoreLevel = new List<OutputLevel>();
        }

        /// <summary>
        /// 添加某个类型的日志输出控制
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="outputType"></param>
        public void AddLogHandler(ILogging handler, OutputType outputType = OutputType.Console)
        {
            if (m_LogHandlers.ContainsKey(outputType))
            {
                m_LogHandlers[outputType] = handler;
            }
            else
            {
                m_LogHandlers.Add(outputType, handler);
            }
        }

        /// <summary>
        /// 移除某个类型的日志输出控制
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="outputType"></param>
        public void RemoveLogHandler(ILogging handler, OutputType outputType)
        {
            if (m_LogHandlers.ContainsKey(outputType))
            {
                m_LogHandlers.Remove(outputType);
            }
        }

        /// <summary>
        /// 添加忽略的日志级别
        /// </summary>
        /// <param name="level"></param>
        public void AddIgnoreLogLevel(OutputLevel level)
        {
            if (!IgnoreLevel.Contains(level))
                IgnoreLevel.Add(level);
        }

        /// <summary>
        /// 移除忽略的日志级别
        /// </summary>
        /// <param name="level"></param>
        public void RemoveIgnoreLogLevel(OutputLevel level)
        {
            if (IgnoreLevel.Contains(level))
                IgnoreLevel.Remove(level);
        }

        /// <summary>
        /// 获取相对Assets的路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string GetRelativePath(string path)
        {
            path = path.Substring(path.IndexOf("Assets"));
            path = path.Replace('\\', '/');
            return path;
        }

        /// <summary>
        /// 获取方法的参数
        /// </summary>
        /// <param name="methodBase"></param>
        /// <returns></returns>
        private string GetMethodParameters(System.Reflection.MethodBase methodBase)
        {
            StringBuilder stringBuilder = new StringBuilder(1);
            foreach (var item in methodBase.GetParameters())
            {
                stringBuilder.Append(item.ParameterType.Name).Append(" ");
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 获取简洁的堆栈帧信息
        /// </summary>
        /// <param name="stackFrame">堆栈帧</param>
        /// <returns></returns>
        private string GetStackFrameConcise(StackFrame stackFrame)
        {
            return string.Format("{0}:{1}({2}) (at {3}:{4,4})",
                stackFrame.GetMethod().DeclaringType.Name,
                stackFrame.GetMethod().Name,
                GetMethodParameters(stackFrame.GetMethod()),
                GetRelativePath(stackFrame.GetFileName()),
                stackFrame.GetFileLineNumber());
        }

        /// <summary>
        /// 获取格式化的调用堆栈信息
        /// </summary>
        /// <param name="stackTrace">堆栈跟踪信息</param>
        /// <returns></returns>
        private string TraceFormatting(StackTrace stackTrace)
        {
            StringBuilder stringBuilder = new StringBuilder(240);

            foreach (StackFrame stackFrame in stackTrace.GetFrames())
            {
                stringBuilder.Append(GetStackFrameConcise(stackFrame)).Append("\n\r");
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 获取格式化的异常的调用堆栈信息
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        private string ExceptionTraceFormatting(Exception exception)
        {
            StringBuilder stringBuilder = new StringBuilder(240);
            stringBuilder.Append("Error: " + exception.Message).Append("\n\r");
            if (!string.IsNullOrEmpty(exception.StackTrace))
            {
                stringBuilder.Append(exception.StackTrace);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 检查是否满足日志输出条件
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private bool CheckLogCondition(OutputLevel level)
        {
            return LogSwitch && level > LogLevel && !IgnoreLevel.Contains(level);
        }

        /// <summary>
        /// 各级别输出日志通过此方法输出
        /// </summary>
        /// <param name="outputLevel">日志级别</param>
        /// <param name="message">日志信息</param>
        /// <param name="trace">调用堆栈信息</param>
        private void LogBase(OutputLevel outputLevel, object message, string trace)
        {
            if (!CheckLogCondition(outputLevel)) return;
            LogData logData = new LogData();
            logData.LogLevel = outputLevel;
            logData.LogMessage = message;
            logData.LogTime = DateTime.Now;
            logData.LogTrace = trace;

            foreach (var key in m_LogHandlers.Keys)
            {
                m_LogHandlers[key].Log(logData);
            }
        }

        #region 各级别输出方法
        public void Debug(object message)
        {
            LogBase(OutputLevel.Debug, message, TraceFormatting(StackTrace));
        }

        public void Debug(object message, string trace)
        {
            LogBase(OutputLevel.Debug, message, trace);
        }

        public void Debug(string format, params object[] args)
        {
            LogBase(OutputLevel.Debug, string.Format(format, args), TraceFormatting(StackTrace));
        }

        public void Debug(object message, Exception exception)
        {
            LogBase(OutputLevel.Debug, message, ExceptionTraceFormatting(exception));
        }

        public void Info(object message)
        {
            LogBase(OutputLevel.Info, message, TraceFormatting(StackTrace));
        }

        public void Info(string format, params object[] args)
        {
            LogBase(OutputLevel.Info, string.Format(format, args), TraceFormatting(StackTrace));
        }

        public void Info(object message, string trace)
        {
            LogBase(OutputLevel.Info, message, TraceFormatting(StackTrace));
        }

        public void Info(object message, Exception exception)
        {
            LogBase(OutputLevel.Info, message, ExceptionTraceFormatting(exception));
        }

        public void Warn(object message)
        {
            LogBase(OutputLevel.Warning, message, TraceFormatting(StackTrace));
        }

        public void Warn(string format, params object[] args)
        {
            LogBase(OutputLevel.Warning, string.Format(format, args), TraceFormatting(StackTrace));
        }

        public void Warn(object message, string trace)
        {
            LogBase(OutputLevel.Warning, message, trace);
        }

        public void Warn(object message, Exception exception)
        {
            LogBase(OutputLevel.Warning, message, ExceptionTraceFormatting(exception));
        }

        public void Error(object message)
        {
            LogBase(OutputLevel.Error, message, TraceFormatting(StackTrace));
        }

        public void Error(string format, params object[] args)
        {
            LogBase(OutputLevel.Error, string.Format(format, args), TraceFormatting(StackTrace));
        }

        public void Error(object message, string trace)
        {
            LogBase(OutputLevel.Error, message, trace);
        }

        public void Error(object message, Exception exception)
        {
            LogBase(OutputLevel.Error, message, ExceptionTraceFormatting(exception));
        }
        #endregion
    }


    //----------------------------------------------------------------------------------------------------------
    //以下为具体实现


    /// <summary>
    /// 文件输出，可继承重写输出
    /// </summary>
    public class LogOutputTypeFile : ILogging
    {
        public string m_FilePath { get; set; } = System.IO.Directory.GetCurrentDirectory() + "/MyLog.txt";

        private StreamWriter m_Writer;

        public LogOutputTypeFile()
        {
            m_Writer = new StreamWriter(m_FilePath, false, Encoding.UTF8);
        }

        public LogOutputTypeFile(string filePath)
        {
            m_FilePath = filePath;
            m_Writer = new StreamWriter(m_FilePath, false, Encoding.UTF8);
        }

        public virtual void Log(LogData logData)
        {
            m_Writer.Write("\n\r-----------------------------------------------------------------------------------------------" +
                "\n\rID：" + logData.LogID +
                "\n\r时间：" + logData.LogTime +
                "\n\r级别：" + logData.LogLevel +
                "\n\r消息：" + logData.LogMessage +
                "\n\r堆栈：\n\r" + logData.LogTrace);
            m_Writer.Flush();
        }
    }

    /// <summary>
    /// Console输出，可继承重写输出
    /// </summary>
    public class LogOutputTypeConsole : ILogging
    {
        public virtual void Log(LogData logData)
        {

            UnityEngine.Debug.Log(GetLevelFormat(logData.LogLevel) + " " + logData.LogMessage + "\n\r堆栈: " + logData.LogTrace);
        }

        protected virtual string GetLevelFormat(OutputLevel LogLevel)
        {
            switch (LogLevel)
            {
                case OutputLevel.Debug:
                    return $"<color=#FFFFFF>{OutputLevel.Debug.ToString()}: </color>";
                case OutputLevel.Info:
                    return $"<color=#00E000>{OutputLevel.Info.ToString()}: </color>";
                case OutputLevel.Warning:
                    return $"<color=#FFFF00>{OutputLevel.Warning.ToString()}: </color>";
                case OutputLevel.Error:
                    return $"<color=#FF0000>{OutputLevel.Error.ToString()}: </color>";
            }
            return "Level Error!";
        }

        private static string GetStackTraceText()
        {
            var consoleWindowType = typeof(UnityEditor.EditorWindow).Assembly.GetType("UnityEditor.ConsoleWindow");
            var fieldInfo = consoleWindowType.GetField("ms_ConsoleWindow",
                System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.NonPublic);
            var consoleWindowInstance = fieldInfo.GetValue(null);

            if (consoleWindowInstance != null)
            {
                if ((object)UnityEditor.EditorWindow.focusedWindow == consoleWindowInstance)
                {
                    fieldInfo = consoleWindowType.GetField("m_ActiveText",
                        System.Reflection.BindingFlags.Instance |
                        System.Reflection.BindingFlags.NonPublic);
                    string activeText = fieldInfo.GetValue(consoleWindowInstance).ToString();
                    return activeText;
                }
            }
            return null;
        }

        [UnityEditor.Callbacks.OnOpenAssetAttribute(0)]
        private static bool OnOpenAsset(int instanceID, int line)
        {
            string stackTrace = GetStackTraceText();
            if (!string.IsNullOrEmpty(stackTrace) && stackTrace.Contains("LogTool.cs"))
            {
                var matches = System.Text.RegularExpressions.Regex.Match(stackTrace, @"\(at (.+)\)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                string pathLine = "";
                while (matches.Success)
                {
                    pathLine = matches.Groups[1].Value;

                    if (!pathLine.Contains("LogTool.cs"))
                    {
                        int splitIndex = pathLine.IndexOf(':');
                        //脚本路径
                        string path = pathLine.Substring(0, splitIndex);
                        //行号
                        line = System.Convert.ToInt32(pathLine.Substring(splitIndex + 1));

                        string fullPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Assets"));
                        fullPath = fullPath + path;
                        //跳转到目标代码指定行
                        UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(fullPath, line);
                        break;
                    }
                    matches = matches.NextMatch();
                }
                return true;
            }
            return false;
        }
    }

    public class LogTool
    {
        private static Logging m_Logging;
        private static LogOutputTypeFile m_TypeFile;
        private static LogOutputTypeConsole m_TypeConsole;
        static LogTool()
        {
            m_Logging = new Logging();
            m_TypeFile = new LogOutputTypeFile();
            m_TypeConsole = new LogOutputTypeConsole();
            m_Logging.AddLogHandler(m_TypeFile, OutputType.File);
            m_Logging.AddLogHandler(m_TypeConsole, OutputType.Console);
        }

        public static void LogDebug(object message)
        {
            m_Logging.Debug(message);
        }
        public static void LogDebug(object message, Exception exception)
        {
            m_Logging.Debug(message, exception);
        }
        public static void LogDebug(object message, string trace)
        {
            m_Logging.Debug(message, trace);
        }
        public static void LogDebug(string format, params object[] objs)
        {
            m_Logging.Debug(format, objs);
        }
        public static void LogInfo(object message)
        {
            m_Logging.Info(message);
        }
        public static void LogInfo(object message, Exception exception)
        {
            m_Logging.Info(message, exception);
        }
        public static void LogInfo(object message, string trace)
        {
            m_Logging.Info(message, trace);
        }
        public static void LogInfo(string format, params object[] objs)
        {
            m_Logging.Info(format, objs);
        }
        public static void LogWarn(object message)
        {
            m_Logging.Warn(message);
        }
        public static void LogWarn(object message, Exception exception)
        {
            m_Logging.Warn(message, exception);
        }
        public static void LogWarn(object message, string trace)
        {
            m_Logging.Warn(message, trace);
        }
        public static void LogWarn(string format, params object[] objs)
        {
            m_Logging.Warn(format, objs);
        }
        public static void LogError(object message)
        {
            m_Logging.Error(message);
        }
        public static void LogError(object message, Exception exception)
        {
            m_Logging.Error(message, exception);
        }
        public static void LogError(object message, string trace)
        {
            m_Logging.Error(message, trace);
        }
        public static void LogError(string format, params object[] objs)
        {
            m_Logging.Error(format, objs);
        }
    }

    #endregion
}
