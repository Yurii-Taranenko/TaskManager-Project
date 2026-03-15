using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTO;
using TaskManager.Application.Repository;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repository;

        public TasksController(ITaskRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var response = await _repository.GetAllTasksAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskRequest request)
        {
            TaskItem newItem = new TaskItem(request.Title, request.Description, request.DueDate, request.Priority);
            
            await _repository.AddTaskAsync(newItem);

            return Ok();
        }
    }
}
