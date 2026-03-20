
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
            var task = new TaskItem("TestTitle", "TestDescription", DateTime.UtcNow.AddDays(1), TaskPriorityType.High)

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
    }
}
