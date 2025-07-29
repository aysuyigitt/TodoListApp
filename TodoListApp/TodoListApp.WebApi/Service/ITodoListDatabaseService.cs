using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using TodoListApp.WebApi.Entitites;

namespace TodoListApp.WebApi.Service
{
    public interface ITodoListDatabaseService
    {
        Task<List<TodoListEntity>> GetAllAsync();

        Task<TodoListEntity> GetByIdAsync(int id);

        Task CreateAsync(TodoListEntity entity);

        Task UpdateAsync(TodoListEntity entity);

        Task DeleteAsync(int id);
    }
}
