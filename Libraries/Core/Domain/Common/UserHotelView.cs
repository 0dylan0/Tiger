using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Common
{
    public class UserHotelView : BaseEntity
    {
        public string UserCode
        {
            get;
            set;
        }

        public string HotelCode
        {
            get;
            set;
        }

        public Users User
        {
            get;
            set;
        }

        public Hotel Hotel
        {
            get;
            set;
        }
    }
}
