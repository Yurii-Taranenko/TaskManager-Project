using TaskManager.Domain.Entities;

namespace TaskManager.Application.DTO
{
    public static class TaskResponseMapper
    {
        public static TaskResponse ToResponse(this TaskItem task) => 
            new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsCompleted,
            CreatedDate = task.CreatedDate,
            DueDate = task.DueDate,
            Priority = task.Priority
        };
    }
}
