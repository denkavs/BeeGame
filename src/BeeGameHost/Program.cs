using BeeGameHost.Infrastructure;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeGameHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string appServiceUri = HostConfigMgr.GetBaseHostUrl();
            WebApp.Start<Startup>(appServiceUri);
            Console.WriteLine("Press any keys to stop BeeGame host.");
            Console.ReadLine();
        }
    }
}
