using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class TaskItem
    {
        private const TaskPriorityType DEFAULT_PRIORITY = TaskPriorityType.Low;

        public Guid Id { get; private set; }
        public bool IsCompleted { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? DueDate { get; private set; }
        public TaskPriorityType? Priority { get; private set; }

        public TaskItem(string title, string? description, DateTime? dueDate, TaskPriorityType? priority)
        {
            // Domain validation
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            
            if (title.Length > 100)
                throw new ArgumentException("Title must be less than 100 characters.", nameof(title));

            Id = Guid.NewGuid();
            IsCompleted = false;
            UpdateTitle(title);
            UpdateDescription(description);
            UpdateCreateDate();
            UpdateDueDate(dueDate);
            UpdatePriority(priority);
        }

        public void UpdateTitle(string newTitle)
        {
            Title = newTitle;
        }

        public void UpdateDescription(string? newDescription)
        {
            Description = newDescription;
        }

        public void UpdateCreateDate()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public void UpdateDueDate(DateTime? newDueDate)
        {
            DueDate = newDueDate;
        }

        public void UpdatePriority(TaskPriorityType? newPriority)
        {
            Priority = newPriority ?? DEFAULT_PRIORITY;
        }

        public void SetComplete()
        {
            if (IsCompleted)
                throw new Exception("Task is already completed.");

            IsCompleted = true;
        }

        public void SetIncomplete()
        {
            if (!IsCompleted)
                throw new Exception("Task is already incomplete.");
            IsCompleted = false;
        }
    }
}
