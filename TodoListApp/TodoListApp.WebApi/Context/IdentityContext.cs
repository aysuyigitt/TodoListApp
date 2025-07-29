using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApi.Entitites;

namespace TodoListApp.WebApi.Context
{
    public class IdentityContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }
    }
}
