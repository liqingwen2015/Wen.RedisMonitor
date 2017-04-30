using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wen.RedisMonitor.Core.Models;

namespace Wen.RedisMonitor.WebUI.Models
{
    public class IndexViewModel
    {
        public ServerInfo ServerInfo { get; set; }

        public string ServerInfoRaw { get; set; }

        public IEnumerable<IGrouping<string, KeyValuePair<string, string>>> ServerDetailInfos { get; set; }
    }
}