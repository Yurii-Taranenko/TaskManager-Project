
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Tests.Tests
{
    public class TaskItemTests
    {
        [Fact]
        public void Constructor_ValidData_CreatesTask()
        {
            var task = new TaskItem("TestTitle", "TestDescription", DateTime.UtcNow.AddDays(1), TaskPriorityType.High);

            Assert.Equal("TestTitle", task.Title);
            Assert.Equal("TestDescription", task.Description);
            Assert.Equal(TaskPriorityType.High, task.Priority);
            Assert.False(task.IsCompleted);
            Assert.NotEqual(Guid.Empty, task.Id);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("    ")]
        public void Constructor_EmptyTitle_ThrowsDomainException(string? title)
        {
            var ex = Assert.Throws<DomainException>(() => new TaskItem(title!, null, null, null));

            Assert.Equal("Title cannot be empty.", ex.Message);
        }

        [Fact]
        public void Constructor_TitleTooLong_ThrowsDomainException()
        {
            var ex = Assert.Throws<DomainException>(() => new TaskItem(new string('a', 101), null, null, null));

            Assert.Equal("Title must be less than 100 characters.", ex.Message);
        }

        [Fact]
        public void Constructor_NullPriority_DefaultToLow()
        {
            var task = new TaskItem("Test", null, null, null);

            Assert.Equal(task.Priority, TaskPriorityType.Low);
        }

        [Fact]
        public void SetComplete_CheckIsTrue()
        {
            var task = new TaskItem("Test", null, null, null);

            task.SetComplete();

            Assert.True(task.IsCompleted);
        }

        [Fact]
        public void SetComplete_WhenAlreadyCompleted_ThrowDomainException()
        {
            var task = new TaskItem("Test", null, null, null);

            task.SetComplete();

            var ex = Assert.Throws<DomainException>(()=> task.SetComplete());

            Assert.Equal("Task is already completed.", ex.Message);
        }

        [Fact]
        public void SetInComplete_CheckIsFalse()
        {
            var task = new TaskItem("Test", null, null, null);

            task.SetComplete();
            task.SetIncomplete();

            Assert.False(task.IsCompleted);
        }

        [Fact]
        public void SetInComplete_WhenAlreadyInCompleted_ThrowDomainException()
        {
            var task = new TaskItem("Test", null, null, null);

            var ex = Assert.Throws<DomainException>(() => task.SetIncomplete());

            Assert.Equal("Task is already incomplete.", ex.Message);
        }

        [Fact]
        public void UpdateTitle_ChangesTitle()
        {
            var task = new TaskItem("Test", null, null, null);

            task.UpdateTitle("New Title");

            Assert.Equal("New Title", task.Title);
        }

        [Fact]
        public void UpdateDescription_ChangesDescription()
        {
            var task = new TaskItem("Test", null, null, null);

            task.UpdateDescription("New Description");

            Assert.Equal("New Description", task.Description);
        }
        [Fact]
        public void UpdatePriority_ChangesPriority()
        {
            var task = new TaskItem("Test", null, null, null);

            task.UpdatePriority(TaskPriorityType.Critical);

            Assert.Equal(task.Priority, TaskPriorityType.Critical);
        }
    }
}
