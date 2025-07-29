using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApi.Entitites;
using TodoListApp.WebApi.Model;
using TodoListApp.WebApi.Service;

namespace TodoListApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListDatabaseService _databaseService;

        public TodoListController(ITodoListDatabaseService databaseService)
        {
            this._databaseService = databaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _databaseService.GetAllAsync();

            var models = entities.Select(e => new TodoListModel
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
            });
            return this.Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoList(int id)
        {
            var entity = await _databaseService.GetByIdAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            var model = new TodoListModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
            };

            return this.Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoList(TodoListModel model)
        {
            var entity = new TodoListEntity
            {
                Title = model.Title,
                Description = model.Description,
            };

            await this._databaseService.CreateAsync(entity);

            var responseModel = new TodoListModel
            {
                Title = entity.Title,
                Description = entity.Description,
            };

            return this.CreatedAtAction(nameof(this.GetTodoList), new { id = responseModel.Id }, responseModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoList(int id, TodoListModel model)
        {
            var updateEntity = await this._databaseService.GetByIdAsync(id);
            if (updateEntity == null)
            {
                return this.NotFound();
            }

            updateEntity.Title = model.Title;
            updateEntity.Description = model.Description;

            await this._databaseService.UpdateAsync(updateEntity);
            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(int id)
        {
            var existing = await this._databaseService.GetByIdAsync(id);
            if (existing == null)
            {
                return this.NotFound();
            }

            await this._databaseService.DeleteAsync(id);
            return this.NoContent();
        }
    }
}
