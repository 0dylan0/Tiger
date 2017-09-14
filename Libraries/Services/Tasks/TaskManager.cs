using Core.Domain.Tasks;
using Core.Infrastructure;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Services.Tasks
{
    public class TaskManager
    {
        private static readonly TaskManager _taskManager = new TaskManager();

        private TaskManager()
        {
        }

        public static TaskManager Instance
        {
            get
            {
                return _taskManager;
            }
        }

        public void Initialize()
        {
            var taskService = EngineContext.Current.Resolve<IScheduleTaskService>();
            List<ScheduleTask> scheduleTasks = taskService.GetAllTasks().ToList();

            var scheduler = EngineContext.Current.Resolve<IScheduler>();
            foreach (ScheduleTask scheduleTask in scheduleTasks)
            {

                Type type = Type.GetType(scheduleTask.Type);

                IJobDetail workJobDetail = JobBuilder.Create(type).Build();
                ITrigger workTrigger = TriggerBuilder.Create().WithCronSchedule(scheduleTask.CronValue).WithIdentity(String.Format("{0}-{1}", scheduleTask.Id, scheduleTask.Type)).Build();
                scheduler.ScheduleJob(workJobDetail, workTrigger);
            }
        }

        public void Start()
        {
            var scheduler = EngineContext.Current.Resolve<IScheduler>();
            scheduler.Start();
        }

        public void Shutdown()
        {
            var scheduler = EngineContext.Current.Resolve<IScheduler>();
            scheduler.Shutdown();
        }
    }
}
