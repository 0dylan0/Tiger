using Core.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping.Tasks
{
    public class ScheduleTaskMap : EntityTypeConfiguration<ScheduleTask>
    {
        public ScheduleTaskMap()
        {
            ToTable("ScheduleTask");

            HasKey(t => t.Id);

            Property(p => p.Name).IsRequired();
            //Property(p => p.Type).IsRequired();
            Property(p => p.CronValue).IsRequired();
        }
    }
}
