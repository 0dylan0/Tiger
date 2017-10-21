using Core.Infrastructure;
using Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService.Infrastructure
{
    public class InitializeStartupTask: IStartupTask
    {
        public int Order => 1;

        public void Execute()
        {
            TaskManager.Instance.Initialize();
            TaskManager.Instance.Start();
        }
    }
}
