using log4net;
using Core.Infrastructure;
using Services.Tasks;

namespace WindowsService
{
    public class WindowsService
    {
        private static ILog _logger = LogManager.GetLogger(typeof(WindowsService));

        public void OnStart()
        {
            EngineContext.Initialize(false);
            _logger.Info("Service is started!");
        }

        public void OnStop()
        {
            TaskManager.Instance.Shutdown();
            _logger.Info("Service is stoped!");
        }
    }
}
