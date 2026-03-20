using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTO
{
    public class UpdateTaskRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriorityType? Priority { get; set; }
    }
}
