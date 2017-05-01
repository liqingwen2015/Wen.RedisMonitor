namespace Wen.RedisMonitor.Core.Log
{
    /// <summary>
    /// 日志信息类
    /// </summary>
    public class LogInfo
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 日志等级
        /// </summary>
        public LogLevel LogLevel { get; set; }
    }
}