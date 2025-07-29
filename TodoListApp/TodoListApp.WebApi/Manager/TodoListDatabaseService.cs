using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApi.Context;
using TodoListApp.WebApi.Entitites;
using TodoListApp.WebApi.Service;

namespace TodoListApp.WebApi.Manager
{
    public class TodoListDatabaseService : ITodoListDatabaseService
    {
        private readonly TodoListDbContext _dbContext;

        public TodoListDatabaseService(TodoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(TodoListEntity entity)
        {
            await this._dbContext.TodoListEntities.AddAsync(entity);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.TodoListEntities.FindAsync(id);
            if (entity != null)
            {
                this._dbContext.TodoListEntities.Remove(entity);
                await this._dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<TodoListEntity>> GetAllAsync()
        {
            return await this._dbContext.TodoListEntities.ToListAsync();
        }

        public async Task<TodoListEntity> GetByIdAsync(int id)
        {
            return await _dbContext.TodoListEntities.FindAsync(id);
        }

        public async Task UpdateAsync(TodoListEntity entity)
        {
            this._dbContext.TodoListEntities.Update(entity);
            await this._dbContext.SaveChangesAsync();
        }
    }
}
