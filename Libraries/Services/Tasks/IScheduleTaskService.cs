using Core.Domain.Tasks;
using Core.Page;
using System;
using System.Collections.Generic;
namespace Services.Tasks
{
    public interface IScheduleTaskService
    {
        IEnumerable<ScheduleTask> GetAllTasks(bool showHidden = false);

        void DeleteTask(ScheduleTask task);

        ScheduleTask GetTaskById(int taskId);

        ScheduleTask GetTaskByType(string type);

        void InsertTask(ScheduleTask task);

        void UpdateTask(ScheduleTask task);

        IPagedList<ScheduleTask> GetList(int pageIndex, int pageSize, string sortExpression);

        ScheduleTask GetById(Guid id);

        void Update(ScheduleTask scheduleTask);

        IList<ScheduleTask> GetScheduleTaskList();
    }
}
