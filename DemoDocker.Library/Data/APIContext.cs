using DemoDocker.Library.Modes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoDocker.Library.Data;

public class APIContext : DbContext
{


    public APIContext(DbContextOptions<APIContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Todo> Todos { get ; set ; } = default!;
}
