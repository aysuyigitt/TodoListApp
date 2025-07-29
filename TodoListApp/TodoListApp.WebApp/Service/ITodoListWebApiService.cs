using TodoListApp.WebApp.Models;

namespace TodoListApp.WebApp.Service
{
    public interface ITodoListWebApiService
    {
        Task<List<TodoListWebApiModel>> GetTodoListsAsync();
    }
}
