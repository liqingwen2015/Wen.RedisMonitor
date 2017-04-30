using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wen.RedisMonitor.Core.Models
{
    /// <summary>
    /// 服务器信息
    /// </summary>
    public class ServerInfo
    {

        /// <summary>
        /// 标识
        /// </summary>
        public string Id => Guid.NewGuid().ToString("N");

        /// <summary>
        /// 主机和端口号
        /// </summary>
        public string HostAndPort { get; set; }

        /// <summary>
        /// 是否连接成功
        /// </summary>
        public bool IsConnected { get; set; }

        /// <summary>
        /// 响应时间
        /// </summary>
        public TimeSpan Pings { get; set; }
    }
}
