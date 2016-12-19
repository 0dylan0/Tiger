using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Common
{
    public abstract class BaseEntity
    {
        public string InsertUser
        {
            get;
            set;
        }

        public DateTime? InsertDate
        {
            get;
            set;
        }

        public string UpdateUser
        {
            get;
            set;
        }

        public DateTime? UpdateDate
        {
            get;
            set;
        }
    }
}
