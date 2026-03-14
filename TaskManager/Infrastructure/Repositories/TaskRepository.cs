using TaskManager.Application.Repository;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private static List<TaskItem> _tasks = new List<TaskItem>();
    
            public Task<List<TaskItem>> GetAllTasksAsync()
            {
                return Task.FromResult(_tasks);
            }
    
            public Task AddTaskAsync(TaskItem task)
            {
                _tasks.Add(task);
                return Task.CompletedTask;
        }
    }
}
