using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Logging
{
    public class ConsoleSqlLogger
    {
        public static void Write(string sql)
        {
            Debug.Write(sql);
        }
    }
}
