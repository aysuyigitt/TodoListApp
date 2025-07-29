using TodoListApp.WebApp.Models;

namespace TodoListApp.WebApp.Service
{
    public interface ITaskTodoWebApiService
    {
        Task<List<TaskTodoWebApiModel>> GetTaskTodosAsync();
        Task<List<TaskTodoWebApiModel>> GetTaskTodosByTodoListIdAsync(int todoListId);
    }
}
