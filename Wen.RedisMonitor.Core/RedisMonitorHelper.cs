using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Wen.RedisMonitor.Core.Log;
using Wen.RedisMonitor.Core.Models;

namespace Wen.RedisMonitor.Core
{
    /// <summary>
    /// Redis 监控 Helper
    /// </summary>
    public class RedisMonitorHelper
    {
        private readonly RedisHelper _redisHelper;

        public RedisMonitorHelper(string connectionString)
        {
            try
            {
                _redisHelper = new RedisHelper(connectionString);
            }
            catch (Exception e)
            {
                MyLogger.Error($"{nameof(RedisMonitorHelper)} \r\n 异常：{e}");
                throw;
            }
        }

        /// <summary>
        /// 获取服务器信息对象
        /// </summary>
        /// <param name="hostAndPort"></param>
        /// <returns></returns>
        public ServerInfo GetServerInfo(string hostAndPort)
        {
            try
            {
                var server = GetServer(hostAndPort);

                var serverInfo = new ServerInfo()
                {
                    HostAndPort = server.EndPoint.ToString(),
                    IsConnected = server.IsConnected,
                    Pings = server.Ping()
                };

                return serverInfo;
            }
            catch (Exception e)
            {
                MyLogger.Error($"{nameof(GetServerInfo)} \r\n 异常：{e}");
                throw;
            }
        }

        /// <summary>
        /// 获取原始信息
        /// </summary>
        /// <param name="hostAndPort"></param>
        /// <returns></returns>
        public string GetInfoRaw(string hostAndPort)
        {
            try
            {
                var server = GetServer(hostAndPort);
                return server.InfoRaw();
            }
            catch (Exception e)
            {
                MyLogger.Error($"{nameof(hostAndPort)} \r\n  异常：{e}");
                throw;
            }

        }

        /// <summary>
        /// 获取服务器信息集合
        /// </summary>
        /// <param name="hostAndPort"></param>
        /// <returns></returns>
        public IGrouping<string, KeyValuePair<string, string>>[] GetInfo(string hostAndPort)
        {
            try
            {
                var server = GetServer(hostAndPort);
                return server.Info();
            }
            catch (Exception e)
            {
                MyLogger.Error($"{nameof(GetInfo)} \r\n  异常：{e}");
                throw;
            }

        }

        /// <summary>
        /// 获取 Server 对象
        /// </summary>
        /// <param name="hostAndPort"></param>
        /// <returns></returns>
        private IServer GetServer(string hostAndPort)
        {
            if (string.IsNullOrEmpty(hostAndPort))
            {
                throw new Exception("参数为空");
            }

            try
            {
                return _redisHelper.GetServer(hostAndPort);
            }
            catch (Exception e)
            {
                MyLogger.Error($"{nameof(GetServer)} \r\n  异常：{e}");
                throw;
            }
        }

        /// <summary>
        /// 获取客户信息集合
        /// </summary>
        /// <param name="hostAndPort"></param>
        /// <returns></returns>
        public IEnumerable<RedisClientInfo> GetClients(string hostAndPort)
        {
            try
            {
                var server = GetServer(hostAndPort);
                return server.ClientList().Select(ConvertClientInfoToRedisClientInfo);
            }
            catch (Exception e)
            {
                MyLogger.Error($"{nameof(GetClients)} \r\n  异常：{e}");
                throw;
            }
        }

        /// <summary>
        /// 将 ClientInfo 转换为 RedisClientInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private static RedisClientInfo ConvertClientInfoToRedisClientInfo(ClientInfo info)
        {
            return new RedisClientInfo()
            {
                AgeSeconds = info.AgeSeconds,
                Database = info.Database,
                Host = info.Host,
                IdleSeconds = info.IdleSeconds,
                LastCommand = info.LastCommand,
                PatternSubscriptionCount = info.PatternSubscriptionCount,
                Port = info.Port,
                Raw = info.Raw,
                SubscriptionCount = info.SubscriptionCount,
                TransactionCommandLength = info.TransactionCommandLength
            };
        }
    }
}
