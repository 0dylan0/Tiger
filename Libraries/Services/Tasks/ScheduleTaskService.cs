using Core.Data;
using Core.Domain.Tasks;
using Quartz;
using System.Collections.Generic;
using System.Linq;
using Core.Page;
using System;


namespace Services.Tasks
{
    public class ScheduleTaskService //: IScheduleTaskService
    {
        private readonly IRepository<ScheduleTask> _taskRepository;
        private readonly IScheduler _scheduler;

        public ScheduleTaskService(IRepository<ScheduleTask> taskRepository,
            IScheduler scheduler)
        {
            _taskRepository = taskRepository;
            _scheduler = scheduler;
        }

        public virtual void DeleteTask(ScheduleTask task)
        {
            _taskRepository.Delete(task);
        }

        public virtual ScheduleTask GetTaskById(int taskId)
        {
            return _taskRepository.GetById(taskId);
        }

        public virtual ScheduleTask GetTaskByType(string type)
        {
            return _taskRepository.Table.FirstOrDefault(st => st.Type == type);
        }

        public virtual IEnumerable<ScheduleTask> GetAllTasks(bool showHidden = false)
        {
            IQueryable<ScheduleTask> query = _taskRepository.Table;
            if (!showHidden)
            {
                query = query.Where(t => t.Enabled);
            }

            List<ScheduleTask> tasks = query.ToList();
            return tasks;
        }

        public virtual void InsertTask(ScheduleTask task)
        {
            _taskRepository.Insert(task);
        }

        public virtual void UpdateTask(ScheduleTask task)
        {
            _taskRepository.Update(task);
            string triggerKey = $"{task.Id}-{task.Type}";
            ITrigger trigger = TriggerBuilder.Create().WithCronSchedule(task.CronValue).WithIdentity(triggerKey).Build();
            _scheduler.RescheduleJob(new TriggerKey(triggerKey), trigger);
        }

        public IPagedList<ScheduleTask> GetList(int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            var res = _taskRepository.Table;
            return new PagedList<ScheduleTask>(res, pageIndex, pageSize, sortExpression);
        }

        public ScheduleTask GetById(Guid id)
        {
            return _taskRepository.GetById(id);
        }

        public void Update(ScheduleTask scheduleTask)
        {
            _taskRepository.Update(scheduleTask);
        }

        public IList<ScheduleTask> GetScheduleTaskList()
        {
            var res = _taskRepository.Table;

            return res.ToList();
        }
    }
}
