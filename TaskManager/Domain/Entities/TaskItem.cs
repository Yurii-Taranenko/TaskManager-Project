using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public bool IsCompleted { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? DueDate { get; private set; }
        public TaskPriorityType? Priority { get; private set; }

        private TaskItem() { }
        public TaskItem(string title, string? description, DateTime? dueDate, TaskPriorityType? priority)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            IsCompleted = false;
            CreatedAt = DateTime.UtcNow;
            DueDate = dueDate;
            Priority = priority;
        }

        public void Complete()
        {
            if (IsCompleted)
                throw new Exception("Task is already completed.");

            IsCompleted = true;
        }

        public void UpdateTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new Exception("Title cannot be empty.");
            Title = newTitle;
        }
    }
}
