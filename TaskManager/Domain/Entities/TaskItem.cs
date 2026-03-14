namespace TaskManager.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public bool IsCompleted { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public TaskItem(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
            IsCompleted = false;
            CreatedAt = DateTime.UtcNow;
        }

        public void Complete()
        {
            if(IsCompleted)
                throw new Exception("Task is already completed.");

            IsCompleted = true;
        }

        public void UpdateTitle(string newTitle)
        {
            if(string.IsNullOrWhiteSpace(newTitle))
                throw new Exception("Title cannot be empty.");
            Title = newTitle;
        }
    }
}
