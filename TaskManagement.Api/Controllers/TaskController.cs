using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.UseCases.Create.Input;
using TaskManagement.Application.UseCases.Update.Input;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController(
        IGetByIdUseCase getByIdUseCase,
        ICreateTaskUseCase createTaskUseCase,
        IGetAllTasksUseCase getAllTasksUseCase,
        IUpdateTaskUseCase updateTaskUseCase,
        IDeleteTaskUseCase deleteTaskUseCase)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTasks(
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var tasks = await getAllTasksUseCase.Handle(pageNumber, pageSize, cancellationToken);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id, CancellationToken cancellationToken = default)
        {
            var task = await getByIdUseCase.Handle(id, cancellationToken);

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskInput input,
            IValidator<CreateTaskInput> validator,
            CancellationToken cancellationToken = default)
        {
            var validate = validator.Validate(input);

            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors.Select(s => new { mesage = s.ErrorMessage }));
            }

            var task = await createTaskUseCase.Handle(input, cancellationToken);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskInput input,
            IValidator<UpdateTaskInput> validator,
            CancellationToken cancellationToken = default)
        {
            var validate = validator.Validate(input);

            if (!validate.IsValid)
                return BadRequest(validate.Errors.Select(s => new { mesage = s.ErrorMessage }));

            var taskUpdated = await updateTaskUseCase.Handle(input, cancellationToken);
            return Ok(taskUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id, CancellationToken cancellationToken = default)
        {
            await deleteTaskUseCase.Handle(id, cancellationToken);
            return Ok();
        }
    }
}
