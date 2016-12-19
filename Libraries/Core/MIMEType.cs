using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    // 使用 MimeMapping 通过后缀获取 MIME Type 好像不太方便

    public static class MIMEType
    {
        public const string XLSX = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public const string PDF = "application/pdf";
    }
}
