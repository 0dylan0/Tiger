using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Test
{
    public class TestService: ITestService
    {
        public void Calculation()
        {
            //写具体计算逻辑和数据库插入操作
            Console.WriteLine("ok");
        }
    }
}
