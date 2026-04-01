using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllTasksAsync();
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task AddTaskAsync(TaskItem task);
        Task<bool> UpdateTaskAsync(TaskItem task);
        Task<bool> DeleteTaskAsync(Guid id);
    }
}
