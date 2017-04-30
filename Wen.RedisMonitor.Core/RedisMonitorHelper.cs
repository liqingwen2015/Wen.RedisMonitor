using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Wen.RedisMonitor.Core.Models;

namespace Wen.RedisMonitor.Core
{
    public class RedisMonitorHelper
    {
        private readonly RedisHelper _redisHelper = new RedisHelper(_connectionString);
        private static string _connectionString;

        public RedisMonitorHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ServerInfo GetServerInfo(string hostAndPort)
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

        public string GetInfoRaw(string hostAndPort)
        {
            var server = GetServer(hostAndPort);
            return server.InfoRaw();
        }

        public IGrouping<string, KeyValuePair<string, string>>[] GetInfo(string hostAndPort)
        {
            var server = GetServer(hostAndPort);
            return server.Info();
        }

        private IServer GetServer(string hostAndPort)
        {
            if (string.IsNullOrEmpty(hostAndPort))
            {
                throw new Exception("参数为空");
            }

            return _redisHelper.GetServer(hostAndPort);
        }
    }
}
