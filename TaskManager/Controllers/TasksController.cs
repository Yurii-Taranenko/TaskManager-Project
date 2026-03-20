using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTO;
using TaskManager.Application.Repository;
using TaskManager.Domain.Entities;

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
            try
            {
                var tasks = await _repository.GetAllTasksAsync();
                var response = tasks.Select(task => task.ToResponse());
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:guid}", Name = nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var task = await _repository.GetByIdAsync(id);

                if (task == null)
                {
                    return NotFound();
                }

                return Ok(task.ToResponse());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskAsync(CreateTaskRequest request)
        {
            try
            {
                TaskItem newItem = new TaskItem(request.Title, request.Description, request.DueDate, request.Priority);
                await _repository.AddTaskAsync(newItem);
                return CreatedAtRoute(
                    nameof(GetByIdAsync),
                    new { id = newItem.Id },
                    newItem.ToResponse());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTaskAsync(Guid id)
        {
            try
            {
                var result = await _repository.DeleteTaskAsync(id);

                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTask(Guid id, UpdateTaskRequest request)
        {
            var task = await _repository.GetByIdAsync(id);

            if (task is null)
                return NotFound();

            try
            {
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id:guid}/complete")]
        public async Task<IActionResult> SetCompleteTask(Guid id)
        {
            var task = await _repository.GetByIdAsync(id);

            if (task is null)
                return NotFound();

            try
            {
                task.SetComplete();
                await _repository.UpdateTaskAsync(task);
                return Ok(task.ToResponse());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id:guid}/incomplete")]
        public async Task<IActionResult> SetInCompleteTask(Guid id)
        {
            var task = await _repository.GetByIdAsync(id);

            if (task is null)
                return NotFound();

            try
            {
                task.SetIncomplete();
                await _repository.UpdateTaskAsync(task);
                return Ok(task.ToResponse());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
