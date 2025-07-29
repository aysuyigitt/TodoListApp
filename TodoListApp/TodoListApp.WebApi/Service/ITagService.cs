using TodoListApp.WebApi.Entitites;

namespace TodoListApp.WebApi.Service
{
    public interface ITagService
    {
        Task<List<Tag>> GetAllAsync();

        Task<Tag> GetByIdAsync(int id);

        Task CreateAsync(Tag entity);

        Task UpdateAsync(Tag entity);

        Task DeleteAsync(int id);
    }
}
