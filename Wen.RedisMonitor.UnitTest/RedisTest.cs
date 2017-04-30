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
    }
}
