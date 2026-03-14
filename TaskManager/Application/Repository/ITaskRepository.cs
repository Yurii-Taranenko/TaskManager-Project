using TaskManager.Domain.Entities;

namespace TaskManager.Application.Repository
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllTasksAsync();
        Task AddTaskAsync(TaskItem task);
    }
}
