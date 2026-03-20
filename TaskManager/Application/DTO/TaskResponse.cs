using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTO
{
    public class TaskResponse
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string? Description { get; init; }
        public bool IsCompleted { get; init; }
        public DateTime CreatedDate { get; init; }
        public DateTime? DueDate { get; init; }
        public TaskPriorityType? Priority { get; init; }
    }
}
