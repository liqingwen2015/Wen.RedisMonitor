using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wen.RedisMonitor.Core;

namespace Wen.RedisMonitor.UnitTest
{
    [TestClass]
    public class RedisTest
    {
        private static readonly RedisMonitorHelper RedisMonitorHelper = new RedisMonitorHelper("127.0.0.1:6379,allowAdmin=true");

        [TestMethod]
        public void GetServerInfo()
        {
            var serverInfo = RedisMonitorHelper.GetServerInfo("127.0.0.1:6379");
            //Console.WriteLine(serverInfo.IsConnected);
            //Console.WriteLine(serverInfo.HostAndPort);
            //Console.WriteLine(serverInfo.Id);
            Console.WriteLine(serverInfo.Pings.TotalMilliseconds);
        }

        [TestMethod]
        public void GetInfoRaw()
        {
            var infoRaw = RedisMonitorHelper.GetInfoRaw("127.0.0.1:6379");
            Console.WriteLine(infoRaw);
        }

        [TestMethod]
        public void GetInfo()
        {
            var info = RedisMonitorHelper.GetInfo("127.0.0.1:6379");
            foreach (var keyValuePair in info)
            {
                Console.WriteLine(keyValuePair.Key);
                Console.WriteLine("======");

                foreach (var valuePair in keyValuePair)
                {
                    Console.WriteLine("-----");
                    Console.WriteLine(valuePair.Key);
                    Console.WriteLine(valuePair.Value);
                }
            }

            Console.WriteLine(info);
        }


        [TestMethod]
        public void GetRedisClientInfos()
        {
            var clients = RedisMonitorHelper.GetClients("127.0.0.1:6379");
            foreach (var redisClientInfo in clients)
            {
                Console.WriteLine("==========");
                Console.WriteLine(redisClientInfo.AgeSeconds);
                Console.WriteLine(redisClientInfo.Database);
                Console.WriteLine(redisClientInfo.Host);
                Console.WriteLine(redisClientInfo.IdleSeconds);
                Console.WriteLine(redisClientInfo.LastCommand);
                Console.WriteLine(redisClientInfo.PatternSubscriptionCount);
                Console.WriteLine(redisClientInfo.Port);
                Console.WriteLine(redisClientInfo.SubscriptionCount);
                Console.WriteLine(redisClientInfo.TransactionCommandLength);
                Console.WriteLine("==========");

            }

        }
    }
}
