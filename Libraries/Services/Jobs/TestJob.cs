using Core;
using Core.Infrastructure;
using log4net;
using Quartz;
using Services.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Jobs
{
    [DisallowConcurrentExecution]
    public class TestJob : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(TestJob));

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                var _workContext = EngineContext.Current.ContainerManager.Resolve<IWorkContext>();
                var _testService = EngineContext.Current.ContainerManager.Resolve<ITestService>();
                Console.WriteLine("正常执行");
                _testService.Calculation();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

    }
}
