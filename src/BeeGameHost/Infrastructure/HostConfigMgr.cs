using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeGameHost.Infrastructure
{
    static class HostConfigMgr
    {
        public static string GetBaseHostUrl()
        {
            return (string)ConfigurationManager.AppSettings["HostBaseUrl"];
        }
    }
}
