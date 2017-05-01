using System;
using System.Collections.Generic;
using System.Configuration;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wen.RedisMonitor.Core;
using Wen.RedisMonitor.WebUI.Models;

namespace Wen.RedisMonitor.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private static readonly string RedisConnectionString = ConfigurationManager.ConnectionStrings["RedisConnectionString"].ToString();
        private static readonly string RedisHostAndPort = RedisConnectionString.Split(',')[0];

        private readonly RedisMonitorHelper _redisMonitorHelper = new RedisMonitorHelper(RedisConnectionString);

        // GET: Home
        public ActionResult Index()
        {
            var serverInfo = _redisMonitorHelper.GetServerInfo(RedisHostAndPort);

            var vm = new IndexViewModel()
            {
                ServerInfo = serverInfo,
            };

            return View(vm);
        }

        public PartialViewResult BaseInfoRaw()
        {
            var infoRaw = _redisMonitorHelper.GetInfoRaw(RedisHostAndPort).Replace("\r\n","<br />");

            return PartialView((object) infoRaw);
        }

        public PartialViewResult DetailInfos()
        {
            var detailInfos = _redisMonitorHelper.GetInfo(RedisHostAndPort);

            return PartialView(detailInfos);
        }

        public PartialViewResult ClientInfos()
        {
            var detailInfos = _redisMonitorHelper.GetClients(RedisHostAndPort);

            return PartialView(detailInfos);
        }

    }
}