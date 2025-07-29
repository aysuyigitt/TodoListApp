using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApi.Entitites;

namespace TodoListApp.WebApi.Context
{
    public class TodoListDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
          : base(options)
        {
        }

        public DbSet<TodoListEntity> TodoListEntities { get; set; }

        public DbSet<TaskTodo> TaskTodos { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
