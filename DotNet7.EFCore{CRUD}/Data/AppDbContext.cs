using DotNet7.EFCore_CRUD_.Model;
using Microsoft.EntityFrameworkCore;

namespace DotNet7.EFCore_CRUD_.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<BlogModel> Blogs { get; set; }
}
