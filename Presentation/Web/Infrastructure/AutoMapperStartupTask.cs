using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Infrastructure
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            AutoMapperConfiguration.Init();
        }

        public int Order
        {
            get
            {
                return 1;
            }
        }
    }
}