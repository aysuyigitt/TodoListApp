using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TodoListApp.WebApi.Entitites;
using TodoListApp.WebApi.Model;
using TodoListApp.WebApi.Service;
using TodoListApp.WebApp.Models;

namespace TodoListApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTodosController : ControllerBase
    {
        private readonly ITaskTodoService _taskTodoService;

        public TaskTodosController(ITaskTodoService taskTodoService)
        {
            _taskTodoService = taskTodoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _taskTodoService.GetAllAsync();
            var model = entities.Select(x => new TaskTodoModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                AssignedTo = x.AssignedTo,
                CreatedDate = x.CreatedDate,
                DueDate = x.DueDate,
                Status = x.Status,
                TodoListEntityId = x.TodoListEntityId,
                TodoName = x.TodoListEntity.Title,
            });
            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskTodo(int id)
        {
            var entity = await _taskTodoService.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var model = new TaskTodoModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                AssignedTo = entity.AssignedTo,
                CreatedDate = entity.CreatedDate,
                DueDate = entity.DueDate,
                Status = entity.Status,
                TodoListEntityId = entity.TodoListEntityId,
            };
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskTodo(TaskTodoModel taskTodo)
        {
            var entity = new TaskTodo
            {
                Title = taskTodo.Title,
                Description = taskTodo.Description,
                AssignedTo = taskTodo.AssignedTo,
                CreatedDate = taskTodo.CreatedDate,
                DueDate = taskTodo.DueDate,
                Status = taskTodo.Status,
                TodoListEntityId = taskTodo.TodoListEntityId,
            };

            await _taskTodoService.CreateAsync(entity);

            var responseModel = new TaskTodoModel
            {
                Title = entity.Title,
                Description = entity.Description,
                AssignedTo = entity.AssignedTo,
                CreatedDate = entity.CreatedDate,
                DueDate = entity.DueDate,
                Status = entity.Status,
                TodoListEntityId = entity.TodoListEntityId,
            };
            return this.CreatedAtAction(nameof(this.GetTaskTodo), new { id = responseModel.Id }, responseModel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTaskTodo(int id, TaskTodoModel taskTodoModel)
        {
            var updateEntity = await _taskTodoService.GetByIdAsync(id);
            if (updateEntity == null)
            {
                return NotFound();
            }

            updateEntity.Title = taskTodoModel.Title;
            updateEntity.Description = taskTodoModel.Description;
            updateEntity.AssignedTo = taskTodoModel.AssignedTo;
            updateEntity.CreatedDate = taskTodoModel.CreatedDate;
            updateEntity.DueDate = taskTodoModel.DueDate;
            updateEntity.Status = taskTodoModel.Status;
            updateEntity.TodoListEntityId = taskTodoModel.TodoListEntityId;

            await this._taskTodoService.UpdateAsync(updateEntity);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTaskTodo(int id)
        {
            var existing = await this._taskTodoService.GetByIdAsync(id);
            if (existing == null)
            {
                return this.NotFound();
            }

            await this._taskTodoService.DeleteAsync(id);
            return this.NoContent();
        }

        [HttpGet("GetTaskTodoByTodoListById")]
        public async Task<ActionResult<List<TaskTodoWebApiModel>>> GetTaskTodoByTodoListById(int id)
        {
            var values = await _taskTodoService.GetTaskTodoByTodoListById(id);
            return Ok(values);
        }

        [HttpPost("GetByTodoListId")]
        public async Task<IActionResult> AddTagToTask(int taskId, int tagId)
        {
            var result = await _taskTodoService.AddTagToTaskAsync(taskId, tagId);

            return Ok("Etiket başarıyla ilişkilendirildi.");
        }

        [HttpGet("GetTaskTodoByTodoListId")]
        public ActionResult<List<TaskTodo>> GetTaskTodoByTodoListId(int id)
        {
            var values = _taskTodoService.GetTaskTodoByTodoListById(id);
            return Ok(values);
        }
    }
}
