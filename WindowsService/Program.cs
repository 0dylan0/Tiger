using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace WindowsService
{
    class Program
    {
        private static ILog _logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            HostFactory.Run(conf =>
            {
                conf.Service<WindowsService>(service =>
                {
                    service.ConstructUsing(name => new WindowsService());
                    service.WhenStarted(tc => tc.OnStart());
                    service.WhenStopped(tc => tc.OnStop());
                });

                conf.RunAsLocalSystem();
                conf.OnException(e => _logger.Error(e));
                conf.SetDescription("InventoryManagement 后台服务");
                conf.SetDisplayName("InventoryManagementService");
                conf.SetServiceName("InventoryManagementService");
            });
        }
    }
}
