using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTO;
using TaskManager.Application.Repository;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Exceptions;

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
        public async Task<IActionResult> GetAllTasksAsync()
        {
            var tasks = await _repository.GetAllTasksAsync();
            var response = tasks.Select(task => task.ToResponse());
            return Ok(response);
        }

        [HttpGet("{id:guid}", Name = nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var task = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Task with id '{id}' not found.");

            return Ok(task.ToResponse());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskAsync(CreateTaskRequest request)
        {
            var newItem = new TaskItem(request.Title, request.Description, request.DueDate, request.Priority);
            await _repository.AddTaskAsync(newItem);
            return CreatedAtRoute(
                nameof(GetByIdAsync),
                new { id = newItem.Id },
                newItem.ToResponse());
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTaskAsync(Guid id)
        {
            var result = await _repository.DeleteTaskAsync(id);

            if (!result)
                throw new NotFoundException($"Task with id '{id}' not found.");

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTask(Guid id, UpdateTaskRequest request)
        {
            var task = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Task with id '{id}' not found.");

            if (!string.IsNullOrWhiteSpace(request.Title))
                task.UpdateTitle(request.Title);

            if (request.Description is not null)
                task.UpdateDescription(request.Description);

            if (request.DueDate.HasValue)
                task.UpdateDueDate(request.DueDate);

            if (request.Priority.HasValue)
                task.UpdatePriority(request.Priority);

            await _repository.UpdateTaskAsync(task);
            return Ok(task.ToResponse());
        }

        [HttpPost("{id:guid}/complete")]
        public async Task<IActionResult> SetCompleteTask(Guid id)
        {
            var task = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Task with id '{id}' not found.");

            task.SetComplete();
            await _repository.UpdateTaskAsync(task);
            return Ok(task.ToResponse());
        }

        [HttpPost("{id:guid}/incomplete")]
        public async Task<IActionResult> SetInCompleteTask(Guid id)
        {
            var task = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Task with id '{id}' not found.");

            task.SetIncomplete();
            await _repository.UpdateTaskAsync(task);
            return Ok(task.ToResponse());
        }
    }
}
