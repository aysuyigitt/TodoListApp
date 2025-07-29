using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TodoListApp.WebApi.Context;
using TodoListApp.WebApi.Entitites;
using TodoListApp.WebApi.Model;
using TodoListApp.WebApi.Service;
using TodoListApp.WebApp.Models;

namespace TodoListApp.WebApi.Manager
{
    public class TaskTodoService : ITaskTodoService
    {
        private readonly TodoListDbContext _dbContext;

        public TaskTodoService(TodoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(TaskTodo entity)
        {
            await _dbContext.TaskTodos.AddAsync(entity);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.TaskTodos.FindAsync(id);
            if (entity != null)
            {
                _dbContext.TaskTodos.Remove(entity);
                await this._dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<TaskTodo>> GetAllAsync()
        {
            return await _dbContext.TaskTodos
                .Include(x => x.TodoListEntity)
                .ToListAsync();
        }

        public async Task<TaskTodo> GetByIdAsync(int id)
        {
            return await _dbContext.TaskTodos.FindAsync(id);
        }

        public async Task<List<TaskTodo>> GetTaskTodoByTodoListById(int id)
        {
            var values = await _dbContext.TaskTodos.Include(x => x.TodoListEntity).Include(x => x.Tags).Where(y => y.TodoListEntityId == id).ToListAsync();
            return values;
        }

        public async Task UpdateAsync(TaskTodo entity)
        {
            this._dbContext.TaskTodos.Update(entity);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<List<TaskTodoModel>> GetAllWithListNamesAsync()
        {
            var values = await _dbContext.TaskTodos
                .Include(x => x.TodoListEntity)
                .Select(x => new TaskTodoModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Title = x.TodoListEntity.Title,
                    AssignedTo = x.AssignedTo,
                    CreatedDate = x.CreatedDate,
                    DueDate = x.DueDate,
                    Status = x.Status,
                    TodoListEntityId = x.TodoListEntityId,
                })
                .ToListAsync();

            return values;
        }

        public async Task<bool> AddTagToTaskAsync(int taskId, int tagId)
        {
            var tasks = await _dbContext.TaskTodos.Include(t => t.Tags).FirstOrDefaultAsync(t => t.Id == taskId);
            var tag = await _dbContext.Tags.FindAsync(tagId);

            if (tasks == null || tag == null)
            {
                return false;
            }

            if (!tasks.Tags.Any(t => t.Id == tagId))
            {
                tasks.Tags.Add(tag);
                await _dbContext.SaveChangesAsync();
            }

            return true;
        }

        public List<TaskTodoWebApiModel> GetTaskTodoByTodoListId(int id)
        {
            var values = _dbContext.TaskTodos
        .Include(x => x.TodoListEntity)
        .Where(y => y.TodoListEntityId == id)
        .Select(todo => new TaskTodoWebApiModel
        {
            Title = todo.Title,
            Description = todo.Description,
            DueDate = todo.DueDate,
        })
        .ToList();

            return values;
        }

        async Task<List<TaskTodoWebApiModel>> ITaskTodoService.GetTaskTodoByTodoListById(int id)
        {
            var values = await _dbContext.TaskTodos
        .Include(x => x.TodoListEntity)
        .Where(y => y.TodoListEntityId == id)
        .Select(todo => new TaskTodoWebApiModel
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            DueDate = todo.DueDate,
        })
        .ToListAsync();

            return values;
        }
    }
    }