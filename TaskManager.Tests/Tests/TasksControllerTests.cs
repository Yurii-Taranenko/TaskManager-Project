using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManager.Application.DTO;
using TaskManager.Controllers;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Exceptions;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Tests.Tests
{
    public class TasksControllerTests
    {
        private readonly Mock<ITaskRepository> _repositoryMock;

        private readonly TasksController tasksController;

        public TasksControllerTests()
        {
            _repositoryMock = new Mock<ITaskRepository>();
            tasksController = new TasksController(_repositoryMock.Object);
        }

        //Get All Tests

        [Fact]
        public async Task GetAllTasks_ReturnAllTasksWithOk()
        {
            var tasks = new List<TaskItem> { new TaskItem("Task1", null, null, null) };

            _repositoryMock.Setup(r => r.GetAllTasksAsync()).ReturnsAsync(tasks);

            var result = await tasksController.GetAllTasksAsync();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllTasks_EmptyList_ReturnOk()
        {
            _repositoryMock.Setup(r => r.GetAllTasksAsync()).ReturnsAsync([]);

            var result = await tasksController.GetAllTasksAsync();

            var isType = Assert.IsType<OkObjectResult>(result);
            
            Assert.NotNull(isType.Value);
        }

        // Get by ID tests

        [Fact]
        public async Task GetById_Exists_ReturnOk()
        {
            var task = new TaskItem("Task1", null, null, null);
            _repositoryMock.Setup(r => r.GetByIdAsync(task.Id)).ReturnsAsync(task);

            var result = await tasksController.GetByIdAsync(task.Id);

            var isType = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetById_NotExists_ReturnNotFoundException()
        {
            var id = Guid.NewGuid();

            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((TaskItem?)null);

            await Assert.ThrowsAsync<NotFoundException>(
                () => tasksController.GetByIdAsync(id));
        }

        // CREATE

        [Fact]
        public async Task Create_ValidRequest_ReturnsCreatedAtRoute()
        {
            var request = new CreateTaskRequest()
            {
                Title = "Test Task",
                Description = "Test Description",
                DueDate = DateTime.UtcNow.AddDays(7),
                Priority = TaskPriorityType.Medium
            };

            var result = await tasksController.CreateTaskAsync(request);
        
            var created = Assert.IsType<CreatedAtRouteResult>(result);

            Assert.Equal(nameof(tasksController.GetByIdAsync), created.RouteName);

            _repositoryMock.Verify(r=>r.AddTaskAsync(It.IsAny<TaskItem>()), Times.Once);
        }
    }
}
