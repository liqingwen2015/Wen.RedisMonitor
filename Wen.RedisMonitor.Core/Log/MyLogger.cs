#region

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

#endregion

namespace Wen.RedisMonitor.Core.Log
{
    /// <summary>
    /// 日志记录器类
    /// </summary>
    public class MyLogger
    {
        private static readonly MyLogger Logger;

        private static readonly ILog Log;
        private static readonly ConcurrentQueue<LogInfo> Queue;

        static MyLogger()
        {
            // 设置日志配置文件路径
            XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")));

            Logger = new MyLogger();
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            Queue = new ConcurrentQueue<LogInfo>();
            Task.Run(() => { WriteLog(); });
        }

        private MyLogger()
        {
        }

        public static MyLogger GetMyLogger()
        {
            return Logger;
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        private static void WriteLog()
        {
            while (true)
                while (Queue.Count > 0)
                {
                    LogInfo logInfo;
                    if (!Queue.TryDequeue(out logInfo)) continue;

                    switch (logInfo.LogLevel)
                    {
                        case LogLevel.Debug:
                            Log.Debug(logInfo.Message);
                            break;
                        case LogLevel.Info:
                            Log.Info(logInfo.Message);
                            break;
                        case LogLevel.Warn:
                            Log.Warn(logInfo.Message);
                            break;
                        case LogLevel.Error:
                            Log.Error(logInfo.Message);
                            break;
                        case LogLevel.Fatal:
                            Log.Fatal(logInfo.Message);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
        }

        /// <summary>
        /// 日志入列
        /// </summary>
        /// <param name="info"></param>
        private static void EnqueueLog(LogInfo info)
        {
            switch (info.LogLevel)
            {
                case LogLevel.Debug:
                    if (!Log.IsDebugEnabled)
                        return;
                    break;
                case LogLevel.Info:
                    if (!Log.IsInfoEnabled)
                        return;
                    break;
                case LogLevel.Warn:
                    if (!Log.IsWarnEnabled)
                        return;
                    break;
                case LogLevel.Error:
                    if (!Log.IsErrorEnabled)
                        return;
                    break;
                case LogLevel.Fatal:
                    if (!Log.IsFatalEnabled)
                        return;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Queue.Enqueue(info);
        }

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="msg"></param>
        public static void Debug(string msg)
        {
            EnqueueLog(new LogInfo
            {
                LogLevel = LogLevel.Debug,
                Message = msg
            });
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            EnqueueLog(new LogInfo
            {
                LogLevel = LogLevel.Error,
                Message = msg
            });
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="msg"></param>
        public static void Fatal(string msg)
        {
            EnqueueLog(new LogInfo
            {
                LogLevel = LogLevel.Fatal,
                Message = msg
            });
        }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            EnqueueLog(new LogInfo
            {
                LogLevel = LogLevel.Info,
                Message = msg
            });
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(string msg)
        {
            EnqueueLog(new LogInfo
            {
                LogLevel = LogLevel.Warn,
                Message = msg
            });
        }
    }
}