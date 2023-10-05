using Demo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.API.Contexts;

public class DemoDbContext : DbContext, IDemoDbContext
{
    public DemoDbContext(DbContextOptions options) : base(options)
    {
    }

    public async Task<int> SaveChangesAsync()
        => await base.SaveChangesAsync();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Province>()
            .HasKey(k => k.ProvinceId);

        modelBuilder.Entity<Province>()
            .Property(p => p.ProvinceId)
            .ValueGeneratedOnAdd();

        base.OnModelCreating(modelBuilder);
    }
}