using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Common
{
    public class UserHotelRange : BaseEntity
    {
        //系统用户代码
        public string UserCode { get; set; }
        public virtual Users User { get; set; }

        //酒店代码
        public string HotelCode { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}
