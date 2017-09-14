using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Tasks
{
    public partial class ScheduleTask : BaseEntity
    {
        public Guid Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string CronValue
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public bool Enabled
        {
            get;
            set;
        }

        public bool StopOnError
        {
            get;
            set;
        }

        public DateTime? LastStart
        {
            get;
            set;
        }

        public DateTime? LastEnd
        {
            get;
            set;
        }

        public DateTime? LastSuccess
        {
            get;
            set;
        }
    }
}