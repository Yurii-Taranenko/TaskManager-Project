using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Repository;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddTaskAsync(TaskItem task)
        {
            await _context.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateTaskAsync(TaskItem task)
        {
            _context.Update(task);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (task == null)
            {
                return false;
            }
            _context.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
