using TodoListApp.WebApi.Entitites;
using TodoListApp.WebApi.Model;
using TodoListApp.WebApp.Models;

namespace TodoListApp.WebApi.Service
{
    public interface ITaskTodoService
    {
        Task<List<TaskTodo>> GetAllAsync();

        Task<TaskTodo> GetByIdAsync(int id);

        Task CreateAsync(TaskTodo entity);

        Task UpdateAsync(TaskTodo entity);

        Task DeleteAsync(int id);

        Task<List<TaskTodoWebApiModel>> GetTaskTodoByTodoListById(int id);

        Task<bool> AddTagToTaskAsync(int taskId, int tagId);

    }
}
