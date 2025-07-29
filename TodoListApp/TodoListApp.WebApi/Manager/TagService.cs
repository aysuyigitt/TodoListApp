using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApi.Context;
using TodoListApp.WebApi.Entitites;
using TodoListApp.WebApi.Service;

namespace TodoListApp.WebApi.Manager
{
    public class TagService : ITagService
    {
        private readonly TodoListDbContext _dbContext;

        public TagService(TodoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Tag entity)
        {
            await _dbContext.Tags.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var value = await _dbContext.Tags.FindAsync(id);
            if (value != null)
            {
                _dbContext.Tags.Remove(value);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Tag>> GetAllAsync()
        {
           return await _dbContext.Tags.ToListAsync();
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            return await _dbContext.Tags.FindAsync(id);
        }

        public async Task UpdateAsync(Tag entity)
        {
            this._dbContext.Tags.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
