using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Infrastructure
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {

        }

        public int Order
        {
            get
            {
                return 0;
            }
        }
    }
}
